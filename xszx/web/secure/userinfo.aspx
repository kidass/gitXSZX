<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userinfo.aspx.vb" Inherits="Xydc.Platform.web.userinfo" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>用户信息显示窗</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
        <meta content="JavaScript" name="vs_defaultClientScript" />
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
        <LINK href="../filecss/styles.css" type="text/css" rel="stylesheet">
        <script src="../scripts/transkey.js"></script>
        <script language="javascript" id="clientEventHandlersJS">
            function document_onreadystatechange() 
            {
                try {
                    var txtRYDM = document.getElementById("txtRYDM");
                    txtRYDM.focus(); 
                } catch (e) {}
            }
        </script>
        <script language="javascript" for="document" event="onreadystatechange">
            return document_onreadystatechange()
        </script>
    </HEAD>
    <body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../images/oabk.gif">
        <form id="frmLogin" method="post" runat="server">
            <asp:panel id="panelInformation" Runat="server">
                <TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <TR>
                        <TD align="center">
                            <TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
                                <TR>
                                    <TD width="5%"></TD>
                                    <td>
                                        <TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
                                            <tr>
                                                <TD class="title" align="center" height="28">使用人员信息一览表</TD> 
                                                <TD align="right" width="20%"><asp:Button id="btnReturn" Runat="server" Font-Name="宋体" Font-Size="11pt" Text=" 返  回 " CssClass="button"></asp:Button></TD>
                                            </tr>
                                        </table>
                                    </td>
                                    <TD width="5%"></TD>
                                </TR>
                                <TR>
                                    <TD width="5%"></TD>
                                    <TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid" vAlign="top" align="center">
                                        <TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            <TR>
                                                <TD colSpan="2" height="10"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">人员代码：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtRYDM" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="16" name="txtRYDM" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">人员名称：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtRYMC" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="16" name="txtRYMC" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">人员真名：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtRYZM" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="16" name="txtRYZM" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">所在单位或部门名称：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtZZMC" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="48" name="txtZZMC" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">排序号：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtRYXH" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="10" name="txtRYXH" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">行政级别：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtJBMC" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="16" name="txtJBMC" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">配备秘书：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtMSMC" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="16" name="txtMSMC" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">联系电话：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtLXDH" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="48" name="txtLXDH" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">移动电话：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtSJHM" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="30" name="txtSJHM" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">电子邮箱：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtYXDZ" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="48" name="txtYXDZ" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">流转时可看见您真名的人员：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtKCKRY" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="48" name="txtKCKRY" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">其他人员系统显示为：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtJJXSMC" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="16" name="txtJJXSMC" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">流转时可直接送给您的人员：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtKZSRY" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="48" name="txtKZSRY" runat="server"></TD>
                                            </TR>
                                            <TR>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="right" height="26">其他人员通过：</TD>
                                                <TD style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" align="left"><INPUT id="txtQTYZS" style="FONT-SIZE: 11pt; FONT-FAMILY: 宋体" type="text" size="16" name="txtQTYZS" runat="server">&nbsp;转送</TD>
                                            </TR>
                                            <TR>
                                                <TD colSpan="2" height="10"></TD>
                                            </TR>
                                        </TABLE>
                                    </TD>
                                    <TD width="5%"></TD>
                                </TR>
                                <TR>
                                    <TD width="5%"></TD>
                                    <TD class="title" align="center" height="28">现任职务情况一览表</TD>
                                    <TD width="5%"></TD>
                                </TR>
                                <TR>
                                    <TD width="5%"></TD>
                                    <TD style="BORDER-RIGHT: #6699cc 1px solid; BORDER-TOP: #6699cc 1px solid; BORDER-LEFT: #6699cc 1px solid; BORDER-BOTTOM: #6699cc 1px solid" align="center">
                                        <TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            <TR>
                                                <TD><asp:CheckBoxList id="chklstGW" Runat="server" CssClass="textbox" RepeatDirection="Horizontal" Width="100%" RepeatLayout="Table" RepeatColumns="6" Enabled="False"></asp:CheckBoxList></TD>
                                            </TR>
                                        </TABLE>
                                    </TD>
                                    <TD width="5%"></TD>
                                </TR>
                            </TABLE>
                        </TD>
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