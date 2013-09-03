
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sunshineData_houseAveragePrice_compute.aspx.vb" Inherits="Xydc.Platform.web.sunshineData_houseAveragePrice_compute" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register Assembly="DateTimePickerControls" Namespace="DateTimePickerControls"  TagPrefix="DTP" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>楼盘均价查询</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../../filecss/styles01.css" type="text/css" rel="stylesheet">
    <style>
        TD.grdObjectsLocked
        { ;LEFT:expression(divObjects.scrollLeft);POSITION:relative}
        TH.grdObjectsLocked
        { ;LEFT:expression(divObjects.scrollLeft);POSITION:relative}
        TH
        {
            z-index: 10;
            position: relative;
        }
        TH.grdObjectsLocked
        {
            z-index: 99;
        }
    </style>

    <script src="../../../scripts/transkey.js"></script>
		 <script language="javascript" src="../../../scripts/Calendar.js" type="text/javascript"></script>
    <script language="javascript">
		<!--
			function window_onresize() 
			{
				var dblHeight = 0;
				var strHeight = "";
				var dblDeltaY = 30;
				
				if (document.all("divObjects") == null)
					return;
				
				dblHeight = 300 + dblDeltaY + document.body.clientHeight - 570; //default state : 300px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				//divObjects.style.width  = "100%";
				//divObjects.style.height = strHeight;
				//divObjects.style.clip = "rect(0px " + strHeight + " 0px)";
			}
			function document_onreadystatechange() 
			{
				return window_onresize();
			}
		//-->
    </script>

    <script language="javascript" for="document" event="onreadystatechange">
		<!--
			return document_onreadystatechange()
		//-->
    </script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" onresize="return window_onresize()"
    background="../../../../images/oabk.gif">
    <form id="frmGWDM_JJCD" method="post" runat="server">
    <asp:Panel ID="panelMain" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="4" height="5"></td>                
            </tr>
           
            <tr>
                <td width="5">
                </td>
                <td align="center" style="border-bottom: #99cccc 2px solid">
                    <table cellspacing="0" cellpadding="0"  border="0">
                        <tr>
                            <td valign="middle" align="center" width="100"><asp:LinkButton ID="lnkMLAddNew" runat="server" Font-Name="宋体" Font-Size="12px"><img border="0" height="16" src="../../../images/new.gif" width="16">增加</img></asp:LinkButton></td>                            
                            <td valign="middle" align="center" width="100"><asp:LinkButton ID="lnkMLDelete" runat="server" Font-Name="宋体" Font-Size="12px"><img border="0" height="16" src="../../../images/delete.gif" width="16">删除</img></asp:LinkButton></td>
                            <td valign="middle" align="center" width="100"><asp:LinkButton ID="lnkMLRefresh" runat="server" Font-Name="宋体" Font-Size="12px"><img border="0" height="16" src="../../../images/refresh.ico" width="16">刷新数据</img></asp:LinkButton></td>                           
                            <td valign="middle" align="center" width="100"><asp:LinkButton ID="lnkMLClose" runat="server" Font-Name="宋体" Font-Size="12px"><img alt="返回上级" border="0" height="16" src="../../../images/CLOSE.GIF" width="16">返回上级</img></asp:LinkButton></td>
                         </tr>
                         <tr>
                            <td class="label"  colspan="2" align="center" valign="middle" width="220">楼盘名称：                            
                                <asp:TextBox ID="txtHouse" Width="90" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="12" CssClass="textbox"></asp:TextBox>
                                <span class="label" style="color: red">*</span>                            
                                <asp:LinkButton ID="LnkMLSeek" runat="server" Font-Name="宋体" Font-Size="12px">查找</asp:LinkButton>
                            </td>                        
                             <td class="label"  colspan="2" align="center" valign="middle" width="220">项目名称：                            
                                <asp:TextBox ID="txtProject" Width="90" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="12" CssClass="textbox"></asp:TextBox>
                                <span class="label" style="color: red">*</span>                            
                                <asp:LinkButton ID="LnkMLSeek_1" runat="server" Font-Name="宋体" Font-Size="12px">查找</asp:LinkButton>
                            </td>                        
                             <td class="label"  colspan="2" align="center" valign="middle" width="220">预售证：                            
                                <asp:TextBox ID="txtPresellId" Width="90" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="12" CssClass="textbox"></asp:TextBox>
                                <span class="label" style="color: red">*</span>                            
                                <asp:LinkButton ID="lnkMLSeek_2" runat="server" Font-Name="宋体" Font-Size="12px">查找</asp:LinkButton>
                            </td>  
                         </tr>
                    </table>
                </td>
                <td width="5"></td>
            </tr>
            <tr>
                <td colspan="3" height="2"></td>
            </tr>
            <tr>
                <td width="5"></td>
                <td valign="top" align="center">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="tips" align="left" colspan="4"><asp:LinkButton ID="lnkBlank" runat="server" Width="0px" Height="5px"></asp:LinkButton></td>
                        </tr>                        
                        <tr>
                            <td width="5"></td>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <div id="divProject" style="border: 1px solid #99cccc; table-layout: fixed; overflow: auto; width: 300px; clip: rect(0px 580px 190px 0px); height: 300px">
                                                  <asp:DataGrid ID="grdProject" runat="server" Width="280px" CssClass="labelGrid"
                                                    AllowPaging="false" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
                                                    PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"
                                                    UseAccessibleHeader="True" BorderStyle="Solid">
                                                    <SelectedItemStyle Font-Bold="False" VerticalAlign="top" ForeColor="blue"></SelectedItemStyle>
                                                    <EditItemStyle BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
                                                    <AlternatingItemStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="White"></AlternatingItemStyle>                                                        
                                                    <ItemStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
                                                    <HeaderStyle CssClass="FixedHead" Font-Bold="True" ForeColor="White" VerticalAlign="top" BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
                                                    <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                                    <Columns>
                                                    
														<asp:TemplateColumn HeaderText="选">
															<HeaderStyle HorizontalAlign="Center" Width="20px" ForeColor="White" Font-Size="14px"></HeaderStyle>
															<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
															<ItemTemplate>
																<asp:CheckBox id="chkInformation" runat="server" AutoPostBack="False"></asp:CheckBox>
															</ItemTemplate>
														</asp:TemplateColumn>                                                    
                                                    
                                                        <asp:ButtonColumn Visible="false" DataTextField="C_ID" SortExpression="C_ID" HeaderText="C_ID" CommandName="Select">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="C_XZQY" SortExpression="C_XZQY"
                                                            HeaderText="区域" CommandName="Select">
                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="200px" DataTextField="C_XM_NAME" SortExpression="C_XM_NAME"
                                                            HeaderText="项目名称" CommandName="Select">
                                                            <HeaderStyle Width="200px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="100px" DataTextField="C_HOUSE" SortExpression="C_HOUSE"
                                                            HeaderText="楼盘名称" CommandName="Select">
                                                            <HeaderStyle Width="100px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="100px" DataTextField="C_XM_ID" SortExpression="C_XM_ID"
                                                            HeaderText="预售证" CommandName="Select">
                                                            <HeaderStyle Width="100px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="TYPENAME" SortExpression="TYPENAME"
                                                            HeaderText="类型" CommandName="Select">
                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="0px" Visible="false" DataTextField="C_XM_ADDRESS"
                                                            SortExpression="C_XM_ADDRESS" HeaderText="项目地址" CommandName="Select">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                    </Columns>
                                                    <PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页"
                                                        HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue">
                                                    </PagerStyle>
                                                </asp:DataGrid><input id="Hidden2" type="hidden" value="0" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10px"> </td>
                                    </tr> 
                                    <tr>
                                        <td height="50px" class="label" align="center" style="border-right: #99cccc 1px solid; border-top: #99cccc 1px solid;border-left: #99cccc 1px solid; border-bottom: #99cccc 1px solid" >
                                            <table cellspacing="0" cellpadding="0" border="0">                                                
                                                <tr>
                                                    <td height="10px"> </td>
                                                </tr>                            
                                                <tr>
                                                    <td class="label" align="center" colspan="2"><b>楼盘配置</b> </td>                                                   
                                                </tr>
                                                <tr>
                                                    <td class="label" align="center" colspan="2" height="10px"> </td>                                                               
                                                </tr>
                                                <tr>
                                                    <td class="label" align="center" colspan="2">
                                                        <asp:Button ID="btnSave" runat="server" Font-Size="12px" Font-Name="宋体" Width="96px" Height="24px" CssClass="button" Text="保存"></asp:Button>&nbsp;&nbsp;                                                                        
                                                        <asp:Button ID="btnCancel" runat="server" Font-Size="12px" Font-Name="宋体" Width="96px" Height="24px" CssClass="button" Text="取消"></asp:Button>
                                                     </td> 
                                                </tr>
                                                <tr>
                                                    <td class="label" align="center" colspan="2" height="10px"> </td>                                                               
                                                </tr>                                               
                                            </table>
                                        </td>                                    
                                    </tr>                                    
                                    <tr>
                                        <td height="10px"> </td>
                                    </tr>                                    
                                    <tr>
                                        <td height="200px" class="label" align="center" style="border-right: #99cccc 1px solid; border-top: #99cccc 1px solid;border-left: #99cccc 1px solid; border-bottom: #99cccc 1px solid" >
                                            <table cellspacing="0" cellpadding="0" border="0">                                                
                                                <tr>
                                                    <td class="label" align="center" colspan="2"><b>楼盘均价查询</b> </td>                                                   
                                                </tr>
                                                <tr>
                                                    <td class="label" align="center" colspan="2" height="10px"> </td>                                                               
                                                </tr>
                                                <tr>
									                <td class="labelNotNull"  align="right" >日期：</TD>
									                <td class="label" align="left"><asp:textbox onfocus="calendar()" id="txtStartDate" runat="server" CssClass="textbox" Columns="9"></asp:textbox>~<asp:textbox onfocus="calendar()" id="txtEndDate" runat="server" CssClass="textbox" Columns="9"></asp:textbox><span class="label" style="color: red"> *</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="labelNotNull" align="right"> 楼盘：</td>
                                                    <td class="label" align="left"><asp:TextBox ID="txtBuildingName" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="25" CssClass="textbox"></asp:TextBox> <span class="label" style="color: red">*</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="labelNotNull"  align="right">类型：</td>
                                                    <td>
                                                         <asp:RadioButtonList id="rblHouseType" Runat="server" CssClass="textbox" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
							                                            <asp:ListItem Value="0" Selected="True">洋房</asp:ListItem>
							                                            <asp:ListItem Value="1" >别墅</asp:ListItem>
							                             </asp:RadioButtonList>
                                                    </td>
                                                </tr>                                             
                                                <tr>
                                                    <td class="labelNotNull" align="right"> 均价：</td>
                                                    <td class="label" align="left"><asp:TextBox ID="txtAveragePrice" runat="server" Font-Size="12px" Font-Name="宋体"  Columns="25" CssClass="textbox"></asp:TextBox> <span class="label" style="color: red">*</span></td>                                        
                                                </tr>   
                                                <tr>
                                                    <td class="label" align="center" colspan="2" height="10px"> </td>                                                               
                                                </tr>
                                                <tr>
                                                    <td class="label" align="center" colspan="2">
                                                        <asp:Button ID="btnQuery" runat="server" Font-Size="12px" Font-Name="宋体" Width="96px" Height="24px" CssClass="button" Text="查询"></asp:Button>&nbsp;&nbsp;                                                                        
                                                     </td> 
                                                </tr>                                             
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 5px;"></td>
                            <td align="left" valign="top" style="height: 100%">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <div id="divHOUSEMATCH" style="border-right: #99cccc 1px solid; table-layout: fixed;border-top: #99cccc 1px solid; overflow: auto; border-left: #99cccc 1px solid;
                                                width: 580px; clip: rect(0px 580px 300px 0px); border-bottom: #99cccc 1px solid; height: 300px">
                                                <asp:DataGrid ID="grdHOUSEMATCH" runat="server"  Width="560px" CssClass="labelGrid"
                                                    AllowPaging="True" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
                                                    PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"
                                                    UseAccessibleHeader="True" BorderStyle="Solid">
                                                    <SelectedItemStyle Font-Bold="False" VerticalAlign="top" ForeColor="blue"></SelectedItemStyle>
                                                    <EditItemStyle BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
                                                    <AlternatingItemStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top"
                                                        BackColor="White"></AlternatingItemStyle>
                                                    <ItemStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top"
                                                        BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
                                                    <HeaderStyle CssClass="FixedHead" Font-Bold="True" ForeColor="White" VerticalAlign="top"
                                                        BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
                                                    <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                                    <Columns>
                                                        <asp:ButtonColumn Visible="false" DataTextField="C_ID" SortExpression="C_ID" HeaderText="C_ID"   CommandName="Select">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="C_XZQY" SortExpression="C_XZQY"
                                                            HeaderText="区域" CommandName="Select">
                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="200px" DataTextField="C_XM_NAME" SortExpression="C_XM_NAME"
                                                            HeaderText="项目名称" CommandName="Select">
                                                            <HeaderStyle Width="200px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="100px" DataTextField="C_HOUSE" SortExpression="C_HOUSE"
                                                            HeaderText="楼盘名称" CommandName="Select">
                                                            <HeaderStyle Width="100px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="100px" DataTextField="C_XM_ID" SortExpression="C_XM_ID"
                                                            HeaderText="预售证" CommandName="Select">
                                                            <HeaderStyle Width="100px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="TYPENAME" SortExpression="TYPENAME"
                                                            HeaderText="类型" CommandName="Select">
                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn Visible="false" ItemStyle-Width="0px"  DataTextField="C_XM_ADDRESS"  SortExpression="C_XM_ADDRESS" HeaderText="项目地址" CommandName="Select">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                    </Columns>
                                                    <PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页"
                                                        HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue">
                                                    </PagerStyle>
                                                </asp:DataGrid><input id="Hidden1" type="hidden" value="0" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td class="label">
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tr align="center">
                                                    <td class="labelBlack" valign="middle" align="left">
                                                        <asp:LinkButton ID="lnkCZMoveFirst" runat="server" CssClass="labelBlack">最前</asp:LinkButton>
                                                    </td>
                                                    <td class="labelBlack" valign="middle" align="left">
                                                        <asp:LinkButton ID="lnkCZMovePrev" runat="server" CssClass="labelBlack">前页</asp:LinkButton>
                                                    </td>
                                                    <td class="labelBlack" valign="middle" align="left">
                                                        <asp:LinkButton ID="lnkCZMoveNext" runat="server" CssClass="labelBlack">下页</asp:LinkButton>
                                                    </td>
                                                    <td class="labelBlack" valign="middle" align="left">
                                                        <asp:LinkButton ID="lnkCZMoveLast" runat="server" CssClass="labelBlack">最后</asp:LinkButton>
                                                    </td>
                                                    <td class="labelBlack" valign="middle" align="left">
                                                        <asp:LinkButton ID="lnkCZGotoPage" runat="server" CssClass="labelBlack">前往</asp:LinkButton><asp:TextBox
                                                            ID="txtPageIndex" runat="server" Font-Size="12px" Font-Name="宋体" Columns="2"
                                                            CssClass="textbox">1</asp:TextBox>页
                                                    </td>
                                                    <td class="labelBlack" valign="middle" align="left">
                                                        <asp:LinkButton ID="lnkCZSetPageSize" runat="server" CssClass="labelBlack">每页</asp:LinkButton><asp:TextBox  ID="txtPageSize" runat="server" Font-Size="12px" Font-Name="宋体" Columns="3" CssClass="textbox">30</asp:TextBox>条
                                                    </td>
                                                    <td class="labelBlack" valign="middle" align="right">
                                                        <asp:Label ID="lblGridLocInfo" runat="server" Font-Size="12px" CssClass="labelBlack">1/10 N/15</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <div id="divObjects" style="border-right: #99cccc 1px solid; table-layout: fixed;
                                                border-top: #99cccc 1px solid; overflow: auto; border-left: #99cccc 1px solid;
                                                width: 580px; clip: rect(0px 300px 300px 0px); border-bottom: #99cccc 1px solid;
                                                height: 270px">
                                                <asp:DataGrid ID="grdObjects" runat="server" runat="server" Width="560px" CssClass="labelGrid"
                                                    AllowPaging="false" AutoGenerateColumns="False" GridLines="Both" BackColor="White"
                                                    PageSize="30" BorderColor="#dfdfdf" BorderWidth="1px" AllowSorting="True" CellPadding="4"
                                                    UseAccessibleHeader="True" BorderStyle="Solid">
                                                    <SelectedItemStyle Font-Bold="False" VerticalAlign="top" ForeColor="blue"></SelectedItemStyle>
                                                    <EditItemStyle BackColor="#FFCC00" VerticalAlign="top"></EditItemStyle>
                                                    <AlternatingItemStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top"
                                                        BackColor="White"></AlternatingItemStyle>
                                                    <ItemStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="top"
                                                        BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
                                                    <HeaderStyle CssClass="FixedHead" Font-Bold="True" ForeColor="White" VerticalAlign="top"
                                                        BackColor="#6699cc" HorizontalAlign="Left"></HeaderStyle>
                                                    <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                                    <Columns>
                                                        <asp:ButtonColumn Visible="false" DataTextField="C_ID" SortExpression="C_ID" HeaderText="C_ID"
                                                            CommandName="Select">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="C_XZQY" SortExpression="C_XZQY"
                                                            HeaderText="区域" CommandName="Select">
                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="200px" DataTextField="C_XM_NAME" SortExpression="C_XM_NAME"
                                                            HeaderText="项目名称" CommandName="Select">
                                                            <HeaderStyle Width="200px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="100px" DataTextField="C_HOUSE" SortExpression="C_HOUSE"
                                                            HeaderText="楼盘名称" CommandName="Select">
                                                            <HeaderStyle Width="100px"></HeaderStyle>
                                                        </asp:ButtonColumn>                                                        
                                                        <asp:ButtonColumn ItemStyle-Width="100px" DataTextField="C_XM_ID" SortExpression="C_XM_ID"
                                                            HeaderText="预售证" CommandName="Select">
                                                            <HeaderStyle Width="100px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                        <asp:ButtonColumn ItemStyle-Width="80px" DataTextField="TYPENAME" SortExpression="TYPENAME"
                                                            HeaderText="类型" CommandName="Select">
                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                        </asp:ButtonColumn>                                                        
                                                        <asp:ButtonColumn ItemStyle-Width="0px" Visible="false" DataTextField="C_XM_ADDRESS"
                                                            SortExpression="C_XM_ADDRESS" HeaderText="项目地址" CommandName="Select">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                        </asp:ButtonColumn>
                                                    </Columns>
                                                    <PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页"
                                                        HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue">
                                                    </PagerStyle>
                                                </asp:DataGrid><input id="htxtOBJECTSFixed" type="hidden" value="0" runat="server">
                                            </div> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" height="5"></td>
                        </tr>
                    </table>
                </td>
                <td width="5px"></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelError" runat="server">
        <table id="tabErrMain" height="98%" cellspacing="0" cellpadding="0" width="100%"
            border="0">
            <tr>
                <td width="5%">
                </td>
                <td>
                    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td id="tdErrInfo" style="font-size: 32pt; color: black; line-height: 40pt; font-family: 宋体;
                                letter-spacing: 2pt" align="center">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label><p>
                                    &nbsp;&nbsp;</p>
                                <p>
                                    <input type="button" id="btnGoBack" value=" 返回 " style="font-size: 24pt; font-family: 宋体"
                                        onclick="javascript:history.back();"></p>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table cellspacing="0" cellpadding="0" align="center" border="0">
        <tr>
            <td>
                <input id="htxtCurrentPage" type="hidden" runat="server">
                <input id="htxtCurrentRow" type="hidden" runat="server">
                <input id="htxtEditMode" type="hidden" runat="server">
                <input id="htxtEditType" type="hidden" runat="server">
                <input id="htxtQuery" type="hidden" runat="server">
                <input id="htxtRows" type="hidden" runat="server">
                <input id="htxtSort" type="hidden" runat="server">
                <input id="htxtSortColumnIndex" type="hidden" runat="server">
                <input id="htxtSortType" type="hidden" runat="server">
                <input id="htxtDivLeftObject" type="hidden" runat="server">
                <input id="htxtDivTopObject" type="hidden" runat="server">
                <input id="htxtDivLeftBody" type="hidden" runat="server">
                <input id="htxtDivTopBody" type="hidden" runat="server">
            </td>
        </tr>
        <tr>
            <td>

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
							function ScrollProc_divObjects() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopObject");
								if (oText != null) oText.value = divObjects.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) oText.value = divObjects.scrollLeft;
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

								oText=null;
								oText=document.getElementById("htxtDivTopObject");
								if (oText != null) divObjects.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) divObjects.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divObjects.onscroll = ScrollProc_divObjects;
							}
							catch (e) {}
                </script>

            </td>
        </tr>
        <tr>
            <td>

                <script language="javascript">window_onresize();</script>

                <uwin:PopMessage ID="popMessageObject" runat="server" Height="48px" Width="96px"
                    Visible="False" ActionType="OpenWindow" PopupWindowType="Normal" EnableViewState="False">
                </uwin:PopMessage>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
