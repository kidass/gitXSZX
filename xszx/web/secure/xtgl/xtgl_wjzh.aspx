<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="xtgl_wjzh.aspx.vb" Inherits="Xydc.Platform.web.xtgl_wjzh" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>文件转换器</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdObjectLocked { ; LEFT: expression(divObject.scrollLeft); POSITION: relative }
			TH.grdObjectLocked { ; LEFT: expression(divObject.scrollLeft); POSITION: relative }
			TH { Z-INDEX: 10; POSITION: relative }
			TH.grdObjectLocked { Z-INDEX: 99 }
		</style>
		<script src="../../scripts/transkey.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../../images/oabk.gif">
		<form id="frmXTGL_WJZH" method="post" runat="server">
			<asp:Panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD colSpan="3" height="4"></TD>
					</TR>
					<TR>
						<TD width="4"></TD>
						<TD align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD class="label" align="left"><asp:LinkButton id="lnkBlank" Runat="server" Width="0px"></asp:LinkButton>请指定要转换的目录：<asp:TextBox id="txtDIR" Runat="server" Columns="60" Font-Size="12px" Font-Name="宋体" CssClass="textbox"></asp:TextBox><asp:Button id="btnGetFile" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text="获取目录下的文件"></asp:Button></TD>
								</TR>
								<TR>
									<TD height="20"></TD>
								</TR>
								<TR>
									<TD class="label" align="left">选定目录下的文件如下：</TD>
								</TR>
								<TR>
									<TD height="12"></TD>
								</TR>
								<TR>
									<TD><asp:ListBox id="lstFILE" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Width="100%" Rows="32" SelectionMode="Multiple"></asp:ListBox></TD>
								</TR>
								<TR>
									<TD height="4"></TD>
								</TR>
								<TR>
									<TD align="center">
										<asp:Button id="btnZhuanhuan" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text="执行转换" Height="36px"></asp:Button>
										<asp:Button id="btnFanZhuanhuan" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text="执行反转换" Height="36px"></asp:Button>
										<asp:Button id="btnCancel" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 取    消 " Height="36px"></asp:Button>
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="4"></TD>
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
									<TD id="tdErrInfo" style="FONT-SIZE: 32pt; COLOR: black; LINE-HEIGHT: 40pt; FONT-FAMILY: 宋体; LETTER-SPACING: 2pt" align="center"><asp:Label id="lblMessage" Runat="server"></asp:Label><P>&nbsp;&nbsp;</P><P><INPUT id="btnGoBack" style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" onclick="javascript:history.back();" type="button" value=" 返回 "></P></TD>
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
						<uwin:popmessage id="popMessageObject" runat="server" width="96px" height="48px" Visible="False" ActionType="OpenWindow" PopupWindowType="Normal" EnableViewState="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>