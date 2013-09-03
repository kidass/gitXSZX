<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customer_medium_typechoice.aspx.vb" Inherits="Xydc.Platform.web.customer_medium_typechoice" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>类型选择窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Expires" CONTENT="0"> 
        <meta http-equiv="Cache-Control" CONTENT="no-cache"> 
        <meta http-equiv="Pragma" CONTENT="no-cache"> 
        <base target="_self" />
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
		    TD.grdSELBMLocked { ; LEFT: expression(divSELBM.scrollLeft); POSITION: relative }
		    TH.grdSELBMLocked { ; LEFT: expression(divSELBM.scrollLeft); POSITION: relative }
		    TH.grdSELBMLocked { Z-INDEX: 99 }
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
				
				if (document.all("divMAIN") == null)
					return;
				
				dblHeight = 470 + dblDeltaY + document.body.clientHeight - 570; //default state : 470px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				strWidth  = "100%";
				divMAIN.style.width  = strWidth;
				divMAIN.style.height = strHeight;
				divMAIN.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
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
		<form id="frmDMXZ_ZCBM" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="3"></TD>
						<TD align="center" style="BORDER-BOTTOM: #99cccc 1px solid">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR vAlign="middle" align="left">
									<TD class="label" vAlign="middle" align="center" height="24"><B>人员类型选择窗<asp:Label id="BMlTitle" Runat="server" CssClass="label"></asp:Label></B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="3"></TD>
					</TR>
					<TR>
						<TD width="3"></TD>
						<TD vAlign="top" align="center">
							<div id="divMAIN" style="OVERFLOW: auto; WIDTH: 320px; CLIP: rect(0px 320px 400px 0px); HEIGHT: 400px">
								<TABLE cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD class="tips" align="left" colSpan="5"><asp:LinkButton id="lnkBlank" Runat="server" Width="0px" Height="5px"></asp:LinkButton></TD>
									</TR>
									<TR>										
										<TD>
											<DIV id="divSELBM" style="TABLE-LAYOUT: fixed; OVERFLOW: auto; WIDTH: 150px; CLIP: rect(0px 150px 200px 0px); HEIGHT: 200px">
												<asp:datagrid id="grdSELBM" runat="server" CssClass="labelGrid" Width="140px"
													UseAccessibleHeader="True" CellPadding="4" AllowSorting="True" BorderWidth="1px" BorderColor="#DEDFDE"
													PageSize="30" BorderStyle="None" BackColor="White" GridLines="Vertical" AutoGenerateColumns="False"
													AllowPaging="True">
														<SelectedItemStyle  Font-Bold="False" VerticalAlign="Middle" ForeColor="blue" ></SelectedItemStyle>
														<EditItemStyle   BackColor="#FFCC00" VerticalAlign="Middle"></EditItemStyle>
														<AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
														<ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
														<HeaderStyle CssClass="FixedHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
														<FooterStyle BackColor="#CCCC99"></FooterStyle><Columns>
														<asp:TemplateColumn   HeaderText="选" ItemStyle-Width="20px">
															<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
															<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
															<ItemTemplate>
																<asp:CheckBox id="chkSELBM" runat="server" AutoPostBack="False"></asp:CheckBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:ButtonColumn DataTextField="人员类型" SortExpression="人员类型" HeaderText="人员类型" CommandName="Select">
															<HeaderStyle Width="100px"></HeaderStyle>
														</asp:ButtonColumn>
													</Columns>
													<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
												</asp:datagrid><INPUT id="htxtSELBMFixed" type="hidden" value="0" runat="server" NAME="htxtSELBMFixed">
											</DIV>
										</TD>
									</TR>
									<TR>
										<TD class="label"><div style="display:none">
											<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
												<TR>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMDeSelectAll" runat="server" CssClass="labeBMlack">不选</asp:linkbutton></TD>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMSelectAll" runat="server" CssClass="labeBMlack">全选</asp:linkbutton></TD>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMMoveFirst" runat="server" CssClass="labeBMlack">最前</asp:linkbutton></TD>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMMovePrev" runat="server" CssClass="labeBMlack">前页</asp:linkbutton></TD>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMMoveNext" runat="server" CssClass="labeBMlack">下页</asp:linkbutton></TD>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMMoveLast" runat="server" CssClass="labeBMlack">最后</asp:linkbutton></TD>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMGotoPage" runat="server"  CssClass="labeBMlack">前往</asp:linkbutton><asp:textbox id="txtSELBMPageIndex" runat="server" CssClass="textbox" Font-Size="12px" Font-Name="宋体" Columns="3">1</asp:textbox>页</TD>
													<TD class="labeBMlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSELBMSetPageSize" runat="server" CssClass="labeBMlack">每页</asp:linkbutton><asp:textbox id="txtSELBMPageSize" runat="server" CssClass="textbox" Font-Size="12px" Font-Name="宋体" Columns="3">30</asp:textbox>条</TD>
													<TD class="labeBMlack" vAlign="middle" align="right" width="140"><asp:label id="BMlSELBMGridLocInfo" runat="server" CssClass="labeBMlack" >1/10 N/15</asp:label></TD>
												</TR>
											</TABLE></div>
										</TD>
									</TR>											
									<TR>
										<TD colSpan="5" height="3"></TD>
									</TR>
								</TABLE>
							</div>
						</TD>
						<TD width="3"></TD>
					</TR>
					<TR>
						<TD colSpan="3" height="3"></TD>
					</TR>
					<TR>
						<TD align="center" colspan="3" style="BORDER-TOP: #99cccc 1px solid">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							    <tr>
							        <td  height="3"></td>
							    </tr>
								<TR vAlign="middle" align="left">
									<TD class="label" vAlign="middle" align="center" height="30"><asp:Button id="btnOK" Runat="server" Height="30px" Font-Size="12px" Font-Name="宋体" Text=" 确  定 "></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button id="btnCancel" Runat="server" Height="30px" Font-Size="12px" Font-Name="宋体" Text=" 取  消 "></asp:Button></TD>
								</TR>
							</TABLE>
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
									<TD id="tdErrInfo" style="FONT-SIZE: 32pt; COLOR: black; LINE-HEIGHT: 40pt; FONT-FAMILY: 宋体; LETTER-SPACING: 2pt" align="center"><asp:Label id="BMlMessage" Runat="server"></asp:Label><p>&nbsp;&nbsp;</p><p><input type="button" id="btnGoBack" value=" 返回 " style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" onclick="javascript:history.back();"></p></TD>
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
						<input id="htxtSessionIdSELBM" type="hidden" runat="server" NAME="htxtSessionIdSELBM">
						<input id="htxtSELBMSort" type="hidden" runat="server" NAME="htxtSELBMSort">
						<input id="htxtSELBMSortColumnIndex" type="hidden" runat="server" NAME="htxtSELBMSortColumnIndex">
						<input id="htxtSELBMSortType" type="hidden" runat="server" NAME="htxtSELBMSortType">
						<input id="htxtDivLeftSELBM" type="hidden" runat="server" NAME="htxtDivLeftSELBM">
						<input id="htxtDivTopSELBM" type="hidden" runat="server" NAME="htxtDivTopSELBM">
						<input id="htxtDivLeftMAIN" type="hidden" runat="server" NAME="htxtDivLeftMAIN">
						<input id="htxtDivTopMAIN" type="hidden" runat="server" NAME="htxtDivTopMAIN">
						<input id="htxtDivLeftBody" type="hidden" runat="server" NAME="htxtDivLeftBody">
						<input id="htxtDivTopBody" type="hidden" runat="server" NAME="htxtDivTopBody">
						<input id="htxtReturnValue" type="hidden" runat="server" NAME="htxtReturnValue">
						
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
							function ScrollProc_divMAIN() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopMAIN");
								if (oText != null) oText.value = divMAIN.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftMAIN");
								if (oText != null) oText.value = divMAIN.scrollLeft;
								return;
							}							
							function ScrollProc_divSELBM() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopSELBM");
								if (oText != null) oText.value = divSELBM.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftSELBM");
								if (oText != null) oText.value = divSELBM.scrollLeft;
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
								oText=document.getElementById("htxtDivTopMAIN");
								if (oText != null) divMAIN.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftMAIN");
								if (oText != null) divMAIN.scrollLeft = oText.value;

								oText=null;
								oText=document.getElementById("htxtDivTopSELBM");
								if (oText != null) divSELBM.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftSELBM");
								if (oText != null) divSELBM.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divMAIN.onscroll = ScrollProc_divMAIN;
								divSELBM.onscroll = ScrollProc_divSELBM;
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
