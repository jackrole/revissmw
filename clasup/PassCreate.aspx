<%@ Page language="c#" Codebehind="PassCreate.aspx.cs" AutoEventWireup="True" Inherits="Export.Clasup.PassCreate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>������¼��</title>
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
				<h3>������¼��</h3>
			</div>
            <asp:textbox id="hFile" style="DISPLAY: none" Runat="server"></asp:textbox>
			<asp:textbox id="SigleTestID" style="DISPLAY:none" Runat="server"></asp:textbox>
			<table cellspacing="0" cellpadding="0" width="750" align="center" border="0">
				<tr>
					<td align="right">�ƿⵥ�ţ�</td>
					<td><asp:textbox id="iptPassNo" Width="180px" Runat="server"></asp:textbox><asp:button id="btnSearch" Runat="server" Text="��ѯ" onclick="btnSearch_Click"></asp:button></td>
					<td align="right">������ڣ�</td>
					<td><asp:textbox id="iptPassTime" Width="180px" Runat="server" Enabled="false"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right">������</td>
					<td><asp:textbox id="iptBoxNum" Width="180px" Runat="server"></asp:textbox></td>
					<td align="right">������</td>
					<td><asp:textbox id="iptPcs" Width="180px" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right">���أ�</td>
					<td><asp:textbox id="iptCw" Width="180px" Runat="server"></asp:textbox>
                    </td>
					<td align="right">ë�أ�</td>
					<td><asp:textbox id="iptNw" Width="180px" Runat="server"></asp:textbox>
                    </td>
				</tr>
				<tr>
					<td align="right">��</td>
					<td><asp:textbox id="iptPay" Width="180px" Runat="server"></asp:textbox></td>
					<td align="right">���֣�</td>
					<td><asp:textbox id="iptCurrKind" Width="180px" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right">�ͻ����ƣ�</td>
					<td><asp:DropDownList id="ddlCid" Runat="server"></asp:DropDownList></td>
					<td align="right">�������ԣ�</td>
                    <td><asp:DropDownList Runat="server" ID="ddlIsUnderBounded">
                    <asp:ListItem Value="1" Text="��˰"></asp:ListItem>
                    <asp:ListItem Value="0" Text="���ڻ���"></asp:ListItem>
                    </asp:DropDownList></td>
				</tr>
				<tr>
					<td align="right">״̬��</td>
					<td><font color="blue"><asp:CheckBox id="cb1" Runat="server" Text="���ƿ�"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb2" Runat="server" Text="�ѱ���" Enabled="False"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb3" Runat="server" Text="���ύ����" Enabled="False"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb4" Runat="server" Text="�ѷ���" Enabled="False"></asp:CheckBox>
                    &nbsp;<asp:CheckBox id="cb5" Runat="server" Text="���ƿ�" Enabled="False"></asp:CheckBox></font></td>
                    <td colspan="2"><asp:button id="btnBGD" runat="server" Text="���ɱ��ص�" onclick="btnBGD_Click"></asp:button>
                        <asp:button id="Button1" runat="server" Text="һ��ɾ���ļ�" onclick="Button1_Click"></asp:button></td>
				</tr>
				<tr>
					<td align="right">�������嵥��</td>
					<td><input id="tFile" type="file" size="25" name="tFile" runat="server" /></td>
					<td colspan="2"><asp:button id="btnLock" Visible="false" runat="server" Text="�� ��" onclick="btnLock_Click"></asp:button>
                    <asp:button id="btnSave" runat="server" Text="����" onclick="btnSave_Click"></asp:button>
                    </td>
				</tr>
                <tr>
                <td align="right"><font color="blue">��ŵ���</font></td>
					<td><font color="blue"><asp:CheckBox id="PI_SPECIAL" Runat="server" Text="�����ϵȷ��"></asp:CheckBox>
                    <asp:CheckBox id="PI_PRICE" Runat="server" Text="�۸�Ӱ��ȷ��"></asp:CheckBox>
                    <asp:CheckBox id="PI_PAY" Runat="server" Text="֧������ȷ��"></asp:CheckBox></font></td>
                </tr>
			</table>
            <div style="margin:0 auto;width:750px;">
                <fieldset style="WIDTH: 600px"><legend>�������嵥��ʽ</legend>
                <br /><a href="../xls/pass.xlsx">ģ������</a><br />
                <table width="95%" border="1" cellpadding="3" cellspacing="0" bordercolorlight="#aaaaaa" bordercolordark="#ffffff" align="center" class="tblhead">
				<tr class="trhead" align="center"><td>�Ϻ�</td><td>����</td><td>ë��</td><td>����</td><td>���</td><td>����</td><td>����</td><td>���</td><td>��ɫ</td><td>�ߴ�</td></tr></table>
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
