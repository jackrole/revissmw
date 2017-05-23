<%@ Page language="c#" Codebehind="PassBill.aspx.cs" AutoEventWireup="True" Inherits="Export.Clasup.PassBill" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>移库通知单</title>
		<LINK href="../css/main.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Literal ID="lRKD" Runat="server"></asp:Literal>
			<asp:Literal ID="pSonghuo" Runat="server"></asp:Literal>
		<table class="notptn" border="0" width="200" align="center">
			<tr>
				<td align="center">
                    <asp:Button runat="server" ID="btnXLS" Text="导出XLS" onclick="btnXLS_Click" />
					<input type="button" onclick="window.print();;" value="打印" class="button">&nbsp;&nbsp;&nbsp;
					<input type="button" onclick="window.close()" value="关闭" class="button">
				</td>
			</tr>
		</table>
		</form>
	</body>
</HTML>
