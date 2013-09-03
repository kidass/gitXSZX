<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customer_detail.aspx.vb" Inherits="Xydc.Platform.web.customer_detail" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>明细数据查询界面</title>
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
											                <td class="label"  align="right">&nbsp;日期&nbsp;</TD>
												            <td class="label" align="left"><asp:textbox onfocus="calendar()" id="txtStartDate" runat="server" CssClass="textbox" Columns="11"></asp:textbox>~<asp:textbox onfocus="calendar()" id="txtEndDate" runat="server" CssClass="textbox" Columns="11"></asp:textbox></TD>
											                <td class="label"  align="right" >&nbsp;&nbsp;项目名称&nbsp;&nbsp;</TD>
												            <td class="label" align="left" ><asp:textbox  id="txtMainHouse" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											               	<td class="label"  align="right" >&nbsp;&nbsp;建筑面积&nbsp;&nbsp;</TD>
												            <td class="label" align="left"><asp:textbox  id="txtBuildingAreaStart" runat="server" CssClass="textbox" Columns="10"></asp:textbox>~<asp:textbox  id="txtBuildingAreaEnd" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											                <td class="label"  align="right" >&nbsp;&nbsp;套内面积&nbsp;&nbsp;</TD>
												            <td class="label" align="left"><asp:textbox  id="txtFloorAreaStart" runat="server" CssClass="textbox" Columns="10"></asp:textbox>~<asp:textbox  id="txtFloorAreaEnd" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
														</tr>
														<tr>	       
														    <td colspan="2" align="center">
												                <table>
												                    <tr>														                    
												                        <td class="label"  align="right">区域</td>
												               	        <td align="left"><asp:DropDownList id="ddlRegion" Runat="server" CssClass="textbox" Columns="12">
                                                                            <asp:ListItem Value="0" Selected="True">选择区域</asp:ListItem>
                                                                            <asp:ListItem Value="白云">白云</asp:ListItem>
								                                            <asp:ListItem Value="南沙">南沙</asp:ListItem>
								                                            <asp:ListItem Value="天河">天河</asp:ListItem>
								                                            <asp:ListItem Value="越秀">越秀</asp:ListItem>
								                                            <asp:ListItem Value="荔湾">荔湾</asp:ListItem>
								                                            <asp:ListItem Value="萝岗">萝岗</asp:ListItem>
								                                            <asp:ListItem Value="海珠">海珠</asp:ListItem>
								                                            <asp:ListItem Value="番禺">番禺</asp:ListItem>
								                                            <asp:ListItem Value="黄埔">黄埔</asp:ListItem>
								                                            <asp:ListItem Value="花都">花都</asp:ListItem>
								                                            <asp:ListItem Value="增城">增城</asp:ListItem>
								                                            <asp:ListItem Value="从化">从化</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
													                    <td class="label"  align="right">类型</TD>
												                        <td align="left"><asp:DropDownList id="ddlHouseType" Runat="server" CssClass="textbox" Columns="12">
                                                                            <asp:ListItem Value="0" Selected="True">选择类型</asp:ListItem>
								                                            <asp:ListItem Value="1">商业</asp:ListItem>
								                                            <asp:ListItem Value="2">住宅</asp:ListItem>
								                                            <asp:ListItem Value="3">写字楼</asp:ListItem>
								                                            <asp:ListItem Value="4">车位</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>													                   
												                    </tr>
												                </table>
											                </td>
											                <td class="label"  align="right">户型&nbsp;&nbsp;</TD>
								                 	        <td align="left"><asp:DropDownList id="ddlRoomTypeCalc" Runat="server" CssClass="textbox" Columns="12">
                                                                <asp:ListItem Value="9" Selected="True">选择户型</asp:ListItem>
                                                                <asp:ListItem Value="1">1房</asp:ListItem>
			                                                    <asp:ListItem Value="2">2房</asp:ListItem>
			                                                    <asp:ListItem Value="3">3房</asp:ListItem>
			                                                    <asp:ListItem Value="4">4房</asp:ListItem>
			                                                    <asp:ListItem Value="5">5房以上</asp:ListItem>
			                                                    <asp:ListItem Value="100">别墅</asp:ListItem>
			                                                    <asp:ListItem Value="0">其他</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
											    			<TD class="label"  align="right">单价&nbsp;&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtUnitPriceStart" runat="server" CssClass="textbox" Columns="10"></asp:textbox>~<asp:textbox  id="txtUnitPriceEnd" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											                <TD class="label"  align="right">总价&nbsp;&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtTotalPriceStart" runat="server" CssClass="textbox" Columns="10"></asp:textbox>~<asp:textbox  id="txtTotalPriceEnd" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											            
														 </tr>	
														<tr> 
														    <TD class="label"  align="right">&nbsp;楼层&nbsp;</TD>
											                <TD class="label" align="left"><asp:textbox  id="txtFloor" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											                <TD class="label"  align="right">子楼盘&nbsp;&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtPartialHouse" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											                <TD class="label"  align="right">楼盘地址&nbsp;&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtHouseAddress" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											                <TD class="label"  align="right">总层数&nbsp;&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtTotalFloor" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											             </tr>
											             <tr>
											                <TD class="label"  align="right">&nbsp;房号&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtRoomNumber" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											                <TD class="label"  align="right">封闭阳台&nbsp;&nbsp;</TD>
											                <TD class="label" align="left"><asp:textbox  id="txtEnclosedPatio" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
											                <TD class="label"  align="right">卫生间&nbsp;&nbsp;</TD>
											                <TD class="label" align="left"><asp:textbox  id="txtWashroom" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
            											    <TD class="label"  align="right">非封闭阳台&nbsp;&nbsp;</TD>
												            <TD class="label" align="left"><asp:textbox  id="txtNotEnclosedPatio" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
										                    <td  align="left" rowspan="4" valign="top">
													            <asp:button id="btnSearch" Runat="server" CssClass="button" Text=" 查 询 "></asp:button>
													            <asp:button id="btnCancel" Runat="server" CssClass="button" Text=" 返 回 "></asp:button>
												            </td>
										                 </tr>									 												
														 <tr>
														    <td>
														    <div style="display:none">
														        <table >
														            <tr>
														                <td class="label"  align="right" valign="bottom" >&nbsp;排序&nbsp;</td>
														                <td class="label"  align="left" colspan="4">
																            <asp:RadioButtonList ID="rblTop" Runat="server" CssClass="textbox" RepeatColumns="8" RepeatDirection="Vertical" RepeatLayout="Flow">
																	            <asp:ListItem Value="0" Selected="True">项目名称</asp:ListItem>
																	            <asp:ListItem Value="1">建筑面积</asp:ListItem>
																	            <asp:ListItem Value="2">套内面积</asp:ListItem>
																	            <asp:ListItem Value="3">成交套数</asp:ListItem>
																	            <asp:ListItem Value="4">成交均价</asp:ListItem>
																	            <asp:ListItem Value="5">成交金额</asp:ListItem>
																            </asp:RadioButtonList>
											                            </td>
												                        <td class="label" align="left" > &nbsp;&nbsp;前&nbsp;&nbsp;<asp:textbox  id="txtTop" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
            											                
														                <TD class="label" align="left"><asp:textbox  id="txtRegion" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
														                 <TD class="label" align="left"><asp:textbox  id="txtHouseType" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
														                <TD class="label" align="left"><asp:textbox  id="txtRoomTypeCalc" runat="server" CssClass="textbox" Columns="10"></asp:textbox></TD>
																       
															        </tr>
														        </table>
														     </div>
														    </td>
														  </tr>
														  <tr>											               										               
														</tr>
													</TABLE>
												</TD>
													
											</TR>
											<TR>
												<TD>
													<DIV id="divCompute" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 964px; CLIP: rect(0px 964px 382px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 382px">
														<asp:datagrid id="grdCompute" runat="server" CssClass="labelGrid" Width="1400px"
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
																<asp:ButtonColumn ItemStyle-Width="120px" DataTextField="MainHouse" SortExpression="MainHouse" HeaderText="项目名称" CommandName="Select">
																	<HeaderStyle Width="120px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="PartialHouse" SortExpression="PartialHouse" HeaderText="子楼盘" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="Region" SortExpression="Region" HeaderText="行政区" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="HouseAddress" SortExpression="HouseAddress" HeaderText="楼盘地址" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="RoomNumber" SortExpression="RoomNumber" HeaderText="房号" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="HouseType" SortExpression="HouseType" HeaderText="楼盘性质" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>																	
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="Floor" SortExpression="Floor" HeaderText="楼层" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="RoomTypeCalc" SortExpression="RoomTypeCalc" HeaderText="户型" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="TotalFloor" SortExpression="TotalFloor" HeaderText="总层数" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="EnclosedPatio" SortExpression="EnclosedPatio" HeaderText="封闭阳台" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="NotEnclosedPatio" SortExpression="NotEnclosedPatio" HeaderText="非封闭阳台" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="Washroom" SortExpression="Washroom" HeaderText="卫生间" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>	
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="BuildingArea" SortExpression="BuildingArea" HeaderText="建筑面积" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="FloorArea" SortExpression="FloorArea" HeaderText="套内面积" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="UnitPrice" SortExpression="UnitPrice" HeaderText="单价" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="TotalPrice" SortExpression="TotalPrice" HeaderText="总价" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="FixtureDate" SortExpression="FixtureDate" HeaderText="成交日期" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>	
																
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="CustomerName" SortExpression="CustomerName" HeaderText="姓名" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="Sex" SortExpression="Sex" HeaderText="性别" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="birthdate" SortExpression="birthdate" HeaderText="出生年月" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="FixtureAge" SortExpression="FixtureAge" HeaderText="年龄" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="CertificateCode" SortExpression="CertificateCode" HeaderText="证件" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="MailAddress" SortExpression="MailAddress" HeaderText="地址" CommandName="OpenDocument">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="PhoneNumer" SortExpression="PhoneNumer" HeaderText="电话" CommandName="OpenDocument">
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
