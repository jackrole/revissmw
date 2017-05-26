<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomCountList.aspx.cs"
    Inherits="Export.Clasup.CustomCountList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataGrid ID="gList" HorizontalAlign="Center" Runat="server" CssClass="prtTab"
            BorderWidth="1" CellPadding="0" CellSpacing="0" BorderColor="#000000" AutoGenerateColumns="False">
            <headerstyle cssclass="gtop"></headerstyle>
            <itemstyle cssclass="gbody"></itemstyle>
            <columns>
				<asp:TemplateColumn HeaderText="项号">
					<ItemTemplate>
						<%# Eval("code")%>
						<br />
						<%# Eval("code")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="商品编号">
					<ItemTemplate>
						<%# Eval("code")%>
						<%# Eval("code")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="商品名称、规格型号">
					<ItemTemplate>
						<%# Eval("code")%>
						<br />
						<%# Eval("code")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="数量及单位">
					<ItemTemplate>
						<%# Eval("code")%><%# Eval("code")%>
						<%# getName("unit", (string)Eval("code"))%>
						<br />
						<%# Eval("code")%><%# Eval("code")%>
						<%# getName("unit", (string)Eval("code"))%>
						<br />
						<%# Eval("code")%><%# Eval("code")%>
						<%# getName("unit", (string)Eval("code"))%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="原产国（地区）">
					<ItemTemplate>
						<%#getName("origin_country", (string)Eval("code"))%>
						<br />
						<%# Eval("code")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<%--<asp:BoundColumn DataField="decl_price" DataFormatString="{0:#.0000}" HeaderText="单价"></asp:BoundColumn>--%>
				<%--<asp:BoundColumn DataField="trade_total" DataFormatString="{0:F}" HeaderText="总价"></asp:BoundColumn>--%>
				<asp:TemplateColumn HeaderText="币制">
					<ItemTemplate>
						<%# getCurr((string)Eval("code"))%>
						<br />
						<%# getName("trade_curr", (string)Eval("code"))%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="征免">
					<ItemTemplate>
						<%# getName("duty_mode", (string)Eval("code"))%>
						<br />
					</ItemTemplate>
				</asp:TemplateColumn>
			</columns>
        </asp:DataGrid>
    </div>
    </form>
</body>
</html>
