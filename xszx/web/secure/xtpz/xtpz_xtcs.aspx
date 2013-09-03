<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="xtpz_xtcs.aspx.vb" Inherits=" Xydc.Platform.web.xtpz_xtcs" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统运行参数配置窗</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<script src="../../scripts/transkey.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../../images/oabk.gif">
		<form id="frmXTPZ_XTCS" method="post" runat="server">
			<asp:Panel ID="panelMain" Runat="server">
				<TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="title" vAlign="middle" align="center" colSpan="3" height="30" style="BORDER-BOTTOM: #99cccc 1px solid">系统运行参数配置窗<asp:LinkButton id="lnkBlank" Runat="server" Width="0px" Height="5px"></asp:LinkButton></TD>
					</TR>
					<TR>
						<TD width="5%"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0" height="100%">
								<TR vAlign="middle" align="center">
									<TD class="tips" align="left" colSpan="2" height="30">输入框旁带红色*号的内容必须输入，输入完成后按[确定]保存并返回。</TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" noWrap align="right" height="36">主网页地址：</TD>
									<TD class="label" align="left"><SPAN class="label"><asp:textbox id="txtZNBZYWZ" runat="server" Height="24px" CssClass="textbox" Columns="48" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox></SPAN></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="labelNotNull" noWrap align="right" height="36">主FTP服务器名：</TD>
									<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtZFTPFWQ" runat="server" Height="24px" CssClass="textbox" Columns="48" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox><FONT color="#ff0000">*</FONT></SPAN></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="labelNotNull" noWrap align="right" height="36">主FTP服务器端口：</TD>
									<TD class="labelNotNull" align="left"><asp:textbox id="txtZFTPDK" runat="server" Height="24px" CssClass="textbox" Columns="12" Font-Names="宋体" Font-Size="12px"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="labelNotNull" noWrap align="right" height="36">主FTP服务器用户：</TD>
									<TD class="labelNotNull" align="left"><asp:textbox id="txtZFTPYH" runat="server" Height="24px" CssClass="textbox" Columns="36" Font-Names="宋体" Font-Size="12px"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" noWrap align="right" height="36">主FTP服务器用户密码：</TD>
									<TD class="label" align="left"><INPUT id="txtZFTPMM" type="password" size="36" runat="server"></TD>
								</TR>
								<TR vAlign="middle" align="center" height="10">
									<TD class="label" colSpan="2"></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" noWrap align="right" height="36">备用网页地址：</TD>
									<TD class="label" align="left"><SPAN class="label"><asp:textbox id="txtCNBZYWZ" runat="server" Height="24px" CssClass="textbox" Columns="48" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox></SPAN></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" noWrap align="right" height="36">备用FTP服务器名：</TD>
									<TD class="label" align="left"><SPAN class="label"><asp:textbox id="txtCFTPFWQ" runat="server" Height="24px" CssClass="textbox" Columns="48" Font-Names="宋体" Font-Size="12px" Wrap="False"></asp:textbox></SPAN></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" noWrap align="right" height="36">备用FTP服务器端口：</TD>
									<TD class="label" align="left"><asp:textbox id="txtCFTPDK" runat="server" Height="24px" CssClass="textbox" Columns="12" Font-Names="宋体" Font-Size="12px"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" noWrap align="right" height="36">备用FTP服务器用户：</TD>
									<TD class="label" align="left"><asp:textbox id="txtCFTPYH" runat="server" Height="24px" CssClass="textbox" Columns="36" Font-Names="宋体" Font-Size="12px"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" noWrap align="right" height="36">备用FTP服务器用户密码：</TD>
									<TD class="label" align="left"><INPUT id="txtCFTPMM" type="password" size="36" runat="server"></TD>
								</TR>
								<TR vAlign="middle" align="center" height="10">
									<TD class="label" colSpan="2">
										<INPUT id="htxtBS" type="hidden" runat="server">
										<INPUT id="htxtSFJM" type="hidden" runat="server">
										<INPUT id="htxtSessionIdZFTPMM" type="hidden" runat="server">
										<INPUT id="htxtSessionIdCFTPMM" type="hidden" runat="server">
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5%"></TD>
					</TR>
					<TR vAlign="middle">
						<TD colspan="3" align="center" height="6"></TD>
					</TR>
					<TR vAlign="middle">
						<TD align="center" colspan="3" style="BORDER-TOP: #99cccc 1px solid">
							<asp:button id="btnOK" Runat="server" Width="94" Height="36" CssClass="button" Font-Names="宋体" Font-Size="12px" Text=" 保  存 "></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnCancel" Runat="server" Width="94px" Height="36px" CssClass="button" Font-Names="宋体" Font-Size="12px" Text=" 取  消 "></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnClose" Runat="server" Width="94px" Height="36px" CssClass="button" Font-Names="宋体" Font-Size="12px" Text=" 关  闭 "></asp:button>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
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
						<uwin:popmessage id="popMessageObject" runat="server" width="96px" height="48px" ActionType="OpenWindow" PopupWindowType="Normal" EnableViewState="False" Visible="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>