<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ggdm_bmry_bmxx.aspx.vb" Inherits="Xydc.Platform.web.ggdm_bmry_bmxx" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>单位信息显示或编辑窗</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<script src="../../scripts/transkey.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../../images/oabk.gif">
		<form id="frmGGDM_BMRY_BMXX" method="post" runat="server">
			<asp:Panel ID="panelMain" Runat="server">
				<TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="BORDER-BOTTOM: #99cccc 1px solid" class="title" vAlign="middle" align="center" colSpan="3" height="30">单位信息显示或编辑窗<asp:LinkButton id="lnkBlank" Runat="server" Height="5px" Width="0px"></asp:LinkButton></TD>
					</TR>
					<TR>
						<TD width="5%"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0" height="100%">
								<TR vAlign="middle" align="center">
									<TD class="tips" align="left" colSpan="2" height="30">输入框旁带红色*号的内容必须输入，输入完成后按[确定]保存并返回。</TD>
								</TR>
								<TR vAlign="middle">
									<TD class="labelNotNull" align="right">单位代码：</TD>
									<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtZZDM" runat="server" Height="24px" Wrap="False" Font-Size="12px" Font-Names="宋体" Columns="12" CssClass="textbox"></asp:textbox><FONT color="#ff0000">*[格式：AABBCCDDEEFF]</FONT></SPAN></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="labelNotNull" align="right">单位名称：</TD>
									<TD class="labelNotNull" align="left"><SPAN class="labelNotNull"><asp:textbox id="txtZZMC" runat="server" Height="24px" Wrap="False" Font-Size="12px" Font-Names="宋体" Columns="60" CssClass="textbox"></asp:textbox><FONT color="#ff0000">*</FONT></SPAN></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">单位全称：</TD>
									<TD class="label" align="left"><asp:textbox id="txtZZBM" runat="server" Height="60px" Font-Size="12px" Font-Names="宋体" Columns="59" CssClass="textbox" TextMode="MultiLine"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">单位级别：</TD>
									<TD class="label" align="left"><asp:textbox id="txtJBMC" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="16" CssClass="textbox" TextMode="SingleLine" ReadOnly="True"></asp:textbox><asp:Button id="btnSelectJBDM" Runat="server" Font-Size="12px" Text=" … " Font-Name="宋体"></asp:Button><INPUT id="htxtJBDM" type="hidden" runat="server"></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">单位秘书：</TD>
									<TD class="label" align="left"><asp:textbox id="txtMSMC" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="16" CssClass="textbox" TextMode="SingleLine" ReadOnly="True"></asp:textbox><asp:Button id="btnSelectMSDM" Runat="server" Font-Size="12px" Text=" … " Font-Name="宋体"></asp:Button><INPUT id="htxtMSDM" type="hidden" runat="server"></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">联系电话：</TD>
									<TD class="label" align="left"><asp:textbox id="txtLXDH" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="60" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">移动电话：</TD>
									<TD class="label" align="left"><asp:textbox id="txtSJHM" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="30" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								
								<TR vAlign="middle">
									<TD class="label" align="right">组织序号：</TD>
									<TD class="label" align="left"><asp:textbox id="txtZZXH" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="6" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">编制人数：</TD>
									<TD class="label" align="left"><asp:textbox id="txtBZRS" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="6" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								
								<TR vAlign="middle">
									<TD class="label" align="right">内部邮箱：</TD>
									<TD class="label" align="left"><asp:textbox id="txtFTPDZ" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="60" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">因特网邮箱：</TD>
									<TD class="label" align="left"><asp:textbox id="txtYXDZ" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="60" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">联系地址：</TD>
									<TD class="label" align="left"><asp:textbox id="txtLXDZ" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="60" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">邮政编码：</TD>
									<TD class="label" align="left"><asp:textbox id="txtYZBM" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="6" CssClass="textbox" TextMode="SingleLine" ReadOnly="False"></asp:textbox></TD>
								</TR>
								<TR vAlign="middle">
									<TD class="label" align="right">单位联系人：</TD>
									<TD class="label" align="left"><asp:textbox id="txtLXR" runat="server" Height="24px" Font-Size="12px" Font-Names="宋体" Columns="16" CssClass="textbox" TextMode="SingleLine" ReadOnly="True"></asp:textbox><asp:Button id="btnSelectLXR" Runat="server" Font-Size="12px" Text=" … " Font-Name="宋体"></asp:Button><INPUT id="htxtLXRDM" type="hidden" runat="server"></TD>
								</TR>
								<TR vAlign="middle" align="center">
									<TD class="label" colSpan="2" height="10"></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5%"></TD>
					</TR>
					<TR vAlign="middle">
						<TD height="6" colspan="3">
					</TR>
					<TR vAlign="middle">
						<TD align="center" colspan="3" style="BORDER-TOP: #99cccc 1px solid">
							<asp:button id="btnOK" Runat="server" Height="36" Width="94" Font-Size="12px" Font-Names="宋体" CssClass="button" Text=" 确  定 "></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnCancel" Runat="server" Height="36px" Width="94px" Font-Size="12px" Font-Names="宋体" CssClass="button" Text=" 取  消 "></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnClose" Runat="server" Height="36px" Width="94px" Font-Size="12px" Font-Names="宋体" CssClass="button" Text=" 返  回 "></asp:button>
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
