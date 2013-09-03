<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="areaFoot.aspx.vb" Inherits="Xydc.Platform.web.areaFoot" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统版权窗</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../filecss/style.css" type="text/css" rel="stylesheet">
		<script src="scripts/transkey.js"></script>
		<script language="javascript">
		<!--
			function window_onbeforeunload() 
			{
				//关闭监视窗口
				if (window.parent.m_objChatWindowId)
				{
					try {
						window.parent.m_objChatWindowId.close();
					} catch (e) {}
				}
				//注销用户
				window.open("./secure/logout.aspx","iexeFrame");
				//等待
				for(var i=0; i<10000; i++);
			}
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" language="javascript" onbeforeunload="return window_onbeforeunload()">
        <table height="30" cellSpacing="0" cellPadding="0" width="100%" border="0" style="BORDER-TOP: #1092f0 3px solid">
			<tr>
			    <td align="center" style="FONT-SIZE: 12px; COLOR: #003399; FONT-FAMILY: 'Courier New', 宋体, 新宋体"><div style="display:none"><asp:Label ID="lblFootMessage" Runat="server"></asp:Label></div></td>
			</tr>
			<tr>
			    <td height="10"></td>
			</tr>
			<tr>
			    <td align="center" style="FONT-SIZE: 12px; COLOR: #000000; FONT-FAMILY: 宋体">兴业地产</td>
			</tr>
			<tr>
			    <td height="6"></td>
			</tr>
			<tr>
			    <td align="center" style="FONT-SIZE: 12px; COLOR: #000000; FONT-FAMILY: 宋体">办公室信息岗</td>
			</tr>
		</table>
	</body>
</HTML>