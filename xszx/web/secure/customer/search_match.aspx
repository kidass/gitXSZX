<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="search_match.aspx.vb" Inherits="Xydc.Platform.web.search_match" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>通信地址查询匹配</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdObjectsLocked { ; LEFT: expression(divObjects.scrollLeft); POSITION: relative }
			TH.grdObjectsLocked { ; LEFT: expression(divObjects.scrollLeft); POSITION: relative }
			TH { Z-INDEX: 10; POSITION: relative }
			TH.grdObjectsLocked { Z-INDEX: 99 }
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
				var strHeight = "";
				var dblDeltaY = 30;
				
				if (document.all("divObjects") == null)
					return;
				
				dblHeight = 300 + dblDeltaY + document.body.clientHeight - 570; //default state : 300px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				//divObjects.style.width  = "100%";
				//divObjects.style.height = strHeight;
				//divObjects.style.clip = "rect(0px " + strHeight + " 0px)";
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
	<BODY bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onresize="return window_onresize()" background="../../images/oabk.gif">
		<form id="frmGWDM_JJCD" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">				  				
					<TR >
					     <td  colspan="2" style="BORDER-BOTTOM: #99cccc 2px solid">
					        <table  width="100%">
					            <tr>
						            <td class="title" align="center" style="width:95%" height="30">通信地址查询匹配<asp:LinkButton id="lnkBlank" Runat="server" Width="0px"></asp:LinkButton></td>
					                 <td align="right"  colSpan="2" height="30"><asp:button id="btnCancel" Runat="server" CssClass="button" Text=" 返 回 "></asp:button></td> 
					            </tr>
					        </table>
					    </td>				
					</TR>					
					<TR>
						<TD width="5"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								
								<TR>
									<TD width="5"></TD>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<tr>
											    <td>
											        <table>
											            <tr>
											                <td class="label"  align="right">成交日期：</TD>
												            <td class="label" align="left"><asp:textbox onfocus="calendar()" id="txtStartDate" runat="server" CssClass="textbox" Columns="11"></asp:textbox>~<asp:textbox onfocus="calendar()" id="txtEndDate" runat="server" CssClass="textbox" Columns="11"></asp:textbox></TD>
											                <td class="label"  align="right">通信区域：</td>
									               	        <td align="left"><asp:DropDownList id="ddlRegion" Runat="server" CssClass="textbox" Columns="12"  Width="85px">
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
											            </tr>											            	
														<tr>
														    <td class="label"  colspan="4" align="left" valign="middle">通信地址：                            
                                                                <asp:TextBox ID="txtMailAddress" Width="290" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="12" CssClass="textbox"></asp:TextBox>                                                                         
                                                                <asp:LinkButton ID="lnkSearchSort" runat="server" Font-Name="宋体" Font-Size="12px">查找</asp:LinkButton>
                                                            </td>  
														</tr>														
											        </table>
											    </td>						                        
						                    </tr>
											<TR>
												<TD>
													<DIV id="divObjects" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 430px; CLIP: rect(0px 430px 400px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 400px">
														<asp:datagrid id="grdObjects" runat="server"  Width="500px" CssClass="labelGrid" 
                                                            AllowPaging="true" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
                                                            PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
                                                            
                                                            <SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
                                                            <EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
                                                            <AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
                                                            <ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
                                                            <HeaderStyle   Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
                                                            <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                                            <Columns>	
                                                                															
																<asp:ButtonColumn ItemStyle-Width="50px"  DataTextField="SalesMessageID" SortExpression="SalesMessageID" HeaderText="ID" CommandName="Select">
																	<HeaderStyle Width="50px"></HeaderStyle>
																</asp:ButtonColumn>																
																<asp:ButtonColumn ItemStyle-Width="100px" DataTextField="FixtureDate" SortExpression="FixtureDate" HeaderText="成交日期" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="200px" DataTextField="MailAddress" SortExpression="MailAddress" HeaderText="通信地址" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="100px" DataTextField="MailRegion" SortExpression="MailRegion" HeaderText="来源区域" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtOBJECTSFixed" type="hidden" value="0" runat="server">
													</DIV>
												</TD>
											</TR>
											<TR>
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkGotoPage" runat="server"  CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtMailPageIndex" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtMailPageSize" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right" width="200"><asp:label id="lblMailGridLocInfo" runat="server" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>		
											<TR>
												<TD height="3"></TD>
											</TR>											
										</TABLE>
									</TD>
									<td style="width:5px;"></td>
									<td align="left" valign="top" style="height:100%" >
						                <table cellspacing="0" cellpadding="0" border="0">
						                <tr>
						                    <td>
						                        <table>
						                            <tr>
						                                <td class="label"  colspan="2" align="left" valign="middle">匹配内容：                            
                                                            <asp:TextBox ID="txtSearchContent" Width="200" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="12" CssClass="textbox"></asp:TextBox>                                                                         
                                                            <asp:LinkButton ID="LnkMLSeek" runat="server" Font-Name="宋体" Font-Size="12px">查找</asp:LinkButton>
                                                        </td>  
						                            </tr>
						                            <tr>
						                                <td class="label"  colspan="2" align="left" valign="middle">匹配区域：                            
                                                            <asp:DropDownList id="ddlMatchRegion" Runat="server" CssClass="textbox" Columns="12"  Width="85px">
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
                                                            <asp:LinkButton ID="lnkSingleMatch" runat="server" Font-Name="宋体" Font-Size="12px">单个匹配</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkMultiMatch" runat="server" Font-Name="宋体" Font-Size="12px">批量匹配</asp:LinkButton>
                                                        </td> 
						                            </tr>
						                        </table>
						                    </td>
						                </tr>
						                
						                <TR>
									            <TD>
										            <DIV id="divHOUSEMATCH" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 460px; CLIP: rect(0px 460px 400px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 400px">
											            <asp:datagrid id="grdHOUSEMATCH" runat="server"  Width="450px" CssClass="labelGrid" 
                                                            AllowPaging="True" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
                                                            PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
                                                            <SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
                                                            <EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
                                                            <AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
                                                            <ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
                                                            <HeaderStyle CssClass="fixeHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
                                                            <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                                            <Columns>	                                                            	
													            <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="C_SearchContent" SortExpression="C_SearchContent" HeaderText="查找内容" CommandName="Select">
														            <HeaderStyle Width="80px"></HeaderStyle>
													            </asp:ButtonColumn>
													            <asp:ButtonColumn ItemStyle-Width="100px" DataTextField="C_Region" SortExpression="C_Region" HeaderText="对应区域" CommandName="Select">
														            <HeaderStyle Width="100px"></HeaderStyle>
													            </asp:ButtonColumn>
													            <asp:ButtonColumn ItemStyle-Width="120px" DataTextField="C_SourceContent" SortExpression="C_SourceContent" HeaderText="对应内容" CommandName="Select">
														            <HeaderStyle Width="120px"></HeaderStyle>
													            </asp:ButtonColumn>
													            <asp:TemplateColumn HeaderText="匹配" HeaderStyle-ForeColor="White">
																	<HeaderStyle HorizontalAlign="left" Width="150px" ForeColor="White" Font-Size="14px"></HeaderStyle>
																	<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
																	<ItemTemplate>
																		 <asp:LinkButton ID="dataSingleMatch" runat="server" Font-Name="宋体" Font-Size="12px" CommandName="lnkSingleMatch">单个匹配</asp:LinkButton>
                                                                           <asp:LinkButton ID="dataMultiMatch" runat="server" Font-Name="宋体" Font-Size="12px" CommandName="lnkMultiMatch">批量匹配</asp:LinkButton>
																	</ItemTemplate>
																</asp:TemplateColumn>
													            <asp:ButtonColumn Visible="false"  DataTextField="C_SourceTable" SortExpression="C_SourceTable" HeaderText="C_SourceTable" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>		
												            </Columns>
												            <PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
											            </asp:datagrid><INPUT id="Hidden1" type="hidden" value="0" runat="server">
										            </DIV>
									            </TD>
								            </TR>
								            <TR align="center">
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR align="center">
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZGotoPage" runat="server"  CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtPageIndex" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="2" CssClass="textbox">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtPageSize" runat="server" Font-Size="12px" Font-Name="宋体" Columns="3" CssClass="textbox">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right" ><asp:label id="lblGridLocInfo" runat="server" Font-Size="12px" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
						                </TABLE> 
						            </td>
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
						<input id="htxtCurrentPage" type="hidden" runat="server">
						<input id="htxtCurrentRow" type="hidden" runat="server">
						<input id="htxtEditMode" type="hidden" runat="server">
						<input id="htxtEditType" type="hidden" runat="server">
						<input id="htxtQuery" type="hidden" runat="server">
						<input id="htxtRows" type="hidden" runat="server">
						<input id="htxtSort" type="hidden" runat="server">
						<input id="htxtSortColumnIndex" type="hidden" runat="server">
						<input id="htxtSortType" type="hidden" runat="server">
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
							function ScrollProc_divObjects() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopObject");
								if (oText != null) oText.value = divObjects.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) oText.value = divObjects.scrollLeft;
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
								if (oText != null) divObjects.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) divObjects.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divObjects.onscroll = ScrollProc_divObjects;
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
	</BODY>
</HTML>
