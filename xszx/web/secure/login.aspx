<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Xydc.Platform.web.login" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统登录窗</title>
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
				
				if (document.readyState.toLowerCase() != "complete")
					window.setTimeout("document_onreadystatechange()", 500);
				
			
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
				
				if (document.all("htxtAutoPostBack").value == "1")
				{
					document.all("htxtAutoPostBack").value = "0";
					__doPostBack("lnkStartupOption");
				}
				
            }
            function btnLogin_onClick()
            {
				if (window.navigator.cookieEnabled == true)
					if (document.all("txtUserId").value != "")
						setDefaultCookie("JoscoJsoaUsername", document.all("txtUserId").value);
				__doPostBack("lnkLogin","");
            }
            function btnModifyPassword_onClick()
            {
				if (window.navigator.cookieEnabled == true)
					if (document.all("txtUserId").value != "")
						setDefaultCookie("JoscoJsoaUsername", document.all("txtUserId").value);
				__doPostBack("lnkModifyPassword","");
            }
		//-->
		</script>
		<script language="javascript" for="document" event="onreadystatechange">
		<!--
            return document_onreadystatechange()
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0"  background="../images/oabk.gif">
		<form id="frmLogin" method="post" runat="server" language="javascript">
			<asp:panel id="panelLogin" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%" height="98%" >
					<TR>
						<TD vAlign="middle" align="center" width="100%">
							<TABLE style="BORDER-RIGHT: #3399ff 2px outset; BORDER-TOP: #FFFFFF 2px outset; FONT-SIZE: 11pt; FILTER: progid: DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr='#99CCFF', EndColorStr='#99CCFF'); BORDER-LEFT: #ffffff 2px outset; BORDER-BOTTOM: #3399ff 2px outset; FONT-FAMILY: 宋体" cellSpacing="0" cellPadding="0" border="0" background="../images/passbk.jpg">
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left" colSpan="2" height="30"><asp:LinkButton ID="lnkStartupOption" Runat="server" Width="0px"></asp:LinkButton><asp:LinkButton ID="lnkLogin" Runat="server" Width="0px"></asp:LinkButton><asp:LinkButton ID="lnkModifyPassword" Runat="server" Width="0px"></asp:LinkButton></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24">&nbsp;&nbsp;&nbsp;&nbsp;用&nbsp;&nbsp;&nbsp;&nbsp;户：</TD>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24"><INPUT tabindex="1"  id="txtUserId" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="18" name="txtUserId" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="20"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24">&nbsp;&nbsp;&nbsp;&nbsp;密&nbsp;&nbsp;&nbsp;&nbsp;码：</TD>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" height="24"><INPUT tabindex="2" id="txtUserPwd" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="password" size="18" name="txtUserPwd" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="10"></TD>
								</TR>
								<TR>									
									<TD class="labelBlack" colspan="2" align="center"><div style="display:none">
										<asp:RadioButtonList id="rblJRLX" Runat="server"  RepeatColumns="2" RepeatDirection="Vertical" RepeatLayout="Flow" Font-Size="11pt" Font-Name="宋体" CellSpacing="10" EnableViewState="True" >
											<asp:ListItem Value="0" Selected="True">内部主页</asp:ListItem>
											<asp:ListItem Value="1">办公事宜</asp:ListItem>
										</asp:RadioButtonList></div>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" colSpan="2" height="10"></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2" height="24"><INPUT tabindex="3" id="btnLogin" style="FONT-SIZE: 11pt; WIDTH: 100px; FONT-FAMILY: 宋体; HEIGHT: 36px" type="button" value=" 登  录 " name="btnLogin" onclick="javascript:btnLogin_onClick();">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT tabindex="4" id="btnModifyPassword" style="FONT-SIZE: 11pt; WIDTH: 100px; FONT-FAMILY: 宋体; HEIGHT: 36px" type="button" value=" 修改密码 " name="btnModifyPassword" onclick="javascript:btnModifyPassword_onClick();"></TD>
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
						<input id="htxtAutoPostBack" runat="server" type="hidden" value="1">
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
