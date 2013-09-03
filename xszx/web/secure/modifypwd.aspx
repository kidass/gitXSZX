<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="modifypwd.aspx.vb" Inherits="Xydc.Platform.web.modifypwd" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>用户密码修改窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../filecss/styles.css" type="text/css" rel="stylesheet">
		<script src="../scripts/transkey.js"></script>
		<script language="javascript" id="clientEventHandlersJS">
            function document_onreadystatechange() 
            {
                try {
                    var txtNewUserPwd = document.getElementById("txtNewUserPwd");
                    txtNewUserPwd.focus(); 
                } catch (e) {}
            }
		</script>
		<script language="javascript" for="document" event="onreadystatechange">
            return document_onreadystatechange()
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../images/oabk.gif">
		<form id="frmLogin" method="post" runat="server">
			<asp:panel id="panelModifyPwd" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%" height="98%">
					<TR>
						<TD vAlign="middle" align="center" width="100%">
							<TABLE style="BORDER-RIGHT: #3399ff 2px outset; BORDER-TOP: #3399ff 2px outset; FONT-SIZE: 11pt;  BORDER-LEFT: #3399ff 2px outset; BORDER-BOTTOM: #3399ff 2px outset; FONT-FAMILY: 宋体" cellSpacing="0" cellPadding="0" border="0" bgcolor="#DFEFFF">
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left" colSpan="2" height="30"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="24">&nbsp;&nbsp;&nbsp;&nbsp;用户标识：</TD>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24"><INPUT id="txtUserId" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="18" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="20"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="24">&nbsp;&nbsp;&nbsp;&nbsp;输入新密码：</TD>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24"><INPUT id="txtNewUserPwd" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="password" size="18" runat="server">&nbsp;(最少<%=Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength%>个字符)&nbsp;</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="20"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="24">&nbsp;&nbsp;&nbsp;&nbsp;确认新密码：</TD>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24"><INPUT id="txtNewUserPwdQR" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="password" size="18" runat="server">&nbsp;(最少<%=Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength%>个字符)&nbsp;</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="20"></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2" height="24"><INPUT language="javascript" id="btnModify" style="FONT-SIZE: 11pt; WIDTH: 100px; FONT-FAMILY: 宋体; HEIGHT: 36px" type="button" value=" 确  定 " runat="server">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="btnReset" style="FONT-SIZE: 11pt; WIDTH: 100px; FONT-FAMILY: 宋体; HEIGHT: 36px" type="reset"  value=" 重  设 " runat="server">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="btnCancel" style="FONT-SIZE: 11pt; WIDTH: 100px; FONT-FAMILY: 宋体; HEIGHT: 36px" type="button" value=" 取  消 " runat="server"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="30"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<asp:Panel id="panelInformation" Runat="server">
				<TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="5%"></TD>
						<TD>
							<TABLE id="tabErrInfo" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD style="FONT-SIZE: 30pt; COLOR: black; LINE-HEIGHT: 48pt; FONT-FAMILY: 宋体; LETTER-SPACING: 2pt" align="center"><asp:Label id="lblMessage" Runat="server"></asp:Label><p>&nbsp;&nbsp;</p><p><input type="button" id="btnGoBack" value=" 返回 " style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" onclick="javascript:history.back();"></p></TD>
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
						<uwin:popmessage id="popMessageObject" runat="server" width="100px" height="60px" Visible="False" ActionType="OpenWindow" EnableViewState="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>