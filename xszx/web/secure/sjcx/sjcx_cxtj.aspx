<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sjcx_cxtj.aspx.vb" Inherits="Xydc.Platform.web.sjcx_cxtj" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>通用查询条件输入窗</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<script src="../../scripts/transkey.js"></script>
		<style>
		    TD.grdTJLocked { ; LEFT: expression(divTJ.scrollLeft); POSITION: relative }
		    TH.grdTJLocked { ; LEFT: expression(divTJ.scrollLeft); POSITION: relative }
		    TH.grdTJLocked { Z-INDEX: 99 }
		    TH { Z-INDEX: 10; POSITION: relative }
		</style>
		<script language="javascript">
			function window_onresize() 
			{
				var dblHeight = 0;
				var dblWidth  = 0;
				var strHeight = "";
				var strWidth  = "";
				var dblDeltaY = 20;
				var dblDeltaX = 20;
				
				if (document.all("divTJ") == null)
					return;
				
				dblHeight = 274 + dblDeltaY + document.body.clientHeight - 570; //default state : 274px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				dblWidth  = 592 + dblDeltaX + document.body.clientWidth  - 850; //default state : 592px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				divTJ.style.width  = strWidth;
				divTJ.style.height = strHeight;
				divTJ.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";

				var objControl = null;
				objControl = document.getElementById("lstField");
				if (objControl)
				{
					dblHeight = 274 + dblDeltaY + 10 + document.body.clientHeight - 570; //default state : 274px
					strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
					strWidth = objControl.style.width;
					objControl.style.width  = strWidth;
					objControl.style.height = strHeight;
				}
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
		<form id="frmSJCX_CXTJ" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD colSpan="3" height="4"></TD>
					</TR>
					<TR>
						<TD width="4"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td colspan="4" height="4"></td>
								</tr>
								<TR vAlign="middle" height="28">
									<TD>&nbsp;&nbsp;</TD>
									<TD class="label" style="BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid;" noWrap align="right"><asp:LinkButton id="lnkBlank" Runat="server" Height="4px" Width="0px"></asp:LinkButton>左边括弧：</TD>
									<TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid;" align="left">&nbsp;&nbsp;<asp:TextBox id="txtZKHZ" Runat="server" CssClass="textbox" Columns="6" Font-Size="12px" Font-Names="宋体"></asp:TextBox>(个)</TD>
									<TD>&nbsp;&nbsp;</TD>
								</TR>
								<TR vAlign="middle" height="28">
									<TD>&nbsp;&nbsp;</TD>
									<TD class="label" style="BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid;" noWrap align="right">比较运算符：</TD>
									<TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid;" align="left">&nbsp;&nbsp;<asp:RadioButtonList id="rblBJF" Runat="server" CssClass="textbox" RepeatLayout="Flow" RepeatDirection="Horizontal" Font-Size="12px" Font-Names="宋体" AutoPostBack="True"></asp:RadioButtonList></TD>
									<TD>&nbsp;&nbsp;</TD>
								</TR>
								<TR vAlign="middle">
									<TD height="28">&nbsp;&nbsp;</TD>
									<TD class="label" style="BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid;" noWrap align="right" rowSpan="2">准备查询值：</TD>
									<TD class="label" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid;" align="left">&nbsp;&nbsp;值1(小值)<asp:TextBox id="txtVal1" Runat="server" CssClass="textbox" Columns="60" Font-Size="12px" Font-Names="宋体"></asp:TextBox></TD>
									<TD>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD height="28">&nbsp;&nbsp;</TD>
									<TD class="label" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid;" align="left">&nbsp;&nbsp;值2(大值)<asp:TextBox id="txtVal2" Runat="server" CssClass="textbox" Columns="60" Font-Size="12px" Font-Names="宋体"></asp:TextBox></TD>
									<TD>&nbsp;&nbsp;</TD>
								</TR>
								<TR vAlign="middle" height="28">
									<TD style="HEIGHT: 25px">&nbsp;&nbsp;</TD>
									<TD class="label" style="BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; HEIGHT: 25px" noWrap align="right">右边括弧：</TD>
									<TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; HEIGHT: 25px" align="left">&nbsp;&nbsp;<asp:TextBox id="txtYKHZ" Runat="server" CssClass="textbox" Columns="6" Font-Size="12px" Font-Names="宋体"></asp:TextBox>(个)</TD>
									<TD style="HEIGHT: 25px">&nbsp;&nbsp;</TD>
								</TR>
								<TR vAlign="middle" height="28">
									<TD>&nbsp;&nbsp;</TD>
									<TD class="label" style="BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid" noWrap align="right">条件连接符：</TD>
									<TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid" align="left">&nbsp;&nbsp;<asp:RadioButtonList id="rblLJF" Runat="server" CssClass="textbox" RepeatLayout="Flow" RepeatDirection="Horizontal" Font-Size="12px" Font-Names="宋体"></asp:RadioButtonList></TD>
									<TD>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD>&nbsp;&nbsp;</TD>
									<TD colSpan="2" align="center" valign="top" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD class="title" noWrap align="left" height="32">查询字段列表</TD>
												<TD>&nbsp;</TD>
												<TD class="title" noWrap align="left" height="32">已定义的查询条件列表</TD>
											</TR>
											<TR>
												<TD vAlign="top" align="left"><asp:ListBox id="lstField" Runat="server" Width="200px" Height="274px" CssClass="textbox" AutoPostBack="True" Font-Name="宋体" Font-Size="12px"></asp:ListBox></TD>
												<TD>&nbsp;</TD>
												<TD valign="top" align="left">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD vAlign="top" align="left">
																<DIV id="divTJ" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 592px; CLIP: rect(0px 592px 274px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 274px">
																	<asp:datagrid id="grdTJ" runat="server" CssClass="label" BorderColor="#DEDFDE" BorderWidth="0px"
																		AllowSorting="False" PageSize="30" AllowPaging="false" CellPadding="4" BorderStyle="None"
																		BackColor="White" GridLines="Vertical" AutoGenerateColumns="False" UseAccessibleHeader="True"
																		Font-Size="12px" Font-Names="宋体">
																		<FooterStyle BackColor="#CCCC99"></FooterStyle>
																		<SelectedItemStyle Font-Size="12px" Font-Names="宋体" Font-Bold="False" VerticalAlign="Middle" ForeColor="#CC0000" BackColor="#FFFFDD"></SelectedItemStyle>
																		<EditItemStyle Font-Size="12px" Font-Names="宋体" VerticalAlign="Middle" BackColor="#FFCC00"></EditItemStyle>
																		<AlternatingItemStyle Font-Size="12px" Font-Names="宋体" VerticalAlign="Middle" BackColor="White"></AlternatingItemStyle>
																		<ItemStyle Font-Size="12px" Font-Names="宋体" BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
																		<HeaderStyle Font-Size="12px" Font-Names="宋体" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" BackColor="#87cefa" HorizontalAlign="Left"></HeaderStyle>
																		<Columns>
																			<asp:TemplateColumn HeaderText="选">
																				<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
																				<ItemTemplate>
																					<asp:CheckBox id="chkTJ" runat="server" AutoPostBack="False"></asp:CheckBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:ButtonColumn DataTextField="左括弧名" SortExpression="左括弧名" HeaderText="(" CommandName="Select">
																				<HeaderStyle Width="40px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="左括弧值" SortExpression="左括弧值" HeaderText="左括弧值" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="字段名" SortExpression="字段名" HeaderText="字段名" CommandName="Select">
																				<HeaderStyle Width="320px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="比较符名" SortExpression="比较符名" HeaderText="比较符" CommandName="Select">
																				<HeaderStyle Width="80px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="比较符值" SortExpression="比较符值" HeaderText="比较符值" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="值1" SortExpression="值1" HeaderText="值1" CommandName="Select">
																				<HeaderStyle Width="160px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="值2" SortExpression="值2" HeaderText="值2" CommandName="Select">
																				<HeaderStyle Width="160px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="右括弧名" SortExpression="右括弧名" HeaderText=")" CommandName="Select">
																				<HeaderStyle Width="40px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="右括弧值" SortExpression="右括弧值" HeaderText="右括弧值" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="连接符名" SortExpression="连接符名" HeaderText="连接符" CommandName="Select">
																				<HeaderStyle Width="80px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="连接符值" SortExpression="连接符值" HeaderText="连接符值" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="字段类型" SortExpression="字段类型" HeaderText="字段类型" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																		</Columns>
																		<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
																	</asp:datagrid><INPUT id="htxtTJFixed" type="hidden" value="1" name="htxtTJFixed" runat="server"></DIV>
															</TD>
														</TR>
														<tr>
															<td height="4"></td>
														</tr>
														<TR>
															<TD vAlign="middle" align="center">
																<asp:Button id="btnAddNew" Runat="server" CssClass="button" Text=" 增加条件 " Height="24px"></asp:Button>
																<asp:Button id="btnModify" Runat="server" CssClass="button" Text=" 修改条件 " Height="24px"></asp:Button>
																<asp:Button id="btnDelete" Runat="server" CssClass="button" Text=" 删除条件 " Height="24px"></asp:Button>
																<asp:Button id="btnDelAll" Runat="server" CssClass="button" Text=" 清除条件 " Height="24px"></asp:Button>
															</TD>
														</TR>
														<tr>
															<td height="4"></td>
														</tr>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD>&nbsp;&nbsp;</TD>
								</TR>
								<tr>
									<td colspan="4" height="4"></td>
								</tr>
							</TABLE>
						</TD>
						<TD width="4"></TD>
					</TR>
					<TR>
						<TD width="4"></TD>
						<TD class="label" align="center" height="2"></TD>
						<TD width="4"></TD>
					</TR>
					<TR>
						<TD width="4"></TD>
						<TD class="label" vAlign="middle" align="center">
							<asp:Button id="btnOK" Runat="server" Height="28px" CssClass="button" Text=" 确  定 "></asp:Button>
							<asp:Button id="btnCancel" Runat="server" Height="28px" CssClass="button" Text=" 取  消 "></asp:Button>
						<TD width="4"></TD>
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
				<TR>
					<TD>
						<input id="htxtSessionIDTJ" type="hidden" runat="server">
						<input id="htxtTJSort" type="hidden" runat="server">
						<input id="htxtTJSortColumnIndex" type="hidden" runat="server">
						<input id="htxtTJSortType" type="hidden" runat="server">
						<input id="htxtDivLeftTJ" type="hidden" runat="server">
						<input id="htxtDivTopTJ" type="hidden" runat="server">
						<input id="htxtDivLeftBody" type="hidden" runat="server">
						<input id="htxtDivTopBody" type="hidden" runat="server">
					</TD>
				</TR>
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
							function ScrollProc_divTJ() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopTJ");
								if (oText != null) oText.value = divTJ.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftTJ");
								if (oText != null) oText.value = divTJ.scrollLeft;
								return;
							}
							try {
								var oText;

								oText=null;
								oText=document.getElementById("htxtDivTopBody");
								if (oText != null) document.body.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftBody");
								if (oText != null) document.body.scrollLeft = oText.value;

								oText=null;
								oText=document.getElementById("htxtDivTopTJ");
								if (oText != null) divTJ.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftTJ");
								if (oText != null) divTJ.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divTJ.onscroll = ScrollProc_divTJ;
							}
							catch (e) {}
						</script>
					</td>
				</tr>
				<TR>
					<TD>
						<script language="javascript">window_onresize();</script>
						<uwin:popmessage id="popMessageObject" runat="server" Visible="False" EnableViewState="False" PopupWindowType="Normal" ActionType="OpenWindow" height="48px" width="96px"></uwin:popmessage>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
