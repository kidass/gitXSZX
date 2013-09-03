<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="appunlock.aspx.vb" Inherits="Xydc.Platform.web.appunlock" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统解锁窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../filecss/styles.css" type="text/css" rel="stylesheet">
		<script src="../scripts/transkey.js"></script>
		<script src="../scripts/libcookie.js"></script>
		<script language="javascript">
		<!--
            function document_onreadystatechange() 
            {
				//检查登录尝试次数
				try {
				    var intMaxTry = 0;
					var intTry = 0;
					intMaxTry = parseInt(document.getElementById("htxtMaxTryCount").value, 10);
					intTry = parseInt(document.getElementById("htxtTryCount").value, 10);
					if (intTry >= intMaxTry)
					{
						//锁定用户
						var objIexeFrame = null;
						var strUserId = "";
						strUserId = document.getElementById("txtUserId").value;
						objIexeFrame = getFrame(window.parent.frames, "iexeFrame"); //获取"iexeFrame"帧
						if (objIexeFrame)
						{
							objIexeFrame.window.open("./../lockuser.aspx?UserId=" + strUserId,"iexeFrame");
							for(var i=0; i<10000; i++);
						}

						//提示
						var strLockTime = "";
						strLockTime = document.getElementById("htxtLockTime").value;
						alert("提示：您已经尝试了" + intMaxTry.toString() +"次，您已被锁定，[" + strLockTime + "]分钟后系统自动解锁，按[确定]退出！");
						window.parent.doSetQuitPrompt(true);
						window.parent.close();
						return;
					}
					window.parent.doSetQuitPrompt(false);
					//检查登录尝试次数
				} catch (e) {}
				
				if (window.navigator.cookieEnabled == true)
					if (document.all("txtUserId").value == "")
						document.all("txtUserId").value = getCookie("JoscoJsoaUsername");
				if (document.all("txtUserId").value == "")
				{
					document.all("txtUserId").focus();
					return;
				}
				document.all("txtUserPwd").focus();
            }
            function btnLogin_onClick()
            {
				if (window.navigator.cookieEnabled == true)
					if (document.all("txtUserId").value != "")
						setDefaultCookie("JoscoJsoaUsername", document.all("txtUserId").value);
				__doPostBack("lnkLogin","");
            }
		//-->
		</script>
		<script language="javascript" for="document" event="onreadystatechange">
		<!--
            return document_onreadystatechange()
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../images/oabk.gif">
		<form id="frmAppUnlock" method="post" runat="server" language="javascript">
			<asp:panel id="panelMain" Runat="server">
				<TABLE height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0" background="">
					<TR>
						<TD vAlign="middle" align="center" width="100%">
							<TABLE style="BORDER-RIGHT: #3399ff 2px outset; BORDER-TOP: #3399ff 2px outset; FONT-SIZE: 11pt; FILTER: progid: DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr='#006699', EndColorStr='#6699CC'); BORDER-LEFT: #3399ff 2px outset; BORDER-BOTTOM: #3399ff 2px outset; FONT-FAMILY: 宋体" cellSpacing="0" cellPadding="0" border="0" background="../images/passbk.jpg">
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left" colSpan="2" height="30"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24">&nbsp;&nbsp;&nbsp;&nbsp;用户标识：<asp:LinkButton id="lnkLogin" Runat="server" Width="0px"></asp:LinkButton></TD>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24"><INPUT id="txtUserId" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="18" name="txtUserId" runat="server" readonly>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="20"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24">&nbsp;&nbsp;&nbsp;&nbsp;用户密码：</TD>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24"><INPUT id="txtUserPwd" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="password" size="18" name="txtUserPwd" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="20"></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2" height="24"><INPUT id="btnLogin" style="FONT-SIZE: 11pt; WIDTH: 100px; FONT-FAMILY: 宋体; HEIGHT: 36px" onclick="javascript:btnLogin_onClick();" type="button" value=" 登  录 " name="btnLogin">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT style="FONT-SIZE: 11pt; WIDTH: 100px; FONT-FAMILY: 宋体; HEIGHT: 36px" type="reset" value=" 重  设 "></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="30"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<table cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<td>
						<input id="htxtTryCount" runat="server" type="hidden" value="0">
						<input id="htxtMaxTryCount" runat="server" type="hidden">
						<input id="htxtLockTime" runat="server" type="hidden">
					</td>
					<td>
						<uwin:popmessage id="popMessageObject" runat="server" width="100px" height="60px" Visible="False" ActionType="OpenWindow" EnableViewState="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>