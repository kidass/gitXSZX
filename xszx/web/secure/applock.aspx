<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="applock.aspx.vb" Inherits="Xydc.Platform.web.applock" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统锁定窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<LINK href="../filecss/styles01.css" type="text/css" rel="stylesheet" />
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../images/oabk.gif">
		<form id="frmAppLock" method="post" runat="server" language="javascript">
			<asp:panel id="panelMain" Runat="server">
				<TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" vAlign="middle" align="center" width="100%">系统已经被锁定，开始正常使用请单击<A id="btnAppUnlock" style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" href="appunlock.aspx" target="mainFrame">这里</A>解除锁定！</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<table cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<td>
						<uwin:popmessage id="popMessageObject" runat="server" width="100px" height="60px" Visible="False" ActionType="OpenWindow" EnableViewState="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>