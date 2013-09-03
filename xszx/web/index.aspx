<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Xydc.Platform.web.index" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN" "http://www.w3.org/TR/html4/frameset.dtd">
<html>
	<head>
		<title>
			<%=System.Configuration.ConfigurationManager.AppSettings("applicationname") & "v" & System.Configuration.ConfigurationManager.AppSettings("applicationversion")%>
		</title>
        <meta name="generator" content="microsoft visual studio .net 7.1" />
        <meta name="code_language" content="visual basic .net 7.1" />
        <meta name="vs_defaultclientscript" content="javascript" />
        <meta name="vs_targetschema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<script  src="scripts/transkey.js"></script>
		<!--为方便用户阅读，将页面最大化，代码开始-->
		<script  language="JavaScript"> 
		self.moveTo(0,0)
		self.resizeTo(screen.availWidth,screen.availHeight)
		</script>
		<!--为方便用户阅读，将页面最大化，代码结束-->
		<script language = "javascript">
			//
			//id for chat popup window
			//
			var m_objChatWindowId = null;
			//
			//false：退出确认
			//true ：直接退出
			//
			var blnEnforced = false; 
			function doSetQuitPrompt(blnPrompt)
			{
				blnEnforced = blnPrompt;
			}
			function doHideLogoWindow()
			{
				window.mainFrameSet.rows = "0,25,*";
				window.contentFrameSet01.rows = "*,0,0";
			}
			function doShowLogoWindow()
			{
				window.mainFrameSet.rows = "80,25,*";
				window.contentFrameSet01.rows = "*,0,0";
			}
			function doHideLeftFrame()
			{
				window.contentFrameSet.cols = "0,*";
			}
			function window_onbeforeunload() 
			{
				if (blnEnforced == false)
					return "警告：您确定要退出系统吗（确定/取消）？\n    [确定]退出系统，\n    [取消]回到工作页面！";
			}
		</script>
	</HEAD>
	<frameset id="mainFrameSet" rows="80,25,*" framespacing="0" frameborder="no" onbeforeunload="return window_onbeforeunload();">
		<frame id="topFrameLogo" src="areaTopLogo.aspx" name="topFrameLogo" scrolling="no" frameborder="no" noresize>
		<frame id="topFrame" src="areaTop.aspx" name="topFrame" scrolling="no" frameborder="no" noresize>
		<frameset id="contentFrameSet" cols="0,*" framespacing="0" frameborder="no">
			<frame id="leftFrame" src="areaLeft.aspx" name="leftFrame" scrolling="auto" frameborder="no">
			<frameset id="contentFrameSet01" rows="*,0,0" framespacing="0">
				<frame id="mainFrame" src="areaContent.aspx" name="mainFrame" scrolling="auto" frameborder="no">
				<frame id="footFrame" src="areaFoot.aspx" name="footFrame" scrolling="no" frameborder="no">
				<frame id="iexeFrame" src="about:blank" name="iexeFrame" scrolling="no" frameborder="no">
			</frameset>
		</frameset>
	</frameset>
</HTML>

