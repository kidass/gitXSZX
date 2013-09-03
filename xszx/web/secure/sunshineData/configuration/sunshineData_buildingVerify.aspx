<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sunshineData_buildingVerify.aspx.vb" Inherits="Xydc.Platform.web.sunshineData_buildingVerify" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>楼盘匹配检查</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../filecss/styles01.css" type="text/css" rel="stylesheet">
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
		<script src="../../../scripts/transkey.js"></script>
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
	<BODY bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onresize="return window_onresize()" background="../../../images/oabk.gif">
		<form id="frmGWDM_JJCD" method="post" runat="server">
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
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLAddNew" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../../images/new.gif" border="0" width="16" height="16">增加</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLUpdate" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../../images/modify.ico" border="0" width="16" height="16">修改</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLDelete" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../../images/delete.gif" border="0" width="16" height="16">删除</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLRefresh" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../../images/refresh.ico" border="0" width="16" height="16">刷新数据</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLClose" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../../images/CLOSE.GIF" alt="返回上级" border="0" width="16" height="16">返回上级</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"></TD>
									<TD vAlign="middle" align="center" width="100"></TD>
									<TD vAlign="middle" align="center" width="100"></TD>
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
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD class="tips" align="left" colSpan="3"><asp:LinkButton id="lnkBlank" Runat="server" Width="0px" Height="5px"></asp:LinkButton></TD>
								</TR>
								<TR>
									<TD width="5"></TD>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											
											<TR>
												<TD>
													<DIV id="divObjects" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 300px; CLIP: rect(0px 300px 300px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 300px">
														<asp:datagrid id="grdObjects" runat="server" runat="server" Width="280px" CssClass="labelGrid" 
                                                            AllowPaging="false" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
                                                            PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
                                                            
                                                            <SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
                                                            <EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
                                                            <AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
                                                            <ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
                                                            <HeaderStyle CssClass="FixedHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
                                                            <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                                            <Columns>	
                                                                															
																<asp:ButtonColumn Visible="false"  DataTextField="C_ID" SortExpression="C_ID" HeaderText="C_ID" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="80px" DataTextField="C_XZQY" SortExpression="C_XZQY" HeaderText="区域" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn ItemStyle-Width="200px" DataTextField="C_XM_NAME" SortExpression="C_XM_NAME" HeaderText="项目名称" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtOBJECTSFixed" type="hidden" value="0" runat="server">
													</DIV>
												</TD>
											</TR>
											
											<TR>
												<TD height="3"></TD>
											</TR>
											<TR>
												<TD class="label" align="center" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" align="center" height="2"></TD>
														</TR>
														<TR>
															<TD class="label" align="center" height="20"><B>楼盘匹配</B></TD>
														</TR>
														<TR>
															<TD class="label" align="center">
																<TABLE cellSpacing="0" cellPadding="0" border="0">
																	<TR>
																		<TD class="label" align="center" colSpan="2" height="2"></TD>
																	</TR>
																	<TR>
																		<TD class="labelNotNull" align="right" width="40%">行政区域：</TD>
																		<TD class="label" align="left" width="60%"><asp:textbox id="txtRegion" Runat="server" Font-Size="12px" Font-Name="宋体" Columns="8" CssClass="textbox" ReadOnly="true"></asp:textbox><SPAN class="label" style="COLOR: red">*</SPAN></TD>
																	</TR>
																	<TR>
																		<TD class="label" align="center" colSpan="2" height="2"></TD>
																	</TR>
																	<TR>
																		<TD class="labelNotNull" align="right">项目名称：</TD>
																		<TD class="label" align="left"><asp:textbox id="txtProjectName" Runat="server" Font-Size="12px" Font-Name="宋体" Columns="24" CssClass="textbox" ReadOnly="true"></asp:textbox><SPAN class="label" style="COLOR: red">*</SPAN></TD>
																	</TR>
																	<TR>
																		<TD class="labelNotNull" align="right">楼盘名称：</TD>
																		<TD class="label" align="left"><asp:textbox id="txtBuildingName" Runat="server" Font-Size="12px" Font-Name="宋体" Columns="24" CssClass="textbox"></asp:textbox><SPAN class="label" style="COLOR: red">*</SPAN></TD>
																	</TR>																	
																	<TR>
																		<TD class="label" align="center" colSpan="2" height="2"></TD>
																	</TR>
																	<TR>
																		<TD class="label" align="center" colSpan="2">
																			<asp:button id="btnSave" Runat="server" Font-Size="12px" Font-Name="宋体" Width="96px" Height="24px" CssClass="button" Text="保存"></asp:button>&nbsp;&nbsp;
																			<asp:button id="btnCancel" Runat="server" Font-Size="12px" Font-Name="宋体" Width="96px" Height="24px" CssClass="button" Text="取消"></asp:button></TD>
																	</TR>
																	<TR>
																		<TD class="label" align="center" colSpan="2" height="2"></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<td style="width:5px;"></td>
									<td align="left" valign="top" style="height:100%" >
						                <TABLE cellSpacing="0" cellPadding="0" border="0">
						                <TR>
									            <TD>
										            <DIV id="divHOUSEMATCH" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 500px; CLIP: rect(0px 500px 400px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 400px">
											            <asp:datagrid id="grdHOUSEMATCH" runat="server" runat="server" Width="480px" CssClass="labelGrid" 
                                                            AllowPaging="True" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
                                                            PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
                                                            
                                                            <SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
                                                            <EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
                                                            <AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
                                                            <ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
                                                            <HeaderStyle CssClass="FixedHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
                                                            <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                                            <Columns>	
                                                            													
													            <asp:ButtonColumn Visible="false"  DataTextField="C_ID" SortExpression="C_ID" HeaderText="C_ID" CommandName="Select">
														            <HeaderStyle Width="0px"></HeaderStyle>
													            </asp:ButtonColumn>
													            <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="C_XZQY" SortExpression="C_XZQY" HeaderText="区域" CommandName="Select">
														            <HeaderStyle Width="80px"></HeaderStyle>
													            </asp:ButtonColumn>
													            <asp:ButtonColumn ItemStyle-Width="200px" DataTextField="C_XM_NAME" SortExpression="C_XM_NAME" HeaderText="项目名称" CommandName="Select">
														            <HeaderStyle Width="200px"></HeaderStyle>
													            </asp:ButtonColumn>
													            <asp:ButtonColumn ItemStyle-Width="200px" DataTextField="C_HOUSE" SortExpression="C_HOUSE" HeaderText="楼盘名称" CommandName="Select">
														            <HeaderStyle Width="200px"></HeaderStyle>
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