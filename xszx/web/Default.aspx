<%@ page language="vb" autoeventwireup="false" codebehind="default.aspx.vb" inherits="Xydc.Platform.web.DefaultPage" %>
<!DOCTYPE html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
<html>
	<head>
		<title>网站启动页</title>		
		<meta name="generator" content="microsoft visual studio .net 7.1" >
		<meta name="code_language" content="visual basic .net 7.1" >
		<meta name="vs_defaultclientscript" content="javascript" >
		<meta name="vs_targetschema" content="http://schemas.microsoft.com/intellisense/ie5" >
		<script language="javascript">
			function doStartApplication()
			{
				window.setTimeout("closeStartupWindow();",1000);
				window.open("index.aspx","_blank","fullscreen=no,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,toolbar=no","");
			}
			function closeStartupWindow()
			{
				window.opener = null;
				window.open("about:blank","_top");
				window.top.close();
			}
            function document_onreadystatechange() 
            {
	            doStartApplication();
            }
		</script>
        <script type="text/javascript" language="javascript" for="document" event="onreadystatechange">
            return document_onreadystatechange()
        </script>
    </head>
	<body></body>
</html>
