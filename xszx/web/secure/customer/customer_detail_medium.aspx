<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customer_detail_medium.aspx.vb" Inherits="Xydc.Platform.web.customer_detail_medium" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>二手客户明细数据查询界面</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<LINK href="../../filecss/mnuStyle01.css" type="text/css" rel="stylesheet">
		<style>
		    TD.grdComputeLocked { ; LEFT: expression(divCompute.scrollLeft); POSITION: relative }
		    TH.grdComputeLocked { ; LEFT: expression(divCompute.scrollLeft); POSITION: relative }
		    TH.grdComputeLocked { Z-INDEX: 99 }
		    TH { Z-INDEX: 10; POSITION: relative }
		</style>
		<style type="text/css">
		    .fixeHead
              {
	               position:relative ;	
	               top:expression(this.offsetParent.scrollTop);
               }
		</style>
		<script src="../../scripts/transkey.js"></script>
		 <script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
		<script language="javascript">
		
		 function openBMXZDC(){ 
            var k = window.showModalDialog("../customer/customer_medium_typechoice.aspx",window,"dialogWidth:350px;status:no;dialogHeight:300px"); 
            if(k != null) 
                    document.getElementById("txtCustomerType").value = k; 
            } 
            
		<!--
			function window_onresize() 
			{
				var dblHeight = 0;
				var dblWidth  = 0;
				var strHeight = "";
				var strWidth  = "";
				
				if (document.all("divCompute") == null)
					return;
				
				intWidth   = document.body.clientWidth;   //总宽度
				intWidth  -= 24;                          //滚动条
				intWidth  -= 2 * 4;                       //左、右空白
				intWidth  -= 16;                          //调整数
				strWidth   = intWidth.toString() + "px";
				
				intHeight  = document.body.clientHeight;  //总高度
				intHeight -= 140;                          //调整数
				intHeight -= trRow01.clientHeight
				strHeight  = intHeight.toString() + "px";
                //alert(strWidth + " " + strHeight);
                
				divCompute.style.width  = strWidth;
				divCompute.style.height = strHeight;
				divCompute.style.clip   = "rect(0px " + strWidth + " " + strHeight + " 0px)";
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
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onresize="return window_onresize()">
		<form id="frmestate_es_hetong_list" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="5"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR id="trRow01">
									<TD class="title" align="center" colSpan="3" height="30">【<%=propRYMC%>】能查看的明细数据统计<asp:LinkButton id="lnkBlank" Runat="server" Width="0px"></asp:LinkButton></TD>
								</TR>
								<tr>
								    <td height="4"></td>
								</tr>
								<TR>
									<TD width="5"></TD>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR id="trRow02">
												<TD class="label" align="center" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<tr>
											                <td class="label"  align="right" >&nbsp;&nbsp;公司名称&nbsp;&nbsp;</TD>
												            <td class="label" align="left" ><asp:textbox  id="txtCompanyName" runat="server" CssClass="textbox" Columns="15"></asp:textbox></TD>
											               	<td class="label"  align="right" >&nbsp;&nbsp;职务&nbsp;&nbsp;</TD>
												            <td class="label" align="left"><asp:textbox  id="txtPosition" runat="server" CssClass="textbox" Columns="15"></asp:textbox></TD>
											                <td class="label"  align="right" >&nbsp;&nbsp;地址&nbsp;&nbsp;</TD>
												            <td class="label" align="left"><asp:textbox  id="txtAddress" runat="server" CssClass="textbox" Columns="15"></asp:textbox></TD>
														    <TD class="label"  align="right">&nbsp;人员类型&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtCustomerType" runat="server" CssClass="textbox" Columns="20"></asp:textbox><input type ="button" value="…" onclick="openBMXZDC()" /></TD>
											                <td  align="left" rowspan="4" valign="top">
													            <asp:button id="btnSearch" Runat="server" CssClass="button" Text=" 查 询 "></asp:button>
													            <asp:button id="btnCancel" Runat="server" CssClass="button" Text=" 返 回 "></asp:button>
												            </td>
														</tr>
													</TABLE>
												</TD>
													
											</TR>
											<TR>
												<TD>
													<DIV id="divCompute" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 964px; CLIP: rect(0px 964px 382px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 382px">
														<asp:datagrid id="grdCompute" runat="server" CssClass="labelGrid" Width="1040px"
															CellPadding="4" AllowSorting="True" BorderWidth="0px" BorderColor="#dfdfdf" PageSize="30"
															BorderStyle="None" BackColor="White" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="True"
															UseAccessibleHeader="False">
															<SelectedItemStyle Font-Bold="False" VerticalAlign="Middle" ForeColor="blue"></SelectedItemStyle>
															<EditItemStyle VerticalAlign="Middle" BackColor="#FFCC00"></EditItemStyle>
															<AlternatingItemStyle BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" BackColor="White"></AlternatingItemStyle>
															<ItemStyle BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" HorizontalAlign="center" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
															<HeaderStyle CssClass="fixeHead" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" BackColor="#6699cc" HorizontalAlign="center" ></HeaderStyle>
															<FooterStyle BackColor="#CCCC99"></FooterStyle>
															
															<Columns>																
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="人员类型" SortExpression="人员类型" HeaderText="人员类型" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="公司名称" SortExpression="公司名称" HeaderText="公司名称" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="法定代表人" SortExpression="法定代表人" HeaderText="法定代表人" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="电话" SortExpression="电话" HeaderText="电话" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="移动电话" SortExpression="移动电话" HeaderText="移动电话" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="联系人一" SortExpression="联系人一" HeaderText="联系人一" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="联系人二" SortExpression="联系人二" HeaderText="联系人二" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="称呼" SortExpression="称呼" HeaderText="称呼" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>																	
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="职务" SortExpression="职务" HeaderText="职务" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="地址" SortExpression="地址" HeaderText="地址" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="注册资本" SortExpression="注册资本" HeaderText="注册资本" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="年营业额" SortExpression="年营业额" HeaderText="年营业额" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="车辆品牌" SortExpression="车辆品牌" HeaderText="车辆品牌" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>																														
															</Columns>
															
															<PagerStyle Visible="False" NextPageText="下页" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtHTFixed" type="hidden" value="0" runat="server"></DIV>
												</TD>
											</TR>	
											<TR>
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZBMRYMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZBMRYMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZBMRYMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZBMRYMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZBMRYGotoPage" runat="server"  CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtBMRYPageIndex" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZBMRYSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtBMRYPageSize" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right" width="200"><asp:label id="lblBMRYGridLocInfo" runat="server" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>										
										</TABLE>
									</TD>
									<TD width="5"></TD>
								</TR>
								<tr>
								    	<TD  width="5" colSpan="5" height="10"></TD>
								</tr>
								<TR>
								    
									<TD colSpan="2" height="3" align="right"><asp:linkbutton id="lnkExportData" runat="server" Font-Size="12px" Font-Name="宋体">导出数据</asp:linkbutton></TD>
									<TD width="5" height="10">&nbsp;</TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5"></TD>
					</TR >
					<TR >
						<TD colSpan="3" height="3"></TD>
					</TR>					
				</TABLE>
			</asp:panel>
			<asp:Panel id="panelError" Runat="server">
				<TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="5%"></TD>
						<TD>
							<TABLE id="tabErrInfo" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD id="tdErrInfo" style="FONT-SIZE: 32pt; COLOR: black; LINE-HEIGHT: 40pt; FONT-FAMILY: 宋体; LETTER-SPACING: 2pt" align="center"><asp:Label id="lblMessage" Runat="server"></asp:Label><p>&nbsp;&nbsp;</p><p><asp:Button ID="btnGoBack" Runat="server" Font-Size="24pt" Text=" 返回 "></asp:Button></p></TD>
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
						<input id="htxtComputeQuery" type="hidden" runat="server">						
						<input id="htxtType" type="hidden" runat="server">
						<input id="htxtStartDate" type="hidden" runat="server">
						<input id="htxtEndDate" type="hidden" runat="server">
						<input id="htxtComputeRows" type="hidden" runat="server">
						<input id="htxtComputeSort" type="hidden" runat="server">
						<input id="htxtComputeSortColumnIndex" type="hidden" runat="server">
						<input id="htxtComputeSortType" type="hidden" runat="server">
						<input id="htxtDivLeftCompute" type="hidden" runat="server">
						<input id="htxtDivTopCompute" type="hidden" runat="server">
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
							function ScrollProc_divCompute() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopCompute");
								if (oText != null) oText.value = divCompute.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftCompute");
								if (oText != null) oText.value = divCompute.scrollLeft;
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
								oText=document.getElementById("htxtDivTopCompute");
								if (oText != null) divCompute.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftCompute");
								if (oText != null) divCompute.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divCompute.onscroll = ScrollProc_divCompute;
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

