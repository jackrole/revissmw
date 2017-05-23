<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CJJYS.aspx.cs" Inherits="Export.Clasup.CJJYS" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="Expires" CONTENT="0" />
		<meta http-equiv="Cache-Control" CONTENT="no-cache" />
		<meta http-equiv="Pragma" CONTENT="no-cache" />
		<title>出境货物备案清单</title>
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
		.bz{PADDING-LEFT: 15px; WIDTH: 700px; MAX-WIDTH: 700px; WHITE-SPACE: normal; WORD-BREAK: break-all;}
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="TRANS_PRE_ID" Visible="false" Runat="server"></asp:textbox>
            <asp:Literal Runat="server" ID="pTOP" Visible="false">
				<TABLE border="0" cellSpacing="0" cellPadding="0" align="center">
					<TR>
						<TD style="HEIGHT: 16mm" colSpan="4">&nbsp;</TD>
					</TR>
					<TR>
						<TD width="15"></TD>
						<TD style="HEIGHT: 6mm; FONT-SIZE: 22px" align="center"><B>中华人民共和国海关出境货物备案清单</B></TD>
						<TD style="WIDTH: 6mm"></TD>
						<TD vAlign="top" rowSpan="2">#Code_2</TD>
					</TR>
					<TR>
						<TD></TD>
						<TD style="LETTER-SPACING: 5px" colSpan="2" align="center">#Code_39</TD>
						<TD></TD>
					</TR>
				</TABLE>
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="720" align="center">
					<TR>
						<TD>预录入编号：#pre_entry_id</TD>
						<TD>Page 1</TD>
						<TD>海关编号：#customs_id</TD>
						<TD>Page 1</TD>
					</TR>
				</TABLE>
				<TABLE id="prtTab" border="1" cellSpacing="0" borderColor="black" cellPadding="0" align="center">
					<TR>
						<TD colSpan="2">出境口岸<BR />&nbsp;&nbsp;&nbsp;&nbsp;浦机综保&nbsp;&nbsp;&nbsp;&nbsp;2216</TD>
						<TD>备案号<BR />&nbsp;&nbsp;&nbsp;&nbsp;H22161000002</TD>
						<TD>出境日期<BR />&nbsp;&nbsp;&nbsp;&nbsp;#i_e_date</TD>
						<TD>申报日期<BR />&nbsp;&nbsp;&nbsp;&nbsp;#d_date</TD>
					</TR>
					<TR>
						<TD colSpan="2">区内经营单位&nbsp;&nbsp;&nbsp;&nbsp;3122610003<BR />&nbsp;&nbsp;&nbsp;&nbsp;上海中远空港保税物流有限公司</TD>
						<TD>运输方式<BR />其它运输&nbsp;&nbsp;9</TD>
						<TD>运输工具名称<BR />分送集报/</TD>
						<TD>提运单号<BR />&nbsp;&nbsp;&nbsp;&nbsp;#bill_no</TD>
					</TR>
					<TR>
						<TD colSpan="2">区内发货单位&nbsp;&nbsp;&nbsp;&nbsp;3122610003<BR />&nbsp;&nbsp;&nbsp;&nbsp;上海中远空港保税物流有限公司</TD>
						<TD>贸易方式<BR />成品进出区&nbsp;&nbsp;&nbsp;&nbsp;5100</TD>
						<TD>运抵国（地区）<BR />&nbsp;&nbsp;&nbsp;&nbsp;中国&nbsp;&nbsp;&nbsp;&nbsp;142</TD>
						<TD>境内货源地<BR /><div align="center">上海浦东机场综&nbsp;31226</div></TD>
					</TR>
					<TR>
						<TD>许可证号<BR />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#lisence_no</TD>
						<TD>成交方式<BR />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FOB</TD>
						<TD>运费<BR />&nbsp;&nbsp;&nbsp;&nbsp;</TD>
						<TD>保费<BR />&nbsp;&nbsp;&nbsp;&nbsp;</TD>
						<TD>杂费<BR />&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					</TR>
					<TR>
						<TD>件数&nbsp;&nbsp;#pack_no<BR />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;纸箱</TD>
						<TD>毛重（千克）<BR />&nbsp;&nbsp;&nbsp;&nbsp;#gross_wt</TD>
						<TD>净重（千克）<BR />&nbsp;&nbsp;&nbsp;&nbsp;#net_wt</TD>
						<TD>随附单证<BR />&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WHITE-SPACE: normal; HEIGHT: 17mm" valign="top" colSpan="5">标记唛码及备注<BR />
							<DIV class="bz">#note_s</DIV>
						</TD>
					</TR>
				</TABLE>
			</asp:Literal>			

			<asp:Literal Runat="server" ID="pFoot" Visible="false">
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
					<TR valign="top">
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
					<TR valign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">报关员</TD>
						<TD class="bl bd" colSpan="2">&nbsp;</TD>
					</TR>
					<TR valign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">&nbsp;</TD>
						<TD class="bl">征税</TD>
						<TD>统计</TD>
					</TR>
					<TR valign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">单位地址</TD>
						<TD class="bl bd" colSpan="2">&nbsp;</TD>
					</TR>
					<TR valign="top">
						<TD style="HEIGHT: 6mm" colSpan="3">&nbsp;</TD>
						<TD class="bl">查验</TD>
						<TD>查验</TD>
					</TR>
					<TR valign="top">
						<TD style="HEIGHT: 6mm">邮编</TD>
						<TD>电话</TD>
						<TD>填制日期</TD>
						<TD class="bl">&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
				</TABLE>
			</asp:Literal>
			<asp:Literal ID="phList" Runat="server"></asp:Literal>
		</form>
		<SCRIPT language=javascript>
		    window.print();
		</script>
	</body>
</HTML>
