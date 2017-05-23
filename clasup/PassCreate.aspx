<%@ Page language="c#" Codebehind="PassCreate.aspx.cs" AutoEventWireup="True" Inherits="Export.Clasup.PassCreate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>待放行录单</title>
		<script type="text/javascript" src="../js/jquery.js"></script>
		<script type="text/javascript" src="../js/jquery.bgiframe.min.js"></script>
		<script type="text/javascript" src="../js/thickbox-compressed.js"></script>
		<script type="text/javascript" src="../js/jquery.cookie.js"></script>
		<script type="text/javascript" src="../js/jquery.autocomplete.pack.js"></script>
		<script type="text/javascript" src="../js/g.js"></script>
		<script type="text/javascript" src="../js/setday.js"></script>
		<script type="text/javascript" src="js.js"></script>
		<link type="text/css" rel="stylesheet" href="../css/main.css" />
		<link type="text/css" rel="stylesheet" href="../js/jquery.autocomplete.css" />
		<link type="text/css" rel="stylesheet" href="../js/thickbox.css" />
	</head>
	<body scroll="auto">
		<form id="Form1" method="post" enctype="multipart/form-data" runat="server">
		<asp:Literal Runat="server" ID="jsLoad"></asp:Literal>
			<div style="MARGIN-TOP: 15px" align="center">
				<h3>待放行录单</h3>
			</div>
            <asp:textbox id="hFile" style="DISPLAY: none" Runat="server"></asp:textbox>
			<asp:textbox id="SigleTestID" style="DISPLAY:none" Runat="server"></asp:textbox>
			<table cellspacing="0" cellpadding="0" width="750" align="center" border="0">
				<tr>
					<td align="right">移库单号：</td>
					<td><asp:textbox id="iptPassNo" Width="180px" Runat="server"></asp:textbox><asp:button id="btnSearch" Runat="server" Text="查询" onclick="btnSearch_Click"></asp:button></td>
					<td align="right">添加日期：</td>
					<td><asp:textbox id="iptPassTime" Width="180px" Runat="server" Enabled="false"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right">箱数：</td>
					<td><asp:textbox id="iptBoxNum" Width="180px" Runat="server"></asp:textbox></td>
					<td align="right">件数：</td>
					<td><asp:textbox id="iptPcs" Width="180px" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right">净重：</td>
					<td><asp:textbox id="iptCw" Width="180px" Runat="server"></asp:textbox>
                    </td>
					<td align="right">毛重：</td>
					<td><asp:textbox id="iptNw" Width="180px" Runat="server"></asp:textbox>
                    </td>
				</tr>
				<tr>
					<td align="right">金额：</td>
					<td><asp:textbox id="iptPay" Width="180px" Runat="server"></asp:textbox></td>
					<td align="right">币种：</td>
					<td><asp:textbox id="iptCurrKind" Width="180px" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right">客户名称：</td>
					<td><asp:DropDownList id="ddlCid" Runat="server"></asp:DropDownList></td>
					<td align="right">货物属性：</td>
                    <td><asp:DropDownList Runat="server" ID="ddlIsUnderBounded">
                    <asp:ListItem Value="1" Text="保税"></asp:ListItem>
                    <asp:ListItem Value="0" Text="国内货物"></asp:ListItem>
                    </asp:DropDownList></td>
				</tr>
				<tr>
					<td align="right">状态：</td>
					<td><font color="blue"><asp:CheckBox id="cb1" Runat="server" Text="已移库"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb2" Runat="server" Text="已报关" Enabled="False"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb3" Runat="server" Text="已提交海关" Enabled="False"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb4" Runat="server" Text="已放行" Enabled="False"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb5" Runat="server" Text="已移库" Enabled="False"></asp:CheckBox></font></td>
                    <td colspan="2"><asp:button id="btnBGD" runat="server" Text="生成报关单" onclick="btnBGD_Click"></asp:button>
                        <asp:button id="Button1" runat="server" Text="一键删除文件" onclick="Button1_Click"></asp:button></td>
				</tr>
				<tr>
					<td align="right">待放行清单：</td>
					<td><input id="tFile" type="file" size="25" name="tFile" runat="server" /></td>
					<td colspan="2"><asp:button id="btnLock" Visible="false" runat="server" Text="解 锁" onclick="btnLock_Click"></asp:button>
                    <asp:button id="btnSave" runat="server" Text="保存" onclick="btnSave_Click"></asp:button>
                    </td>
				</tr>
                <tr>
                <td align="right"><font color="blue">承诺事项：</font></td>
					<td><font color="blue"><asp:CheckBox id="PI_SPECIAL" Runat="server" Text="特殊关系确认"></asp:CheckBox>
                    <asp:CheckBox id="PI_PRICE" Runat="server" Text="价格影响确认"></asp:CheckBox>
                    <asp:CheckBox id="PI_PAY" Runat="server" Text="支付特许确认"></asp:CheckBox></font></td>
                </tr>
			</table>
            <div style="margin:0 auto;width:750px;">
                <fieldset style="WIDTH: 600px"><legend>待放行清单格式</legend>
                <br /><a href="../xls/pass.xlsx">模板下载</a><br />
                <table width="95%" border="1" cellpadding="3" cellspacing="0" bordercolorlight="#aaaaaa" bordercolordark="#ffffff" align="center" class="tblhead">
				<tr class="trhead" align="center"><td>料号</td><td>件数</td><td>毛重</td><td>净重</td><td>金额</td><td>币种</td><td>国家</td><td>箱号</td><td>颜色</td><td>尺寸</td></tr></table>
                </fieldset>
                <div style="margin-left:20px;width:650px">
                <br /><iframe name="fflist" id="fflist" src="" frameborder="0" width="99%" height="150" align="middle"></iframe>
                </div>
            </div>
			<br />
		</form>
		<script type="text/javascript">
		window.returnValue=1;
		$(function() {
		    var no = $("#iptPassNo").val();
			if (no != null && no != "") {
			    url = "../public/DataContainer.aspx?Obj=passno&p=" + no + "&tt=" + new Date();
				$("#fflist").get(0).src = url;
			}
		});
		</script>
	</body>
</html>
