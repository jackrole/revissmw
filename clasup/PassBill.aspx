<%@ Page language="c#" Codebehind="PassBill.aspx.cs" AutoEventWireup="True" Inherits="Export.Clasup.PassBill" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ƿ�֪ͨ��</title>
		<LINK href="../css/main.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Literal ID="lRKD" Runat="server"></asp:Literal>
			<asp:Literal ID="pSonghuo" Runat="server"></asp:Literal>
		<table class="notptn" border="0" width="200" align="center">
			<tr>
				<td align="center">
                    <asp:Button runat="server" ID="btnXLS" Text="����XLS" onclick="btnXLS_Click" />
					<input type="button" onclick="window.print();;" value="��ӡ" class="button">&nbsp;&nbsp;&nbsp;
					<input type="button" onclick="window.close()" value="�ر�" class="button">
				</td>
			</tr>
		</table>
		</form>
	</body>
</HTML>
