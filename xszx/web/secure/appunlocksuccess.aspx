<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="appunlocksuccess.aspx.vb" Inherits="Xydc.Platform.web.appunlocksuccess" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统解锁成功后的继续处理</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../filecss/styles01.css" type="text/css" rel="stylesheet">
		<script src="../scripts/transkey.js"></script>
		<script language="javascript">
//			function openChat()
//			{
//				var objLeftFrame = null;
//				try 
//				{
//					objLeftFrame = getFrame(window.parent.frames, "leftFrame"); 
//					if (objLeftFrame)
//						objLeftFrame.window.execScript("openChatEnforced();");
//				} 
//				catch (e) 
//				{
//					return false;
//				}
//				return true;
//			}
			function doDisplayAppUnLocked()
			{
				var objTopFrame = null;
				try 
				{
					objTopFrame = getFrame(window.parent.frames, "topFrame"); 
					if (objTopFrame)
						objTopFrame.window.execScript("doDisplayAppUnLocked();");
				} 
				catch (e) 
				{
					return false;
				}
				return true;
			}
			function doRedirect()
			{
				//刷新系统锁定状态显示
				doDisplayAppUnLocked();
				//重新显示Chat界面
				//openChat();
				//重定向到主界面
				window.open("main.aspx","mainFrame");
			}
			function document_onreadystatechange() 
			{
				window.setTimeout("doRedirect()", 3000);
			}
		</script>
		<script language="javascript" for="document" event="onreadystatechange">
			return document_onreadystatechange()
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../images/oabk.gif">
		<form id="frmAppUnlockSuccess" method="post" runat="server" language="javascript">
			<asp:panel id="panelMain" Runat="server">
				<TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="middle" align="center" width="100%" style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体">系统正在进行解锁成功后的后续处理工作，请稍侯......</TD>
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