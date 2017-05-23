<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JYS.aspx.cs" Inherits="Export.Clasup.JYS" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="Expires" CONTENT="0" />
		<meta http-equiv="Cache-Control" CONTENT="no-cache" />
		<meta http-equiv="Pragma" CONTENT="no-cache" />
		<title>进境货物备案清单</title>
		<style type="text/css">
		.newPage { PAGE-BREAK-AFTER: always;clear:both;height:3px;min-height:3px;max-height:3px;overflow:hidden;}
		.bl { BORDER-LEFT: #000 1px solid }
		.br { BORDER-RIGHT: #000 1px solid }
		.bd { BORDER-BOTTOM: #000 1px solid }
		TD { MARGIN: 0px; FONT-SIZE: 11px }
		.ggTab { BORDER-BOTTOM: #000 1px solid; BORDER-LEFT: #000 1px solid; BORDER-TOP: #000 1px solid; BORDER-RIGHT: #000 1px solid ; WIDTH: 196mm;}
		.prtTab { BORDER-LEFT: #000 1px solid; BACKGROUND-COLOR: #ffffff; BORDER-COLLAPSE: collapse; BORDER-TOP: #000 1px solid; WIDTH: 196mm; }
		#prtTab { border-collapse:collapse; table-layout:fixed; BACKGROUND-COLOR: #ffffff;BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 196mm;  BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;}
		#prtTab TD { BORDER-BOTTOM: #000 1px solid; PADDING-BOTTOM: 0px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 3px; PADDING-RIGHT: 0px; FONT-FAMILY: "宋体"; WHITE-SPACE: nowrap; HEIGHT: 8.5mm; FONT-SIZE: 12px; BORDER-RIGHT: #000 1px solid; PADDING-TOP: 1px }
		.prtTab TD { PADDING-LEFT: 3px; PADDING-TOP: 1px }
		.prtTab TD { BORDER-BOTTOM: #000 1px solid; BORDER-LEFT: medium none; BORDER-TOP: medium none; BORDER-RIGHT: medium none }
		.ggTab TD { PADDING-LEFT: 3px; PADDING-TOP: 1px }
		.gtop TD { PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; HEIGHT: 4mm; FONT-SIZE: 10px; PADDING-TOP: 0px }
		.gbody TD { MARGIN: 0px; HEIGHT: 11mm; MAX-HEIGHT: 11mm; FONT-SIZE: 12px; VERTICAL-ALIGN: top }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="TRANS_PRE_ID" Visible="false" Runat="server"></asp:textbox>
			<asp:Panel Runat="server" ID="pTOP">
				<TABLE border="0" cellSpacing="0" cellPadding="0" align="center">
					<TR>
						<TD style="HEIGHT: 16mm" colSpan="4">&nbsp;</TD>
					</TR>
					<TR>
						<TD width="15"></TD>
						<TD style="HEIGHT: 6mm; FONT-SIZE: 22px" align="center"><B>中华人民共和国海关进境货物备案清单</B></TD>
						<TD style="WIDTH: 6mm"></TD>
						<TD vAlign="top" rowSpan="2">
							<asp:Literal id="Code_2" Runat="server"></asp:Literal></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD style="LETTER-SPACING: 5px" colSpan="2" align="center">
							<asp:Literal id="Code_39" Runat="server"></asp:Literal></TD>
						<TD></TD>
					</TR>
				</TABLE>
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="720" align="center">
					<TR>
						<TD>预录入编号：
							<asp:label id="pre_entry_id" Runat="server"></asp:label></TD>
						<TD>Page 1</TD>
						<TD>海关编号：
							<asp:label id="customs_id" Runat="server"></asp:label></TD>
						<TD>Page 1</TD>
					</TR>
				</TABLE>
				<TABLE id="prtTab" border="1" cellSpacing="0" borderColor="black" cellPadding="0" align="center">
					<tr style="display:none;">
						<td style="width:55mm"></td>
						<td style="width:24mm"></td>
						<td style="width:9.5mm"></td>
						<td style="width:9.5mm"></td>
						<td style="width:7mm"></td>
						<td style="width:15.5mm"></td>
						<td style="width:4mm"></td>
						<td style="width:19mm"></td>
						<td style="width:10mm"></td>
						<td style="width:4.5mm"></td>
						<td style="width:3.5mm"></td>
						<td style="width:31.5mm"></td>
					</tr>
					<TR>
						<TD colSpan="2">进口口岸<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;卢湾监管&nbsp;&nbsp;&nbsp;&nbsp;2211</TD>
						<TD colSpan="5">备案号<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;H22161000002</TD>
						<TD colSpan="4">进口日期<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="i_e_date" Runat="server"></asp:label></TD>
						<TD>申报日期<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="d_date" Runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD colSpan="2">经营单位&nbsp;&nbsp;&nbsp;&nbsp;3122610003<BR />&nbsp;&nbsp;&nbsp;&nbsp;上海中远空港保税物流有限公司</TD>
						<TD colSpan="2">运输方式<BR>
							<div align="center">航空运输</div></TD>
						<TD colSpan="4">运输工具名称<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="traf_name" Runat="server"></asp:label>&nbsp;
							<asp:label id="voyage_no" Runat="server"></asp:label></TD>
						<TD colSpan="4">提运单号<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="bill_no" Runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD colSpan="2">收货单位&nbsp;&nbsp;&nbsp;&nbsp;3122610003<BR />&nbsp;&nbsp;&nbsp;&nbsp;上海中远空港保税物流有限公司</TD>
						<TD colSpan="4">贸易方式<BR>&nbsp;&nbsp;&nbsp;&nbsp;区内物流货物&nbsp;&nbsp;5034</TD>
						<TD colSpan="5">征免性质<BR>
							<asp:Label id="cut_modecc" Runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label id="cut_mode" Runat="server"></asp:Label></TD>
						<TD>征税比例<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="in_radio" Runat="server"></asp:Label>%</TD>
					</TR>
					<TR>
						<TD>许可证号<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lisence_no" Runat="server"></asp:Label></TD>
						<TD colSpan="4">起运国（地区）<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label id="trade_country" Runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label id="trade_countrycc" Runat="server"></asp:Label></TD>
						<TD colSpan="4">装货港<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="distinate_port" Runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label id="distinate_portcc" Runat="server"></asp:Label></TD>
						<TD colSpan="3">境内目的地<BR>
							<div align="center"><asp:Label id="district_code" Runat="server"></asp:Label>&nbsp;<asp:Label id="district_codecc" Runat="server"></asp:Label></div></TD>
					</TR>
					<TR>
						<TD>批准文号<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="appr_no" Runat="server"></asp:Label></TD>
						<TD>成交方式<BR>&nbsp;&nbsp;&nbsp;&nbsp;CIF</TD>
						<TD colSpan="4">运费<BR>&nbsp;&nbsp;</TD>
						<TD colSpan="3">保费<BR>&nbsp;&nbsp;</TD>
						<TD colSpan="3">杂费<BR>&nbsp;&nbsp;</TD>
					</TR>
					<TR>
						<TD>合同协议号<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="contr_no" Runat="server"></asp:Label></TD>
						<TD colSpan="2">件数<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="pack_no" Runat="server"></asp:Label></TD>
						<TD colSpan="3">包装种类<BR>&nbsp;&nbsp;&nbsp;&nbsp;纸箱</TD>
						<TD colSpan="4">毛重（千克）<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="gross_wt" Runat="server"></asp:Label></TD>
						<TD colSpan="2">净重（千克）<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="net_wt" Runat="server"></asp:Label></TD>
					</TR>
					<TR vAlign="top">
						<TD>集装箱号<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="ex_source" Runat="server"></asp:Label></TD>
						<TD colSpan="8">随附单证<BR>
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="DanZ" Runat="server"></asp:Label></TD>
						<TD colSpan="3">用途</TD>
					</TR>
					<TR>
						<TD style="WHITE-SPACE: normal; HEIGHT: 17mm" vAlign="top" colSpan="12">标记唛码及备注&nbsp;&nbsp;&nbsp;<BR />
							<DIV style="PADDING-LEFT: 15px; WIDTH: 700px; MAX-WIDTH: 700px; WHITE-SPACE: normal; WORD-BREAK: break-all">
								<asp:Literal id="note_s" Runat="server"></asp:Literal></DIV>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:DataGrid ID="gList" HorizontalAlign="Center" Runat="server" CssClass="prtTab" 
				BorderWidth="1" CellPadding="0" CellSpacing="0" BorderColor="#000000" AutoGenerateColumns="False">
				<HeaderStyle CssClass="gtop"></HeaderStyle>
				<ItemStyle CssClass="gbody"></ItemStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="项号">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "g_no")%>
							<br />
							<%# DataBinder.Eval(Container.DataItem, "contr_item")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="商品编号">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "code_t")%>
							<%# DataBinder.Eval(Container.DataItem, "code_s")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="商品名称、规格型号">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "g_name")%>
							<br />
							<%# DataBinder.Eval(Container.DataItem, "g_model")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="数量及单位">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "qty_conv")%>
							<%# getName("unit",Convert.ToString(DataBinder.Eval(Container.DataItem, "unit_1")))%>
							<br />
							<%# DataBinder.Eval(Container.DataItem, "qty_2")%>
							<%# getName("unit",Convert.ToString(DataBinder.Eval(Container.DataItem, "unit_2")))%>
							<br />
							<%# DataBinder.Eval(Container.DataItem, "qty_1")%>
							<%# getName("unit",Convert.ToString(DataBinder.Eval(Container.DataItem, "g_unit")))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="原产国（地区）">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "origin")%>
							<br />
							<%# DataBinder.Eval(Container.DataItem, "origin_country")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="decl_price" DataFormatString="{0:#.0000}" HeaderText="单价"></asp:BoundColumn>
					<asp:BoundColumn DataField="trade_total" DataFormatString="{0:F}" HeaderText="总价"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="币制">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "trade_curr")%>
							<br />
							<%# DataBinder.Eval(Container.DataItem, "currency")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="征免">
						<ItemTemplate>
							<%# getName("duty_mode",Convert.ToString(DataBinder.Eval(Container.DataItem, "duty_mode")))%>
							<br />
							<%# getName("use_to",Convert.ToString(DataBinder.Eval(Container.DataItem, "use_to")))%>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<asp:Literal Runat="server" ID="pFoot">
				<TABLE class="ggTab" border="0" cellSpacing="0" borderColor="#000000" cellPadding="1" align="center">
					<COLGROUP>
						<COL width="100">
						<COL width="130">
						<COL width="250">
						<COL width="120">
						<COL width="120">
					</COLGROUP>
					<TR>
						<TD style="WHITE-SPACE: normal; HEIGHT: 42mm" class="bd" vAlign="top" colSpan="5">税费征收情况</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 7mm">录入员</TD>
						<TD class="br">录入单位</TD>
						<TD>兹声明以上申报无讹并承担法律责任</TD>
						<TD class="bl" colSpan="2">海关审单批注及放行日期（签章）</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 5mm" class="br bd" colSpan="2">&nbsp;</TD>
						<TD>&nbsp;</TD>
						<TD class="bl">审单</TD>
						<TD>审价</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">报关员</TD>
						<TD class="bl bd" colSpan="2">&nbsp;</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">&nbsp;</TD>
						<TD class="bl">征税</TD>
						<TD>统计</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">单位地址</TD>
						<TD class="bl bd" colSpan="2">&nbsp;</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">&nbsp;</TD>
						<TD class="bl">查验</TD>
						<TD>查验</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 6mm">邮编</TD>
						<TD>电话</TD>
						<TD>填制日期</TD>
						<TD class="bl">&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
				</TABLE>
			</asp:Literal>
			<asp:Panel ID="phList" Runat="server"></asp:Panel>
		</form>
		<OBJECT id="WebBrowser1" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" width="0"	height="0" VIEWASTEXT></OBJECT>
		<SCRIPT language=javascript>
		    WebBrowser1.ExecWB(7, 1);
		</script>
	</body>
</HTML>

