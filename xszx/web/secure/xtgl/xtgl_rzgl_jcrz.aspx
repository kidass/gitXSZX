<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="xtgl_rzgl_jcrz.aspx.vb" Inherits="Xydc.Platform.web.xtgl_rzgl_jcrz" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>用户进出系统日志</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdJCRZLocked {; LEFT: expression(divJCRZ.scrollLeft); POSITION: relative}
			TH.grdJCRZLocked {; LEFT: expression(divJCRZ.scrollLeft); POSITION: relative}
			TH.grdJCRZLocked {Z-INDEX: 99}
			TH {Z-INDEX: 10; POSITION: relative}
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
				
				if (document.all("divJCRZ") == null)
					return;
				
				dblHeight = 390 + dblDeltaY + document.body.clientHeight - 570; //default state : 390px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				dblWidth  = 800 + dblDeltaX + document.body.clientWidth  - 850; //default state : 800px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				divJCRZ.style.width  = strWidth;
				divJCRZ.style.height = strHeight;
				divJCRZ.style.clip   = "rect(0px " + strWidth + " " + strHeight + " 0px)";
			}
			function document_onreadystatechange() 
			{
				return window_onresize();
			}
		//-->
		</script>
		<script language="javascript" event="onreadystatechange" for="document">
		<!--
			return document_onreadystatechange()
		//-->
		</script>
	</HEAD>
	<body onresize="return window_onresize()" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../../images/oabk.gif">
		<form id="frmXTGL_RZGL_JCRZ" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="5"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD class="title" align="center" colSpan="3" height="30">用户进出系统日志<asp:LinkButton id="lnkBlank" Runat="server" Width="0px"></asp:LinkButton></TD>
								</TR>
								<TR>
									<TD width="5"></TD>
									<TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid" vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD class="label" align="left">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" vAlign="middle">用户标识&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtJCRZSearch_YHBS" runat="server" Font-Size="12px" CssClass="textbox" Columns="10" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;用户名称&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtJCRZSearch_YHMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="16" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;IP&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtJCRZSearch_JQDZ" runat="server" Font-Size="12px" CssClass="textbox" Columns="16" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;操作类型&nbsp;</TD>
															<TD class="label" align="left"><asp:DropDownList ID="ddlJCRZSearch_CZLX" Runat="server" Font-Size="12px" CssClass="textbox" Font-Names="宋体">
																<asp:ListItem Value=""></asp:ListItem>
																<asp:ListItem Value="登录">登录</asp:ListItem>
																<asp:ListItem Value="退出">退出</asp:ListItem>
															</asp:DropDownList></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;操作时间&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtJCRZSearch_CZSJMin" runat="server" Font-Size="12px" CssClass="textbox" Columns="10" Font-Names="宋体"></asp:textbox>~<asp:textbox id="txtJCRZSearch_CZSJMax" runat="server" Font-Size="12px" CssClass="textbox" Columns="10" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label">&nbsp;<asp:button id="btnJCRZSearch" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text="快速搜索"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<DIV id="divJCRZ" style="TABLE-LAYOUT: fixed; OVERFLOW: auto; WIDTH: 800px; CLIP: rect(0px 800px 390px 0px); HEIGHT: 390px">
														<asp:datagrid id="grdJCRZ" runat="server" Font-Size="12px" CssClass="label" Font-Names="宋体" CellPadding="4"
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
																		<asp:CheckBox id="chkJCRZ" runat="server" AutoPostBack="False"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:ButtonColumn DataTextField="序号" SortExpression="序号" HeaderText="序号" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="操作人" SortExpression="操作人" HeaderText="用户标识" CommandName="Select">
																	<HeaderStyle Width="160px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="操作人名称" SortExpression="操作人名称" HeaderText="用户名称" CommandName="Select">
																	<HeaderStyle Width="240px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="操作时间" SortExpression="操作时间" HeaderText="操作时间" CommandName="Select" DataTextFormatString="{0:yyyy-MM-dd HH:mm:ss}">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="操作类型" SortExpression="操作类型" HeaderText="操作类型" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="机器地址" SortExpression="机器地址" HeaderText="主机地址" CommandName="Select">
																	<HeaderStyle Width="160px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="机器名称" SortExpression="机器名称" HeaderText="主机名称" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtJCRZFixed" type="hidden" value="0" runat="server"></DIV>
												</TD>
											</TR>
											<TR>
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZDeSelectAll" runat="server" CssClass="labelBlack">不选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZSelectAll" runat="server" CssClass="labelBlack">全选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZGotoPage" runat="server"  CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtJCRZPageIndex" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZJCRZSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtJCRZPageSize" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" noWrap align="right"><asp:label id="lblJCRZGridLocInfo" runat="server" Font-Size="12px" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD align="center" height="28">清理开始时间：<asp:TextBox id="txtJCRZ_QSRQ" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="12"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;清理结束时间：<asp:TextBox id="txtJCRZ_ZZRQ" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="12"></asp:TextBox></TD>
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
							<asp:Button id="btnDeleteSelect" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 选定清除 " Height="36px"></asp:Button>
							<asp:Button id="btnDeleteInterval" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 清除时段 " Height="36px"></asp:Button>
							<asp:Button id="btnClearAll" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 全部清除 " Height="36px"></asp:Button>
							<asp:Button id="btnSearch" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 全文检索 " Height="36px"></asp:Button>
							<asp:Button id="btnPrint" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 打    印 " Height="36px"></asp:Button>
							<asp:Button id="btnClose" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 返    回 " Height="36px"></asp:Button>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<asp:panel id="panelError" Runat="server">
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
			</asp:panel>
			<table cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<td><input id="htxtSessionIdQuery" type="hidden" runat="server">
						<input id="htxtJCRZQuery" type="hidden" runat="server">
						<input id="htxtJCRZRows" type="hidden" runat="server">
						<input id="htxtJCRZSort" type="hidden" runat="server">
						<input id="htxtJCRZSortColumnIndex" type="hidden" runat="server">
						<input id="htxtJCRZSortType" type="hidden" runat="server">
						<input id="htxtDivLeftJCRZ" type="hidden" runat="server">
						<input id="htxtDivTopJCRZ" type="hidden" runat="server">
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
							function ScrollProc_divJCRZ() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopJCRZ");
								if (oText != null) oText.value = divJCRZ.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftJCRZ");
								if (oText != null) oText.value = divJCRZ.scrollLeft;
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
								oText=document.getElementById("htxtDivTopJCRZ");
								if (oText != null) divJCRZ.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftJCRZ");
								if (oText != null) divJCRZ.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divJCRZ.onscroll = ScrollProc_divJCRZ;
							}
							catch (e) {}
						</script>
					</td>
				</tr>
				<tr>
					<td>
						<script language="javascript">window_onresize();</script>
						<uwin:popmessage id="popMessageObject" runat="server" EnableViewState="False" PopupWindowType="Normal" ActionType="OpenWindow" Visible="False" width="96px" height="48px"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
