<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dmxz_jbdm.aspx.vb" Inherits="Xydc.Platform.web.dmxz_jbdm" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>基础代码选择窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdCodeDataLocked { ; LEFT: expression(divCodeData.scrollLeft); POSITION: relative }
			TH.grdCodeDataLocked { ; LEFT: expression(divCodeData.scrollLeft); POSITION: relative }
			TH { Z-INDEX: 10; POSITION: relative }
			TH.grdCodeDataLocked { Z-INDEX: 99 }
		</style>
		<script src="../../scripts/transkey.js"></script>
		<script language="javascript">
            function btnReset_onclick() 
            {
                try {
                    var objInput 
                    objInput = document.getElementById("txtSearch_DM");
                    objInput.value = "";
                    objInput = document.getElementById("txtSearch_MC");
                    objInput.value = "";
                } catch (e) {}
            }
		</script>
		<script language="javascript" for="btnReset" event="onclick">
            return btnReset_onclick()
		</script>
		<script language="javascript">
		<!--
			function window_onresize() 
			{
				var dblHeight  = 0;
				var dblWidth   = 0;
				var strHeight  = "";
				var strWidth   = "";
				var dblDeltaX  = 0;
				var dblDeltaY  = 30;
				
				if (document.all("divCodeData") == null)
					return;
				
				dblHeight = 320 + dblDeltaY + document.body.clientHeight - 570; //default state : 320px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				dblWidth  = 812 + dblDeltaX + document.body.clientWidth  - 850; //default state : 812px
				strWidth  = parseInt(dblWidth.toString(), 10).toString() + "px";
				divCodeData.style.width  = strWidth;
				divCodeData.style.height = strHeight;
				divCodeData.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
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
		<form id="frmDMXZ_JBDM" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD colSpan="3" height="5"></TD>
					</TR>
					<TR>
						<TD width="5"></TD>
						<TD align="center">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR vAlign="middle" align="left" height="30">
									<TD class="label" vAlign="middle" align="center"><asp:Label id="lblTitle" Runat="server" Font-Name="宋体" Font-Size="12px" Font-Bold="True"></asp:Label></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5"></TD>
					</TR>
					<TR>
						<TD colSpan="3" height="2"></TD>
					</TR>
					<TR>
						<TD width="5"></TD>
						<TD vAlign="top" align="center" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD class="tips" align="left" colSpan="3"></TD>
								</TR>
								<TR>
									<TD width="5"></TD>
									<TD style="" vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR align="center">
												<TD class="label" align="left">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" vAlign="middle" align="right" height="30"><asp:Label id="lblSearch_DM" Runat="server" Font-Name="宋体" Font-Size="12px"></asp:Label>&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtSearch_DM" runat="server" Font-Size="12px" CssClass="textbox" Columns="16" Font-Names="宋体"></asp:textbox>&nbsp;</TD>
															<TD class="label" vAlign="middle" align="right"><asp:Label id="lblSearch_MC" Runat="server" Font-Name="宋体" Font-Size="12px"></asp:Label>&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtSearch_MC" runat="server" Font-Size="12px" CssClass="textbox" Columns="36" Font-Names="宋体"></asp:textbox>&nbsp;</TD>
															<TD class="label"><asp:button id="btnSearch" Runat="server" Font-Name="宋体" Font-Size="12px" Width="60px" CssClass="button" Text="搜索"></asp:button>&nbsp;<INPUT class="button" id="btnReset" style="WIDTH: 60px" type="button" value="清空"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<DIV id="divCodeData" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 812px; CLIP: rect(0px 812px 320px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 320px">
														<asp:datagrid id="grdCodeData" runat="server" Font-Size="12px" CssClass="label" Font-Names="宋体"
															UseAccessibleHeader="True" AutoGenerateColumns="False" GridLines="Vertical" BackColor="White"
															BorderStyle="None" CellPadding="4" AllowPaging="True" PageSize="30" AllowSorting="True" BorderWidth="0px"
															BorderColor="#DEDFDE">
															<FooterStyle BackColor="#CCCC99"></FooterStyle>
															<SelectedItemStyle Font-Size="12px" Font-Names="宋体" Font-Bold="False" VerticalAlign="Middle" ForeColor="#CC0000" BackColor="#FFFFDD"></SelectedItemStyle>
															<EditItemStyle Font-Size="12px" Font-Names="宋体" VerticalAlign="Middle" BackColor="#FFCC00"></EditItemStyle>
															<AlternatingItemStyle Font-Size="12px" Font-Names="宋体" VerticalAlign="Middle" BackColor="White"></AlternatingItemStyle>
															<ItemStyle Font-Size="12px" Font-Names="宋体" BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
															<HeaderStyle Font-Size="12px" Font-Names="宋体" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" BackColor="#87cefa" HorizontalAlign="Left"></HeaderStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="选">
																	<HeaderStyle HorizontalAlign="Left" Width="20px"></HeaderStyle>
																	<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
																	<ItemTemplate>
																		<asp:CheckBox id="chkCodeData" runat="server" AutoPostBack="False"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtCODEDATAFixed" type="hidden" value="0" runat="server">
													</DIV>
												</TD>
											</TR>
											<TR align="center">
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZDeSelectAll" runat="server" CssClass="labelBlack">不选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSelectAll" runat="server" CssClass="labelBlack">全选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveFrst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZGotoPage" runat="server" CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtPageIndex" runat="server" Font-Name="宋体" Font-Size="12px" Width="60px" CssClass="textbox" Columns="2">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtPageSize" runat="server" Font-Name="宋体" Font-Size="12px" Width="60px" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right" width="200"><asp:label id="lblGridLocInfo" runat="server" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD height="3"></TD>
											</TR>
											<TR>
												<TD>
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" align="left" style="display:none">&nbsp;&nbsp;输入新代码：<asp:TextBox id="txtNewDM" Runat="server" Font-Name="宋体" Font-Size="12px" Columns="60" Height="24px"></asp:TextBox><asp:Button id="btnAddNew" Runat="server" Font-Name="宋体" Font-Size="12px" Text=" 输入确认 " Height="24px"></asp:Button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD height="3"></TD>
											</TR>
											<TR>
												<TD class="label" align="center">
													<asp:button id="btnOK" Runat="server" Font-Name="宋体" Font-Size="12px" Width="120px" CssClass="button" Text="选值确定" Height="36px"></asp:button>&nbsp;&nbsp;
													<span style="display:none"><asp:button id="btnOKNull" Runat="server" Font-Name="宋体" Font-Size="12px" Width="120px" CssClass="button" Text="空值确定" Height="36px"></asp:button>&nbsp;&nbsp;</span>
													<asp:button id="btnCancel" Runat="server" Font-Name="宋体" Font-Size="12px" Width="120px" CssClass="button" Text="取    消" Height="36px"></asp:button></TD>
											</TR>
											<TR>
												<TD height="3"></TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="5"></TD>
								</TR>
								<TR>
									<TD colSpan="3" height="5"></TD>
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
						<input id="htxtLocalSessionId" type="hidden" runat="server">
						<input id="htxtSortType" type="hidden" runat="server">
						<input id="htxtSortColumnIndex" type="hidden" runat="server">
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
							function ScrollProc_divCodeData() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopObject");
								if (oText != null) oText.value = divCodeData.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) oText.value = divCodeData.scrollLeft;
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
								if (oText != null) divCodeData.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) divCodeData.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divCodeData.onscroll = ScrollProc_divCodeData;
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
