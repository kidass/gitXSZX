<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="areaLeft.aspx.vb" Inherits="Xydc.Platform.web.areaLeft" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统操作窗</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../filecss/Style.css" type="text/css" rel="stylesheet">
		<link href="../filecss/navStyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			var m_objChatWindowId = null;
			function doToFullScreen()
			{
				try
				{
					var objTopFrame = getFrame(window.parent.frames, "topFrame");
					if (objTopFrame)
						objTopFrame.window.doToFullScreen("../");
				} catch (e) {}
			}
            function openWindow(url) 
            {
				try {
					//encode url
					url = encodeURI(url);
					//open
					window.open(url,"mainFrame");
				} catch (e) {}
            }
			
//            function openChat() 
//            {
//				try {
//					if (m_objChatWindowId)
//					{
//						if (m_objChatWindowId.closed)
//						{
//							var intHeight = window.parent.document.body.clientHeight + 24;
//							var intWidth  = screen.availWidth - 12 - 312;
//							m_objChatWindowId = window.open("./secure/chat/chat_main.aspx","_blank","top=50,left=" + intWidth.toString() +",width=270,height=620,fullscreen=no,menubar=no,resizable=yes,scrollbars=no,status=no,titlebar=no"); 
//							window.parent.m_objChatWindowId = m_objChatWindowId;
//						}
//						//else
//						//	m_objChatWindowId.focus();
//					}
//					else
//					{
//						var intHeight = window.parent.document.body.clientHeight + 24;
//						var intWidth  = screen.availWidth - 12 - 312;
//						m_objChatWindowId = window.open("./secure/chat/chat_main.aspx","_blank","top=50,left=" + intWidth.toString() +",width=270,height=620,fullscreen=no,menubar=no,resizable=yes,scrollbars=no,status=no,titlebar=no"); 
//						window.parent.m_objChatWindowId = m_objChatWindowId;
//					}
//				} catch (e) {}
//            }
//            function openChatEnforced() 
//            {
//				try {
//					if (m_objChatWindowId)
//					{
//						m_objChatWindowId.close();
//						m_objChatWindowId = null;
//					}
//					m_objChatWindowId = window.open("./secure/chat/chat_main.aspx","_blank","left=700,width=270,height=620,fullscreen=no,menubar=no,resizable=yes,scrollbars=no,status=no,titlebar=no"); 
//					window.parent.m_objChatWindowId = m_objChatWindowId;
//				} catch (e) {}
//            }
			
            function closeWindow() 
            {
				try {
					if (window.parent)
						window.parent.close();
				} catch (e) {}
            } 
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" bgcolor="#006699" language="javascript">
		<form id="frmLeft" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td valign="top" align="center">
						<ComponentArt:NavBar id="NavBarMain" runat="server" width="100%" SiteMapXmlFile="navData.xml" ShowScrollBar="True"
							ImagesBaseUrl="images/" DefaultSelectedItemLookId="Level2SelectedItemLook" CollapseTransition="Fade"
							ExpandTransition="Fade" CollapseDuration="350" ExpandDuration="350" DefaultItemSpacing="2" DefaultItemLookID="TopItemLook"
							CssClass="NavBar" DefaultChildSelectedItemLookId="Level2ItemLook" DefaultTarget="mainFrame" ExpandSinglePath="True">
							<ITEMLOOKS>
								<componentart:ItemLook CssClass="TopItem" LookId="TopItemLook" ExpandedCssClass="TopItemActive" ActiveCssClass="TopItemActive" HoverCssClass="TopItemHover"></componentart:ItemLook>
								<componentart:ItemLook CssClass="Level2Item" LookId="Level2ItemLook" HoverCssClass="Level2ItemHover"></componentart:ItemLook>
								<componentart:ItemLook CssClass="Level2ItemSelected" LookId="Level2SelectedItemLook" HoverCssClass="Level2ItemSelected" LabelPaddingLeft="15px"></componentart:ItemLook>
							</ITEMLOOKS>
						</ComponentArt:NavBar>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<td>
						<input id="htxtDivLeftBody" type="hidden" runat="server">
						<input id="htxtDivTopBody" type="hidden" runat="server">
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		    function ScrollProc_Body() {
		        var oText;
		        oText=null;
		        oText=document.getElementById("htxtDivTopBody");
		        if (oText != null) oText.value = document.body.scrollTop;
		        oText=null;
		        oText=document.getElementById("htxtDivLeftBody");
		        if (oText != null) oText.value = document.body.scrollLeft;
                return;
		    }
		    try {
		        var Text;

		        oText=null;
		        oText=document.getElementById("htxtDivTopBody");
		        if (oText != null) document.body.scrollTop = oText.value;
		        oText=null;
		        oText=document.getElementById("htxtDivLeftBody");
		        if (oText != null) document.body.scrollLeft = oText.value;

		        document.body.onscroll = ScrollProc_Body;
            }
            catch (e) {}
		</script>
	</body>
</HTML>
