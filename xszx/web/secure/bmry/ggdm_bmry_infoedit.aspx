<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ggdm_bmry_infoedit.aspx.vb" Inherits="Xydc.Platform.web.ggdm_bmry_infoedit" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>人员信息处理窗</TITLE>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">		
		<script src="../../scripts/transkey.js"></script>	
		<link href="../../filecss/StylePerson.css" type="text/css" rel="stylesheet">		
		<style>
			TD.grdRYLocked { ; LEFT: expression(divTASK.scrollLeft); POSITION: relative }
			TH.grdRYLocked { ; LEFT: expression(divTASK.scrollLeft); POSITION: relative }
			TH.grdRYLocked { Z-INDEX: 99 }
			TH { Z-INDEX: 10; POSITION: relative }
        </style>        
        <script language="javascript">
			function doRYXX()
			{
				__doPostBack("lnkRYXX");				
			}
			
			function doXGMM()
			{
				__doPostBack("lnkXGMM");				
			}
			
			function doSQBS()
			{
				__doPostBack("lnkSQBS");				
			}
			function doJSGL()
			{
				__doPostBack("lnkJSGL");				
			}
			function doCYFW()
			{
				__doPostBack("lnkCYFW");				
			}
			function doFHSJ()
			{
				__doPostBack("lnkFHSJ");				
			}			
			function window_onresize() 
			{
				var dblHeight = 0;
				var dblWidth  = 0;
				var strHeight = "";
				var strWidth  = "";
				var dblDeltaY = 40;
				var dblDeltaX = 0;
				var proEdit = 0 ;
				
				dblHeight = 450 + dblDeltaY + document.body.clientHeight - 570; //default state : 450px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				dblWidth  = 800 + dblDeltaX + document.body.clientWidth  - 1050; //default state : 800px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				
				contentRight1.style.width  = strWidth;
				contentRight1.style.height = strHeight;
				contentRight1.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";				
			
				contentRight2.style.width  = strWidth;
				contentRight2.style.height = strHeight;
				contentRight2.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
				
				contentRight3.style.width  = strWidth;
				contentRight3.style.height = strHeight;
				contentRight3.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
				
				contentRight4.style.width  = strWidth;
				contentRight4.style.height = strHeight;
				contentRight4.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
				
				contentRight5.style.width  = strWidth;
				contentRight5.style.height = strHeight;
				contentRight5.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
				
			}
			function document_onreadystatechange() 
			{
				return window_onresize();
			}
		
		</script>
		<script language="javascript" for="document" event="onreadystatechange">		
			return document_onreadystatechange()		
		</script>		
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onresize="return window_onresize()"	background="../../images/oabk.gif">
		<form id="frmGGDM_BMRY_RYXX" method="post" runat="server">
			<asp:Panel ID="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD height="8px"></TD>
					</TR>
					<TR>
						<td>							
							<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
								<tr>
									<TD vAlign="top" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid" width="200px">
										<DIV id="contentLeft">
											<TABLE cellSpacing="1" cellPadding="5" width="100%" height="100%" border="0">
												<TR>
													<TD class="labelTitle">人员信息&nbsp;</TD>
												</TR>
												<TR>
													<TD  class="labelInfo" valign="middle"><asp:LinkButton id="lnkRYXX" Runat="server" Width="0px"></asp:LinkButton><A id="doRYXX" href="javascript:doRYXX()">人员资料</a></TD>										
												</TR>
												<TR>
													<TD class="labelInfo"><asp:LinkButton id="lnkXGMM"  Runat="server" Width="0px"></asp:LinkButton><A id="doXGMM"  href="javascript:doXGMM()">修改密码</a></TD>
												</TR>
												<TR>
													<TD class="labelInfo"><asp:LinkButton id="lnkSQBS" Runat="server" Width="0px"></asp:LinkButton><A  id="doSQBS" href="javascript:doSQBS()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请标识[SA]</a></TD>
												</TR>
												<TR>
													<TD class="labelInfo"><asp:LinkButton id="lnkJSGL" Runat="server" Width="0px"></asp:LinkButton><A id="doJSGL" href="javascript:doJSGL()">角色管理</a></TD>
												</TR>
												<TR>
													<TD class="labelInfo"><asp:LinkButton id="lnkCYFW" Runat="server" Width="0px"></asp:LinkButton><A id="doCYFW" href="javascript:doCYFW()">常用范围</a></TD>
												</TR>
												<TR>
													<TD class="labelInfo"><asp:LinkButton id="lnkFHSJ" Runat="server" Width="0px"></asp:LinkButton><A id="doFHSJ" href="javascript:doFHSJ()">返回上级</a></TD>
												</TR>
												<TR>
													<TD height="450px"></TD>
												</TR>
											</TABLE>
										</DIV>
									</TD> 						
									<TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid" vAlign="top">							
										<% 
											if propEditMode = 1 then
												response.write("<DIV id='contentRight1'>" + vbcr)
											else
												response.write("<DIV id='contentRight1' style='display:none'>" + vbcr)
											end if
										%>
											<TABLE cellSpacing="1" cellPadding="5" width="100%" border="0">
												<TR>
													<TD class="typeTitle" align="left"  background="../../images/bg_title.gif" height="18px"><img src="../../images/titleicon.gif">&nbsp;个人信息</TD>
												</TR>	
												<TR vAlign="middle" align="center">
													<TD class="tips" align="center" colSpan="2" height="30">&nbsp;&nbsp;输入框旁带红色*号的内容必须输入，输入完成后按[确定]保存并返回。</TD>
												</TR>								
												<tr>
													<td align="center">
														<TABLE cellSpacing="0" cellPadding="0" border="0">
															<TR vAlign="middle">
																<TD class="labelNotNull" align="right" nowrap>人员代码：</TD>
																<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtRYDM" runat="server" Height="24px" CssClass="textbox" Columns="16" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox><FONT color="#ff0000">*</FONT></SPAN></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="labelNotNull" align="right" nowrap>人员名称：</TD>
																<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtRYMC" runat="server" Height="24px" CssClass="textbox" Columns="16" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox><FONT color="#ff0000">*</FONT></SPAN></TD>
															</TR>
															
															<tr>
																<TD class="labelNotNull" align="right" nowrap>人员真名：</TD>
																<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtRYZM" runat="server" Height="24px" CssClass="textbox" Columns="16" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox><FONT color="#ff0000">*</FONT></SPAN></TD>
															</tr>
															
															<TR vAlign="middle">
																<TD class="labelNotNull" align="right" nowrap>所在单位：</TD>
																<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtZZMC" runat="server" Height="24px" CssClass="textbox" Columns="48" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="True"></asp:textbox><FONT color="#ff0000">*</FONT></SPAN><asp:Button id="btnSelectZZDM" Runat="server" Font-Size="12px" Font-Name="宋体" Text=" … "></asp:Button><INPUT id="htxtZZDM" type="hidden" runat="server" NAME="htxtZZDM"></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="labelNotNull" align="right" nowrap>所在单位内排序号：</TD>
																<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtRYXH" runat="server" Height="24px" CssClass="textbox" Columns="4" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox><FONT color="#ff0000">*</FONT></SPAN></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>行政级别：</TD>
																<TD class="label" align="left"><asp:textbox id="txtJBMC" runat="server" Height="24px" CssClass="textbox" Columns="16" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="True"></asp:textbox><asp:Button id="btnSelectJBDM" Runat="server" Font-Size="12px" Font-Name="宋体" Text=" … "></asp:Button><INPUT id="htxtJBDM" type="hidden" name="htxtJBDM" runat="server"></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>配备秘书：</TD>
																<TD class="label" align="left"><asp:textbox id="txtMSMC" runat="server" Height="24px" CssClass="textbox" Columns="16" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="True"></asp:textbox><asp:Button id="btnSelectMSDM" Runat="server" Font-Size="12px" Font-Name="宋体" Text=" … "></asp:Button><INPUT id="htxtMSDM" type="hidden" name="htxtMSDM" runat="server"></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>联系电话：</TD>
																<TD class="label" align="left"><asp:textbox id="txtLXDH" runat="server" Height="24px" CssClass="textbox" Columns="50" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>移动电话：</TD>
																<TD class="label" align="left"><asp:textbox id="txtSJHM" runat="server" Height="24px" CssClass="textbox" Columns="30" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>内部邮箱：</TD>
																<TD class="label" align="left"><asp:textbox id="txtFTPDZ" runat="server" Height="24px" CssClass="textbox" Columns="50" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>因特网邮箱：</TD>
																<TD class="label" align="left"><asp:textbox id="txtYXDZ" runat="server" Height="24px" CssClass="textbox" Columns="50" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right"></TD>
																<TD class="label" align="left"><asp:CheckBox id="chkZDQS" Runat="server" Font-Size="12px" Font-Name="宋体" Text="别人送来的文件系统自动签收"></asp:CheckBox></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>文件交接时可直接送给您的<br>人员、单位、范围：</TD>
																<TD class="label" vAlign="top" align="left"><asp:textbox id="txtKZSRY" runat="server" Height="60px" CssClass="textbox" Columns="48" Font-Names="宋体" Font-Size="12px" TextMode="MultiLine" ReadOnly="False"></asp:textbox><asp:Button id="btnSelectKZSRY" Runat="server" Font-Size="12px" Font-Name="宋体" Text=" … "></asp:Button></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>其他人员须由：</TD>
																<TD class="label" align="left"><asp:textbox id="txtQTYZS" runat="server" Height="24px" CssClass="textbox" Columns="16" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="True"></asp:textbox><asp:Button id="btnSelectQTYZS" Runat="server" Font-Size="12px" Font-Name="宋体" Text=" … "></asp:Button>转达<INPUT id="htxtQTYZS" type="hidden" runat="server" NAME="htxtQTYZS"></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>查看文件交接信息时能看见<br>您的真实名称的：</TD>
																<TD class="label" vAlign="top" align="left"><asp:textbox id="txtKCKXM" runat="server" Height="60px" CssClass="textbox" Columns="48" Font-Names="宋体" Font-Size="12px" TextMode="MultiLine" ReadOnly="False"></asp:textbox><asp:Button id="btnSelectKCKXM" Runat="server" Font-Size="12px" Font-Name="宋体" Text=" … "></asp:Button></TD>
															</TR>
															<TR vAlign="middle">
																<TD class="label" align="right" nowrap>其他人则显示为：</TD>
																<TD class="label" align="left"><asp:textbox id="txtJJXSMC" runat="server" Height="24px" CssClass="textbox" Columns="50" Font-Names="宋体" Font-Size="12px" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
															</TR>												
														</table>
													</td>
												</tr>
												<TR vAlign="middle" align="center">
													<TD  height="3"></TD>
												</TR>
												<TR vAlign="middle" align="center">
													<TD class="label">
														<TABLE cellSpacing="0" cellPadding="0" border="0" width="500px">
															<TR>
																<TD height="3"></TD>
															</TR>
															<TR>
																<TD class="label" align="center"><B>担任职务情况一览表</B></TD>
															</TR>
															<TR>
																<TD height="3"></TD>
															</TR>
															<TR>
																<TD class="squre"  align="left"><asp:CheckBoxList id="cblDRZW" Runat="server" CssClass="label" RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%"></asp:CheckBoxList></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR >
													<TD class="label" colSpan="2" vAlign="middle" align="center">
														<TABLE cellSpacing="0" cellPadding="0" border="0" width="98%">
															<TR>
																<TD height="3"></TD>
															</TR>
															<TR>
																<TD class="label" align="center"><B>任职情况一览表</B></TD>
															</TR>
															<TR>
																<TD height="3"></TD>
															</TR>
															<TR>
																<TD vAlign="middle" align="center">
																	<DIV id="divTASK" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 560px; CLIP: rect(0px 560px 136px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 136px;">
																	<asp:datagrid id="grdRY" runat="server" CssClass="label"  AllowPaging="False"
																		AutoGenerateColumns="False" GridLines="Vertical" BackColor="White" BorderStyle="None" PageSize="30"
																		BorderColor="#DEDFDE" BorderWidth="0px" AllowSorting="True" CellPadding="4" UseAccessibleHeader="True">
																	<SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
																		<EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
																		<AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
																		<ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
																		<HeaderStyle  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
																		<FooterStyle BackColor="#CCCC99"></FooterStyle>
																		<Columns>
																			<asp:ButtonColumn Visible="False" DataTextField="编号" SortExpression="编号" HeaderText="编号" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="人员代码" SortExpression="人员代码" HeaderText="人员代码" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="人员名称" SortExpression="人员名称" HeaderText="人员名称" CommandName="Select">
																				<HeaderStyle Width="100px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn  DataTextField="人员序号" SortExpression="人员序号" HeaderText="排序号" CommandName="Select">
																				<HeaderStyle Width="100px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn DataTextField="组织名称" SortExpression="组织名称" HeaderText="所在单位" CommandName="Select">
																				<HeaderStyle Width="100px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn  DataTextField="级别名称" SortExpression="级别名称" HeaderText="行政级别" CommandName="Select">
																				<HeaderStyle Width="100px"></HeaderStyle>
																			</asp:ButtonColumn>                                                                
																			<asp:ButtonColumn Visible="False" DataTextField="组织代码" SortExpression="组织代码" HeaderText="组织代码" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>                                                                
																			<asp:ButtonColumn Visible="False" DataTextField="级别代码" SortExpression="级别代码" HeaderText="级别代码" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																		</Columns>
																		<PagerStyle Visible="False" NextPageText="下页"  PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
																	</asp:datagrid><INPUT id="htxtTASKFixed" type="hidden" value="0" runat="server" NAME="htxtTASKFixed"></DIV>
																</TD>
															</TR>
														</TABLE>
													</TD>
												</TR>									
												<TR vAlign="middle" align="center">
													<TD class="label"  height="3"></TD>
												</TR>
												<TR vAlign="middle">
													<TD align="center"><asp:button id="btnOK" Runat="server" Width="94px" Height="36" CssClass="label" Text=" 修    改 "></asp:Button></TD>
												</TR>											
											</TABLE>
										<% response.write("</DIV>" + vbcr) %>
										<% 
											if propEditMode = 2 then
												response.write("<DIV id='contentRight2' style='display:'>" + vbcr)
											else
												response.write("<DIV id='contentRight2' style='display:none'>" + vbcr)
											end if
										%>
										<TABLE cellSpacing="1" cellPadding="5" width="100%" border="0">
											<TR>
												<TD  class="typeTitle" align="left"  background="../../images/bg_title.gif" height="18px"><img src="../../images/titleicon.gif">&nbsp;修改密码</TD>
											</TR>
											<TR>
												<TD align="center">
													<TABLE  cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" align="left" colSpan="2" height="30"></TD>
														</TR>
														<TR>
															<TD class="label" align="right" height="24">&nbsp;&nbsp;&nbsp;&nbsp;用户标识：</TD>
															<TD class="label" align="left" height="24"><INPUT id="txtUserId" class="label" type="text" size="26" runat="server" NAME="txtUserId">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
														</TR>
														<TR>
															<TD class="label" colSpan="2" height="20"></TD>
														</TR>
														<TR>
															<TD class="label" align="right" height="24">&nbsp;&nbsp;&nbsp;&nbsp;输入新密码：</TD>
															<TD class="label" height="24"><INPUT id="txtNewUserPwd" class="label" type="password" size="28" runat="server" NAME="txtNewUserPwd">&nbsp;(最少<%=Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength%>个字符)&nbsp;</TD>
														</TR>
														<TR>
															<TD class="label" colSpan="2" height="20"></TD>
														</TR>
														<TR>
															<TD class="label" align="right" height="24">&nbsp;&nbsp;&nbsp;&nbsp;确认新密码：</TD>
															<TD class="label" height="24"><INPUT id="txtNewUserPwdQR" class="label" type="password" size="28" runat="server" NAME="txtNewUserPwdQR">&nbsp;(最少<%=Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength%>个字符)&nbsp;</TD>
														</TR>
														<TR>
															<TD class="label" colSpan="2" height="20"></TD>
														</TR>
														<TR>
															<TD align="center" colSpan="2" height="24"><INPUT language="javascript"  id="btnModify"  class="label" type="button" value=" 修  改 " runat="server" NAME="btnModify">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="btnReset" class="label" type="reset"  value=" 重  设 " runat="server" NAME="btnReset">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="btnPasswordCancel" class="label" type="button" value=" 取  消 " runat="server" NAME="btnPasswordCancel"></TD>
														</TR>
														
													</TABLE>
												</TD>
											</TR>
										</TABLE>
										<% response.write("</DIV>" + vbcr) %>	
										<% 
											if propEditMode = 3 then
												response.write("<DIV id='contentRight3' style='display:'>" + vbcr)
											else
												response.write("<DIV id='contentRight3' style='display:none'>" + vbcr)
											end if
										%>
										<TABLE cellSpacing="1" cellPadding="5" width="100%" border="0">
											<TR>
												<TD class="typeTitle" align="left"  background="../../images/bg_title.gif" height="18px"><img src="../../images/titleicon.gif">&nbsp;申请标识</TD>
											</TR>
											<TR>
												<TD align="center">
													<TABLE  cellSpacing="0" cellPadding="0" border="0">	
														<tr>
															<TD class="label" align="left" colSpan="2" height="50px"></td>
														</tr>
														<TR>
															<TD class="labelNotNull" align="center" colSpan="2" height="30">
															<% if propBlnBS = true then 
																	response.write("标识已经申请！")
																else
																	response.write("标识未申请,请点击申请ID！")
																end if 
															%></TD>
														</TR>
														<tr>
															<TD align="center" height="24"><INPUT language="javascript" id="btnApplyID" class="label" type="button" value=" 申请标识 " runat="server" NAME="btnApplyID">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="btnDropID" class="label" type="button"  value=" 删除标识 " runat="server" NAME="btnDropID"></TD>
														</tr>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
										<% response.write("</DIV>" + vbcr) %>
										<% 
											if propEditMode = 4 then
												response.write("<DIV id='contentRight4' style='display:'>" + vbcr)
											else
												response.write("<DIV id='contentRight4' style='display:none'>" + vbcr)
											end if
										%>
										<TABLE cellSpacing="1" cellPadding="5" width="100%" border="0">
											<TR>
												<TD colspan="3"  class="typeTitle" align="left"  background="../../images/bg_title.gif" height="18px"><img src="../../images/titleicon.gif">&nbsp;角色管理</TD>
											</TR>
											<TR>
												<TD align="center">
													<TABLE  cellSpacing="0" cellPadding="0" border="0">	
														<tr>
															<td class="typeTitle" align="center">所有角色</td>
														</tr> 
														<tr>
															<td valign="top">
																<DIV id="divAllRole" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 200px; CLIP: rect(0px 200px 296px 0px); BORDER-BOTTOM: #aed3f0 2px inset; HEIGHT: 296px">
																	<asp:datagrid id="grdAllRole" runat="server" CssClass="labelGrid" 
																			AllowPaging="True" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
																			PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
																			<SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
																			<EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
																			<AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
																			<ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
																			<HeaderStyle CssClass="FixedHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
																			<FooterStyle BackColor="#CCCC99"></FooterStyle><Columns>
																			<asp:ButtonColumn Visible=False  DataTextField="UID" SortExpression="UID" HeaderText="UID" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn  DataTextField="NAME" SortExpression="NAME" HeaderText="角色" CommandName="Select">
																				<HeaderStyle Width="200px"></HeaderStyle>
																			</asp:ButtonColumn>
																		</Columns>
																		<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
																	</asp:datagrid>
																</DIV>															
															</td>
														</tr>
													</TABLE>
												</TD>
												<TD  align="left" width="120">
													<asp:Button id="btnSelectOne" Runat="server" CssClass="button" Text=" > " Width="80px" Height="36px"></asp:Button><BR>
													<asp:Button id="btnSelectAll" Runat="server" CssClass="button" Text=" >> " Width="80px" Height="36px"></asp:Button><BR>
													<asp:Button id="btnDeleteOne" Runat="server" CssClass="button" Text=" < " Width="80px" Height="36px"></asp:Button><BR>
													<asp:Button id="btnDeleteAll" Runat="server" CssClass="button" Text=" << " Width="80px" Height="36px"></asp:Button><BR>
												</TD>												
												<TD align="left">
													<TABLE  cellSpacing="0" cellPadding="0" border="0">	
														<tr>
															<td class="typeTitle" align="center">已加入角色</td>
														</tr>
														<tr>
															<td align="left" valign="top">
																<DIV id="divChoiceRole" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 200px; CLIP: rect(0px 200px 296px 0px); BORDER-BOTTOM: #aed3f0 2px inset; HEIGHT: 296px">
																	<asp:datagrid id="grdChoiceRole" runat="server" CssClass="labelGrid" 
																			AllowPaging="True" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
																			PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
																			<SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
																			<EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
																			<AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
																			<ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
																			<HeaderStyle CssClass="FixedHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
																			<FooterStyle BackColor="#CCCC99"></FooterStyle><Columns>
																			<asp:ButtonColumn Visible=False  DataTextField="UID" SortExpression="UID" HeaderText="UID" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn  DataTextField="NAME" SortExpression="NAME" HeaderText="已选角色" CommandName="Select">
																				<HeaderStyle Width="200px"></HeaderStyle>
																			</asp:ButtonColumn>
																		</Columns>
																		<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
																	</asp:datagrid>
																</DIV>															
															</td>
														</tr>
													</TABLE>
												</TD>
											</TR>	
											<TR vAlign="middle">
												<td></td> 
												<TD align="left"><asp:button id="btnSaveRole" Runat="server" Width="80px" Height="36" CssClass="label" Text=" 修  改 "></asp:Button></TD>
												<td></td> 
											</TR>
											
										</TABLE>
										<% response.write("</DIV>" + vbcr) %>	
										<% 
											if propEditMode = 5 then
												response.write("<DIV id='contentRight5' style='display:'>" + vbcr)
											else
												response.write("<DIV id='contentRight5' style='display:none'>" + vbcr)
											end if
										%>
										<TABLE cellSpacing="1" cellPadding="5" width="100%" border="0">
											<TR>
												<TD colspan="3"  class="typeTitle" align="left"  background="../../images/bg_title.gif" height="18px"><img src="../../images/titleicon.gif">&nbsp;范围管理</TD>
											</TR>
											<TR>
												<TD align="center">
													<TABLE  cellSpacing="0" cellPadding="0" border="0">	
														<tr>
															<td class="typeTitle" align="center">所有范围</td>
														</tr> 
														<tr>
															<td valign="top">
																<DIV id="divAllCYFW" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 200px; CLIP: rect(0px 200px 296px 0px); BORDER-BOTTOM: #aed3f0 2px inset; HEIGHT: 296px">
																	<asp:datagrid id="grdAllCYFW" runat="server" CssClass="labelGrid" 
																			AllowPaging="True" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
																			PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
																			<SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
																			<EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
																			<AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
																			<ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
																			<HeaderStyle CssClass="FixedHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
																			<FooterStyle BackColor="#CCCC99"></FooterStyle><Columns>
																			<asp:ButtonColumn DataTextField="范围名称" SortExpression="范围名称" HeaderText="范围名称" CommandName="Select">
																				<HeaderStyle Width="190px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="流水号" SortExpression="流水号" HeaderText="流水号" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="范围标志" SortExpression="范围标志" HeaderText="范围标志" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>																			
																		</Columns>
																		<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
																	</asp:datagrid>
																</DIV>															
															</td>
														</tr>
													</TABLE>
												</TD>
												<TD  align="left" width="120">
													<asp:Button id="btnSelectCYFWOne" Runat="server" CssClass="button" Text=" > " Width="80px" Height="36px"></asp:Button><BR>
													<asp:Button id="btnSelectCYFWALL" Runat="server" CssClass="button" Text=" >> " Width="80px" Height="36px"></asp:Button><BR>
													<asp:Button id="btnDeleteCYFWOne" Runat="server" CssClass="button" Text=" < " Width="80px" Height="36px"></asp:Button><BR>
													<asp:Button id="btnDeleteCYFWAll" Runat="server" CssClass="button" Text=" << " Width="80px" Height="36px"></asp:Button><BR>
												</TD>												
												<TD align="left">
													<TABLE  cellSpacing="0" cellPadding="0" border="0">	
														<tr>
															<td class="typeTitle" align="center">已加入范围</td>
														</tr>
														<tr>
															<td align="left" valign="top">
																<DIV id="divChoiceCYFW" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 200px; CLIP: rect(0px 200px 296px 0px); BORDER-BOTTOM: #aed3f0 2px inset; HEIGHT: 296px">
																	<asp:datagrid id="grdChoiceCYFW" runat="server" CssClass="labelGrid" 
																			AllowPaging="True" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
																			PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"  UseAccessibleHeader="True" BorderStyle="Solid">
																			<SelectedItemStyle  Font-Bold="False" VerticalAlign="top" ForeColor="blue" ></SelectedItemStyle>
																			<EditItemStyle   BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
																			<AlternatingItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>
																			<ItemStyle  BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
																			<HeaderStyle CssClass="FixedHead"  Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
																			<FooterStyle BackColor="#CCCC99"></FooterStyle><Columns>
																			<asp:ButtonColumn DataTextField="范围名称" SortExpression="范围名称" HeaderText="范围名称" CommandName="Select">
																				<HeaderStyle Width="190px"></HeaderStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="流水号" SortExpression="流水号" HeaderText="流水号" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
																			</asp:ButtonColumn>
																			<asp:ButtonColumn Visible="False" DataTextField="范围标志" SortExpression="范围标志" HeaderText="范围标志" CommandName="Select">
																				<HeaderStyle Width="0px"></HeaderStyle>
																			</asp:ButtonColumn>	
																		</Columns>
																		<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
																	</asp:datagrid>
																</DIV>															
															</td>
														</tr>
													</TABLE>
												</TD>
											</TR>	
											<TR vAlign="middle">
												<td></td> 
												<TD align="left"><asp:button id="btnSaveCYFW" Runat="server" Width="80px" Height="36" CssClass="label" Text=" 修  改 "></asp:Button></TD>
												<td></td> 
											</TR>
											
										</TABLE>
										<% response.write("</DIV>" + vbcr) %>							
									</TD>	
								</tr>														
							</table>							
						</td>						
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:Panel id="panelError" Runat="server">
				<TABLE id="tabErrMain" height="95%" cellSpacing="0" cellPadding="0" width="90%" border="0">
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
						<input id="htxtBH" type="hidden" runat="server" NAME="htxtBH">
						<input id="htxtControlId" type="hidden" runat="server">
						<input id="htxtDivLeftMain" type="hidden" runat="server">
						<input id="htxtDivTopMain" type="hidden" runat="server">
						<input id="htxtDivLeftBody" type="hidden" runat="server">
						<input id="htxtDivTopBody" type="hidden" runat="server">
						<input id="htxtTASKQuery" type="hidden" runat="server">
                        <input id="htxtTASKRows" type="hidden" runat="server">
                        <input id="htxtTASKSort" type="hidden" runat="server">
                        <input id="htxtTASKSortColumnIndex" type="hidden" runat="server">
                        <input id="htxtTASKSortType" type="hidden" runat="server">
                        <input id="htxtDivLeftTASK" type="hidden" runat="server">
                        <input id="htxtDivTopTASK" type="hidden" runat="server">						
                        <input id="htxtSessionIdChoiceRole" type="hidden" runat="server">
                        <input id="htxtSessionIdChoiceCYFW" type="hidden" runat="server">
                        <input id="htxtBS" type="hidden" runat="server">
                        <input id="htxtAllRoleSort" type="hidden" runat="server">
						<input id="htxtAllRoleSortColumnIndex" type="hidden" runat="server">
						<input id="htxtAllRoleSortType" type="hidden" runat="server">
						<input id="htxtChoiceRoleSort" type="hidden" runat="server">
						<input id="htxtChoiceRoleSortColumnIndex" type="hidden" runat="server">
						<input id="htxtChoiceRoleSortType" type="hidden" runat="server">
						<input id="htxtChoiceCYFWSortColumnIndex" type="hidden" runat="server" >
                        <input id="htxtChoiceCYFWSortType" type="hidden" runat="server">
                        <input id="htxtChoiceCYFWSort" type="hidden" runat="server">
						<input id="htxtDivLeftAllRole" type="hidden" runat="server">
						<input id="htxtDivTopAllRole" type="hidden" runat="server">						
						<input id="htxtDivLeftChoiceRole" type="hidden" runat="server">
						<input id="htxtDivTopChoiceRole" type="hidden" runat="server">						
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
							function ScrollProc_divMain() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopMain");
								if (oText != null) oText.value = divMain.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftMain");
								if (oText != null) oText.value = divMain.scrollLeft;
								return;
							}							
							function ScrollProc_divTASK() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopFJ");
								if (oText != null) oText.value = divTASK.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftFJ");
								if (oText != null) oText.value = divTASK.scrollLeft;
								return;
							}
							function ScrollProc_divChoiceRole() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopChoiceRole");
								if (oText != null) oText.value = divChoiceRole.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftChoiceRole");
								if (oText != null) oText.value = divChoiceRole.scrollLeft;
								return;
							}
							function ScrollProc_divAllRole() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopAllRole");
								if (oText != null) oText.value = divAllRole.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftAllRole");
								if (oText != null) oText.value = divAllRole.scrollLeft;
								return;
							}
							function ScrollProc_divChoiceCYFW() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopChoiceCYFW");
								if (oText != null) oText.value = divChoiceCYFW.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftChoiceCYFW");
								if (oText != null) oText.value = divChoiceCYFW.scrollLeft;
								return;
							}
							function ScrollProc_divAllCYFW() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopAllCYFW");
								if (oText != null) oText.value = divAllCYFW.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftAllCYFW");
								if (oText != null) oText.value = divAllCYFW.scrollLeft;
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
								oText=document.getElementById("htxtDivTopMain");
								if (oText != null) divMain.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftMain");
								if (oText != null) divMain.scrollLeft = oText.value;
								
								oText=null;
								oText=document.getElementById("htxtDivTopFJ");
								if (oText != null) divTASK.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftFJ");
								if (oText != null) divTASK.scrollLeft = oText.value;


								document.body.onscroll = ScrollProc_Body;
								divMain.onscroll = ScrollProc_divMain;
								divTASK.onscroll = ScrollProc_divTASK;
							}
							catch (e) {}
						</script>
					</td>
				</tr>
				<tr>
					<td>
						<script language="javascript">window_onresize();</script>
						<uwin:popmessage id="popMessageObject" runat="server" width="96px" height="48px" ActionType="OpenWindow" PopupWindowType="Normal" EnableViewState="False" Visible="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>