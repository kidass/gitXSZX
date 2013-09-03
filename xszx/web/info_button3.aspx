<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="info_button3.aspx.vb" Inherits="Xydc.Platform.web.info_button3" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统信息提示窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script type="text/javascript">
			function  btnYes_Click()
			{
				window.returnValue = 6; //vbYes
				window.close();
			}
			function  btnNo_Click()
			{
				window.returnValue = 7; //vbNo
				window.close();
			}
			function  btnCancel_Click()
			{
				window.returnValue = 2; //vbCancel
				window.close();
			}
			function document_onreadystatechange() 
			{
				divMessage.innerHTML = window.dialogArguments;
			}
		</script>
		<script language="javascript" event="onreadystatechange" for="document">
			return document_onreadystatechange()
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" bgcolor="#ece9d8">
		<form id="frminfo_button3" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" border="0" align="center">
				<tr>
					<td height="10"></td>
				</tr>
				<tr>
					<td align="center">
						<div id="divMessage" style="FONT-SIZE: 16px; WIDTH: 290px; HEIGHT: 100px;line-height:22px;"></div>
					</td>
				</tr>
				<tr>
					<td height="10"></td>
				</tr>
				<tr>
					<td vAlign="middle" align="center" height="30"><input id="btnYes" style="FONT-SIZE: 16px; WIDTH: 80px" onclick="btnYes_Click()" type="button"
							value="是"> <input id="btnNo" style="FONT-SIZE: 16px; WIDTH: 80px" onclick="btnNo_Click()" type="button"
							value="否"> <input id="btnCancel" style="FONT-SIZE: 16px; WIDTH: 80px" onclick="btnCancel_Click()"
							type="button" value="取消">
					</td>
				</tr>
				<tr>
					<td height="10"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
