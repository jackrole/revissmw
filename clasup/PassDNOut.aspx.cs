using System;
using System.Data;
using System.Web.UI;
using System.IO;
using System.Text;
using Aspose.Cells;

namespace Export.Clasup
{
  public partial class PassDNOut : System.Web.UI.Page
  {
    #region telegraph example
    /*
            <?xml version="1.0" encoding="gb2312" ?>
<WBK_PDE_ROOT>
	<WBK_PDE_HEAD>
		<WBOOK_NO>W10003201506180447</WBOOK_NO>
		<TRADE_CO>3122610003</TRADE_CO>
		<TRADE_NAME>上海中远空港保税物流有限公司</TRADE_NAME>
		<DISTRICT_CODE></DISTRICT_CODE>
		<OWNER_CODE>3122610003</OWNER_CODE>
		<OWNER_NAME>上海中远空港保税物流有限公司</OWNER_NAME>
		<AGENT_CODE>3122280170</AGENT_CODE>
		<AGENT_NAME>上海中远国际航空货运代理有限公司</AGENT_NAME>
		<TRAF_MODE>Y</TRAF_MODE>
		<I_E_PORT>2216</I_E_PORT>
		<DISTINATE_PORT></DISTINATE_PORT>
		<TRAF_NAME></TRAF_NAME>
		<CONTR_NO></CONTR_NO>
		<IN_RATIO></IN_RATIO>
		<BILL_NO></BILL_NO>
		<TRADE_COUNTRY></TRADE_COUNTRY>
		<TRADE_MODE>5000</TRADE_MODE>
		<CUT_MODE></CUT_MODE>
		<PAY_MODE></PAY_MODE>
		<TRANS_MODE>1</TRANS_MODE>
		<PAY_WAY></PAY_WAY>
		<FEE_MARK></FEE_MARK>
		<FEE_CURR></FEE_CURR>
		<FEE_RATE>0</FEE_RATE>
		<OTHER_MARK></OTHER_MARK>
		<OTHER_CURR></OTHER_CURR>
		<OTHER_RATE>0</OTHER_RATE>
		<INSUR_MARK></INSUR_MARK>
		<INSUR_CURR></INSUR_CURR>
		<INSUR_RATE>0</INSUR_RATE>
		<PACK_NO>0</PACK_NO>
		<GROSS_WT>6703.580</GROSS_WT>
		<NET_WT>5741.040</NET_WT>
		<LICENSE_NO></LICENSE_NO>
		<APPR_NO></APPR_NO>
		<MANUAL_NO>H22161000002</MANUAL_NO>
		<I_E_DATE>20150618</I_E_DATE>
		<WRAP_TYPE>7</WRAP_TYPE>
		<NOTE_S></NOTE_S>
		<D_DATE>20150618</D_DATE>
		<EX_SOURCE></EX_SOURCE>
		<VOYAGE_NO></VOYAGE_NO>
		<IE_FLAG>E</IE_FLAG>
		<PRDTID></PRDTID>
		<STORENO></STORENO>
		<RAMANUALNO></RAMANUALNO>
		<RADECLNO></RADECLNO>
		<PRE_ENTRY_ID></PRE_ENTRY_ID>
		<WBOOK_TYPE>WBOOK_AUTO</WBOOK_TYPE>
		<ACTION_DIR>-</ACTION_DIR>
		<CLASSIFY_TYPE>E</CLASSIFY_TYPE>
	</WBK_PDE_HEAD>
	<WBK_PDE_LIST_ORG COUNT = "1">
		<WBK_PDE_ITEM_ORG>
			<WBOOK_NO>W10003201506180447</WBOOK_NO>
			<G_NO>1</G_NO>
			<COP_G_NO>6L6-00006</COP_G_NO>
			<CONTR_ITEM>308</CONTR_ITEM>
			<CODE_T>95045019</CODE_T>
			<CODE_S></CODE_S>
			<CLASS_MARK></CLASS_MARK>
			<G_NAME>Xbox One Kinect感应器</G_NAME>
			<G_MODEL>6L6-00006</G_MODEL>
			<ORIGIN_COUNTRY>中国</ORIGIN_COUNTRY>
			<G_QTY>150.0000</G_QTY>
			<G_UNIT>台</G_UNIT>
			<QTY_1>150.0000</QTY_1>
			<UNIT_1>台</UNIT_1>
			<QTY_2>150.0000</QTY_2>
			<UNIT_2>千克</UNIT_2>
			<TRADE_CURR></TRADE_CURR>
			<DECL_PRICE>0</DECL_PRICE>
			<TRADE_TOTAL>0</TRADE_TOTAL>
			<DUTY_MODE>3</DUTY_MODE>
			<USE_TO>11</USE_TO>
			<PRDT_NO>0</PRDT_NO>
			<GROSS_WT>6703.5800</GROSS_WT>
			<NET_WT>5741.0400</NET_WT>
		</WBK_PDE_ITEM_ORG>
	</WBK_PDE_LIST_ORG>
</WBK_PDE_ROOT>
            */
    #endregion

    #region telegraph template
    private static readonly string telegraphRoot = @"
<WBK_PDE_ROOT>
    <WBK_PDE_HEAD>
        <WBOOK_NO>{0}</WBOOK_NO>
        <TRADE_CO>3122610003</TRADE_CO>
        <TRADE_NAME>上海中远空港保税物流有限公司</TRADE_NAME>
        <DISTRICT_CODE></DISTRICT_CODE>
        <OWNER_CODE>3122610003</OWNER_CODE>
        <OWNER_NAME>上海中远空港保税物流有限公司</OWNER_NAME>
        <AGENT_CODE>3122280170</AGENT_CODE>
        <AGENT_NAME>上海中远国际航空货运代理有限公司</AGENT_NAME>
        <TRAF_MODE>Y</TRAF_MODE>
        <I_E_PORT>2216</I_E_PORT>
        <DISTINATE_PORT></DISTINATE_PORT>
        <TRAF_NAME></TRAF_NAME>
        <CONTR_NO></CONTR_NO>
        <IN_RATIO></IN_RATIO>
        <BILL_NO></BILL_NO>
        <TRADE_COUNTRY></TRADE_COUNTRY>
        <TRADE_MODE>5000</TRADE_MODE>
        <CUT_MODE></CUT_MODE>
        <PAY_MODE></PAY_MODE>
        <TRANS_MODE>1</TRANS_MODE>
        <PAY_WAY></PAY_WAY>
        <FEE_MARK></FEE_MARK>
        <FEE_CURR></FEE_CURR>
        <FEE_RATE>0</FEE_RATE>
        <OTHER_MARK></OTHER_MARK>
        <OTHER_CURR></OTHER_CURR>
        <OTHER_RATE>0</OTHER_RATE>
        <INSUR_MARK></INSUR_MARK>
        <INSUR_CURR></INSUR_CURR>
        <INSUR_RATE>0</INSUR_RATE>
        <PACK_NO>{1}</PACK_NO>
        <GROSS_WT>{2}</GROSS_WT>
        <NET_WT>{3}</NET_WT>
        <LICENSE_NO></LICENSE_NO>
        <APPR_NO></APPR_NO>
        <MANUAL_NO>H22161000002</MANUAL_NO>
        <I_E_DATE>{4}</I_E_DATE>
        <WRAP_TYPE></WRAP_TYPE>
        <NOTE_S>{6}</NOTE_S>
        <D_DATE>{5}</D_DATE>
        <EX_SOURCE></EX_SOURCE>
        <VOYAGE_NO></VOYAGE_NO>
        <IE_FLAG>E</IE_FLAG>
        <PRDTID></PRDTID>
        <STORENO></STORENO>
        <RAMANUALNO></RAMANUALNO>
        <RADECLNO></RADECLNO>
        <PRE_ENTRY_ID></PRE_ENTRY_ID>
        <WBOOK_TYPE>WBOOK_AUTO</WBOOK_TYPE>
        <ACTION_DIR>-</ACTION_DIR>
        <CLASSIFY_TYPE>E</CLASSIFY_TYPE>
        <BUSINESS_TYPE>0</BUSINESS_TYPE>
        <IE_TYPE>0</IE_TYPE>
    </WBK_PDE_HEAD>
	  <WBK_PDE_LIST_ORG COUNT = ""{7}"">
		  {8}
	  </WBK_PDE_LIST_ORG>
</WBK_PDE_ROOT>";
    public static readonly string telegraphItem = @"
<WBK_PDE_ITEM_ORG>
    <WBOOK_NO>{0}</WBOOK_NO>
    <G_NO>{1}</G_NO>
    <COP_G_NO>{2}</COP_G_NO>
    <CONTR_ITEM>{3}</CONTR_ITEM>
    <CODE_T>{4}</CODE_T>
    <CODE_S>{5}</CODE_S>
    <CLASS_MARK>{6}</CLASS_MARK>
    <G_NAME>{7}</G_NAME>
    <G_MODEL>{8}</G_MODEL>
    <ORIGIN_COUNTRY>{9}</ORIGIN_COUNTRY>
    <G_QTY>{10}</G_QTY>
    <G_UNIT>{11}</G_UNIT>
    <QTY_1>{12}</QTY_1>
    <UNIT_1>{13}</UNIT_1>
    <QTY_2>{14}</QTY_2>
    <UNIT_2>{15}</UNIT_2>
    <TRADE_CURR>{16}</TRADE_CURR>
    <DECL_PRICE>{17}</DECL_PRICE>
    <TRADE_TOTAL>{18}</TRADE_TOTAL>
    <DUTY_MODE>3</DUTY_MODE>
    <USE_TO>11</USE_TO>
    <PRDT_NO>0</PRDT_NO>
    <GROSS_WT>{19}</GROSS_WT>
    <NET_WT>{20}</NET_WT>
</WBK_PDE_ITEM_ORG>";
    #endregion

    #region procedure names
    private static readonly string wms2_pass_EnsureSku = "wms2_pass_EnsureSku";
    private static readonly string wms2_pass_GetNW = "wms2_pass_GetNW";
    private static readonly string wms2_pass_DN = "wms2_pass_DN";
    #endregion

    private string passID = "";
    private string workFolder
    {
      get
      {
        return Server.MapPath("../xml/") + passID + "/out";
      }
    }
    private string targetZipFilename
    {
      get
      {
        return workFolder + ".zip";
      }
    }

    protected string connOLE = g.getConfig("dbQYSJ");
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
        LoadDN();
    }

    private void LoadDN()
    {
      passID = g.GetRequest("no");
      if (string.IsNullOrEmpty(passID))
      {
        Response.Write("参数不正确，没有要申请的移库单号！");
        return;
      }
      string strsql = g.getSValue(string.Format("exec {1} '{0}'", g.ToSql(passID), wms2_pass_EnsureSku));
      if (!string.IsNullOrEmpty(strsql))
      {
        Response.Write("<a href='../public/datacontainer.aspx?Obj=items_chk1&no=" + passID + "&kind=1'>料号信息不完善，请先完善好料号信息再进行归并新增申请操作！</a>");
        return;
      }

      if (!Directory.Exists(workFolder) || Directory.GetFiles(workFolder).Length == 0)
      {
        if (!Directory.Exists(workFolder)) Directory.CreateDirectory(workFolder);
        CreateXML();
      }
      if (!File.Exists(targetZipFilename)) ClsZip.PackFiles(targetZipFilename, workFolder);
      g.Download(targetZipFilename, passID + "_out.zip");
    }

    private void CreateXML()
    {
      StringBuilder telegraph, sb;
      DataTable dt = g.getTable(string.Format(
        "SELECT PASSTIME,BOXNUM,PSC,CW,NW,PAY,CURRKIND,CID,ISUNDERBOUNDED FROM CLASUP_PASS WHERE ID='{0}'",
        g.ToSql(passID)
      ));
      if (dt.Rows.Count == 0)
      {
        dt.Dispose();
        return;
      }
      int k = 0, iCount = 0, boxNum = g.getInt(dt.Rows[0]["BOXNUM"].ToString());
      decimal netWeight = g.getDecimal(dt.Rows[0]["NW"].ToString());
      decimal grossWeight = g.getDecimal(dt.Rows[0]["CW"].ToString());
      string remarks = "";
      dt.Dispose();

      if (netWeight == 0)
        netWeight = g.getDecimal(g.getSValue(string.Format("exec {1} '{0}'", g.ToSql(passID), wms2_pass_GetNW)));
      if (grossWeight == 0)
        grossWeight = netWeight;

      dt = g.getTable(string.Format("exec {1} '{0}'", g.ToSql(passID), wms2_pass_DN));  //进境报文存储过程
      int iRec = dt.Rows.Count;
      if (iRec == 0)
        return;
      string wbook, cn_origin = "", zcxh = "";
      DateTime dtNow = DateTime.Now;
      string qty_1 = "", qty_2 = "";

      sb = new StringBuilder();
      wbook = g.WBOOK_NO("WBOOK_NO");
      k = iCount = 0;
      netWeight = grossWeight = 0;

      #region save telegraph
      Action saveTelegraph = () =>
      {
        telegraph = new StringBuilder();
        telegraph.AppendFormat(telegraphRoot,
          wbook,
          boxNum,
          grossWeight.ToString("#.###"),
          netWeight.ToString("#.###"),
          dtNow.ToString("yyyyMMdd"),
          dtNow.ToString("yyyyMMdd"),
          remarks,
          iCount,
          sb.ToString()
        );
        g.CreateFile(
          string.Format(
            "{0}/{1}",
            workFolder,
            string.Format("WBK_COP_PDE_3122610003_{0}_{1}.xml", wbook, dtNow.ToString("yyyyMMddHHmmss"))
          ),
          telegraph
        );
      };
      #endregion

      for (int i = 0; i < iRec; i++)
      {
        grossWeight += g.getDecimal(dt.Rows[i]["cw"].ToString());
        netWeight += g.getDecimal(dt.Rows[i]["nw"].ToString());
        iCount++;
        zcxh = dt.Rows[i]["zcxh"].ToString();

        if (k > 0 && k % 50 == 0)
        {
          saveTelegraph();

          sb = new StringBuilder();
          cn_origin = "";
          k = iCount = 0;
          netWeight = grossWeight = 0;
          if (i < iRec)
            wbook = g.WBOOK_NO("WBOOK_NO");
        }

        if (string.IsNullOrEmpty(cn_origin)) cn_origin = dt.Rows[i]["cn_origin"].ToString();

        qty_1 = dt.Rows[i]["pcs"].ToString();
        qty_2 = dt.Rows[i]["nw"].ToString();
        if (dt.Rows[i]["unit_1"].ToString() == "千克")
        {
          qty_1 = dt.Rows[i]["nw"].ToString();
          qty_2 = dt.Rows[i]["pcs"].ToString();
        }
        if (string.IsNullOrEmpty(dt.Rows[i]["unit_2"].ToString())) qty_2 = "";

        #region combine telegraph item
        sb.AppendFormat(
              telegraphItem,
              wbook,
              iCount,
              dt.Rows[i]["sku0"],
              dt.Rows[i]["zcxh"],
              dt.Rows[i]["hscode"],
              dt.Rows[i]["code_s"],
              "",
              dt.Rows[i]["cn_family"],
              dt.Rows[i]["cn_family"],
              cn_origin,
              dt.Rows[i]["pcs"],
              dt.Rows[i]["g_unit"],
              qty_1,
              dt.Rows[i]["unit_1"],
              qty_2,
              dt.Rows[i]["unit_2"],
              dt.Rows[i]["curr"],
              dt.Rows[i]["price"],
              dt.Rows[i]["amount"],
              dt.Rows[i]["gw"],
              dt.Rows[i]["nw"]
            );
        #endregion

        if (i < iRec - 1 && zcxh != dt.Rows[i + 1]["zcxh"].ToString())
        {
          cn_origin = "";
          k++;
        }
      }

      if (sb.Length > 0) saveTelegraph();
    }

    // Former out telegraph xml, delete me if on longer in use.
    private void CreateXML_del()
    {
      StringBuilder sb;

      int k = 0, iRow = 0, pcs = 0;
      decimal nw = 0, gw = 0;
      string strsql = string.Format("select weight, cid, tuopan, memo  from outbound where ckno='{0}'", g.ToSql(passID));
      DataTable dt = g.getTable(strsql);
      if (dt.Rows.Count == 0)
      {
        dt.Dispose();
        return;
      }
      string bz = dt.Rows[0][3].ToString();
      string cc = dt.Rows[0][1].ToString();
      gw = g.getDecimal(dt.Rows[0][0].ToString());
      pcs = g.getInt(dt.Rows[0][2].ToString());
      dt.Dispose();

      cc = cc.ToLower() == "tempe" ? "DB" : "ATS";

      string bgd = g.getSValue(string.Format("select 1 from form_head where rkno='{0}'", g.ToSql(passID)));
      if (string.IsNullOrEmpty(bgd))
        g.ExecSql(string.Format("exec WMS2_p_BGD1 '{0}'", g.ToSql(passID)));

      nw = g.getDecimal(g.getSValue(string.Format("exec wms2_p_NW1 '{0}'", g.ToSql(passID))));
      if (gw == 0)
        gw = nw;
      decimal ww = gw / nw;

      DateTime dtNow = DateTime.Now;
      string DNO = "", DELIVERY_NO = "", bgdno = "", wbook, xmlPath, dbPath;

      dbPath = Server.MapPath("../xls/") + "QYSJ.mdb";
      string dbPath1 = Server.MapPath("../xls/") + "QYSJ1.mdb";

      string connOLE1 = string.Format(connOLE, dbPath1);
      connOLE = string.Format(connOLE, dbPath);
      g.ExecSql("delete from T_EPZ_DELIVERY_OUT", connOLE);
      g.ExecSql("delete from T_EPZ_DELIVERY_OUT_DETAIL", connOLE);

      g.ExecSql("delete from t_bgcz_dbd", connOLE1);
      g.ExecSql("delete from t_bgcz_dbdmx", connOLE1);

      int imon = DateTime.Now.Month;
      string sMon = DateTime.Now.ToString("MM").Substring(1);
      if (imon == 10)
        sMon = "A";
      else if (imon == 11)
        sMon = "B";
      else if (imon == 12)
        sMon = "C";
      DNO = string.Format("BC10003{0}{1}", DateTime.Now.ToString("yy").Substring(1), sMon);

      string dbHead = @"
Insert into T_EPZ_DELIVERY_OUT(DELIVERY_NO,DELIVERY_TIME,ORGANIZATION_CODE,MANUFACTURER_NO,EMS_TYPE,WEIGHT,[MEMO],[STATUS],OLD_DELIVERY_NO,OUT_TYPE,MANUAL_NO_OUT) 
values ('{0}',cdate('{1}'),'{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}')";

      string dbList = @"
Insert into T_EPZ_DELIVERY_OUT_DETAIL
(DELIVERY_NO,DELIVERY_DETAIL_NO,MATERIAL_ID,G_NAME,G_UNIT,
COP_G_NO,CODE_T,CODE_S,QTY_1,NET_WEIGHT,
WEIGHT,ORIGIN_COUNTRY,G_NO,PRD_VERSION,TRADE_TOTAL,
TRADE_CURR,UNIT_PRICE,QTY_4) 
values ('{0}','{1}','{2}','{3}','{4}',
'{5}','{6}','{7}',{8},{9},
{10},'{11}','{12}','{13}',{14},
'{15}',{16},{17})";

      string dbdHead = @"
Insert into t_bgcz_dbd(DBDH, DBRQ, BCQYBM, BRQYBM, ZCLB, DBZL, QYQRRQ, QYQRCZR, ZT, BRZCLB, BZ, DeliveryType) 
values ('T{0}', cdate('{1}'), '3122610003', '312264K004', '1', {2}, null, '', '', '0', '', '1')";


      string dbdList = @"
Insert into t_bgcz_dbdmx(DBDH, MXDH, WZXH, WZPM, WZHH, SBSL, JLDW, JZ, YCD, DBSL,  
YCQK, DZXH, BRDZXH, CODE_T, CODE_S, PRD_VERSION, IN_PRD_VERSION, SBJE, IN_G_NAME, IN_CODE_T, IN_CODE_S, IN_COP_G_NO) 
values ('T{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', {7}, '{8}', {9},
'{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', {17}, '', '', '', '')";
      strsql = string.Format("select pre_entry_id,no_bc from form_head where rkno= '{0}' order by convert(int,replace(pre_entry_id, rkno+'_',''))", g.ToSql(passID));
      DataTable dtt = g.getTable(strsql);
      for (int i = 0; i < dtt.Rows.Count; i++)
      {
        bgdno = dtt.Rows[i]["pre_entry_id"].ToString();

        Workbook wb = new Workbook();
        Worksheet ws = wb.Worksheets[0];
        ws.Cells[0, 0].Value = "DELIVERY_NO";
        ws.Cells[0, 1].Value = "DELIVERY_DETAIL_NO";
        ws.Cells[0, 2].Value = "MATERIAL_ID";
        ws.Cells[0, 3].Value = "G_NAME";
        ws.Cells[0, 4].Value = "G_UNIT";
        ws.Cells[0, 5].Value = "COP_G_NO";
        ws.Cells[0, 6].Value = "CODE_T";
        ws.Cells[0, 7].Value = "CODE_S";
        ws.Cells[0, 8].Value = "QTY_1";
        ws.Cells[0, 9].Value = "NET_WEIGHT";
        ws.Cells[0, 10].Value = "WEIGHT";
        ws.Cells[0, 11].Value = "ORIGIN_COUNTRY";
        ws.Cells[0, 12].Value = "G_NO";
        ws.Cells[0, 13].Value = "PRD_VERSION";
        ws.Cells[0, 14].Value = "TRADE_TOTAL";
        ws.Cells[0, 15].Value = "TRADE_CURR";
        ws.Cells[0, 16].Value = "UNIT_PRICE";
        ws.Cells[0, 17].Value = "QTY_4";

        wbook = g.WBOOK_NO("WBOOK_NO");

        strsql = "select sum(convert(numeric(18,3),qty_1)) pcs, sum(convert(numeric(18,3),qty_2)) nw from Form_LIST where pre_entry_id='{0}'";
        dt = g.getTable(string.Format(strsql, g.ToSql(bgdno)));
        if (pcs == 0)
          pcs = g.getInt(dt.Rows[0][0].ToString());
        nw = g.getDecimal(dt.Rows[0][1].ToString());
        gw = nw * ww;

        dt = g.getTable(string.Format("exec wms2_p_DNL '{0}','{1}'", g.ToSql(passID), g.ToSql(bgdno)));
        iRow = dt.Rows.Count;

        sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"gb2312\" ?>");
        sb.AppendFormat(telegraphRoot, wbook, pcs, gw.ToString("#.###"), nw, dtNow.ToString("yyyyMMdd"), dtNow.ToString("yyyyMMdd"), bz);
        sb.AppendFormat("\r\n   <WBK_PDE_LIST_ORG COUNT = \"{0}\">", iRow);

        DELIVERY_NO = dtt.Rows[i][1].ToString();
        string DBD_NO = DELIVERY_NO;
        if (string.IsNullOrEmpty(DELIVERY_NO))
        {
          if (i == 0)
          {
            DELIVERY_NO = g.getMaxValue(DNO, 5);
            DELIVERY_NO = g.getMaxValue(DNO, 5);
            DELIVERY_NO = g.getMaxValue(DNO, 5);
          }
          DELIVERY_NO = DNO + g.getMaxValue(DNO, 5);
          g.ExecSql(string.Format("update form_head set no_bc='{0}' where pre_entry_id='{1}'", DELIVERY_NO, g.ToSql(bgdno)));
        }

        DBD_NO = DELIVERY_NO.Substring(1);
        g.ExecSql(string.Format(dbHead, DELIVERY_NO, DateTime.Now.ToString("yyyy-MM-dd"), "3122610003", cc, "1", 0, "", "", "", "1", ""), connOLE);
        g.ExecSql(string.Format(dbdHead, DBD_NO, DateTime.Now.ToString("yyyy-MM-dd"), nw), connOLE1);

        int ic = 1, n = 1;
        string qty_1 = "", qty_2 = "";
        k = 1;
        for (int j = 0; j < dt.Rows.Count; j++)
        {
          qty_1 = dt.Rows[j]["pcs"].ToString();
          qty_2 = dt.Rows[j]["nw"].ToString();

          if (dt.Rows[j]["unit_1"].ToString() == "千克")
          {
            qty_1 = dt.Rows[j]["nw"].ToString();
            qty_2 = dt.Rows[j]["pcs"].ToString();
          }

          sb.AppendFormat(telegraphItem, wbook, k, dt.Rows[j]["sku0"], dt.Rows[j]["zcxh"], dt.Rows[j]["hscode"], dt.Rows[j]["code_s"],
              "", dt.Rows[j]["cn_family"], dt.Rows[j]["cn_family"], dt.Rows[j]["cn_origin"], dt.Rows[j]["pcs"], dt.Rows[j]["g_unit"],
              qty_1, dt.Rows[j]["unit_1"], qty_2, dt.Rows[j]["unit_2"],
              dt.Rows[j]["currency"], dt.Rows[j]["price"], dt.Rows[j]["amount"], dt.Rows[j]["cw"], dt.Rows[j]["nw"]);

          ws.Cells[ic, 0].Value = DELIVERY_NO;
          ws.Cells[ic, 1].Value = string.Format("{0:00}", k);
          ws.Cells[ic, 2].Value = dt.Rows[j]["hgxh"].ToString();
          ws.Cells[ic, 3].Value = dt.Rows[j]["cn_family"].ToString();
          ws.Cells[ic, 4].Value = dt.Rows[j]["g_unit"].ToString();
          ws.Cells[ic, 5].Value = dt.Rows[j]["sku0"].ToString();
          ws.Cells[ic, 6].Value = dt.Rows[j]["hscode"].ToString();
          ws.Cells[ic, 7].Value = dt.Rows[j]["code_s"].ToString();
          ws.Cells[ic, 8].Value = dt.Rows[j]["pcs"].ToString();
          ws.Cells[ic, 9].Value = dt.Rows[j]["nw"].ToString();
          ws.Cells[ic, 10].Value = dt.Rows[j]["cw"].ToString();
          ws.Cells[ic, 11].Value = dt.Rows[j]["cn_origin"].ToString();
          ws.Cells[ic, 12].Value = dt.Rows[j]["zcxh"].ToString();
          ws.Cells[ic, 13].Value = "0";
          ws.Cells[ic, 14].Value = "";
          ws.Cells[ic, 15].Value = dt.Rows[j]["currency"].ToString();
          ws.Cells[ic, 16].Value = dt.Rows[j]["price"].ToString();
          ws.Cells[ic, 17].Value = "1";

          g.ExecSql(string.Format(dbList,
              DELIVERY_NO, string.Format("{0:00}", k), dt.Rows[j]["hgxh"], dt.Rows[j]["cn_family"], dt.Rows[j]["g_unit"],
              dt.Rows[j]["sku0"], dt.Rows[j]["hscode"], dt.Rows[j]["code_s"], dt.Rows[j]["pcs"], dt.Rows[j]["nw"],
              dt.Rows[j]["cw"], dt.Rows[j]["cn_origin"], dt.Rows[j]["zcxh"], "0", dt.Rows[j]["amount"],
              dt.Rows[j]["currency"], dt.Rows[j]["price"].ToString(), "1"), connOLE);

          if (n > 99)
          {
            n = 1;
            DBD_NO = DNO.Substring(1) + g.getMaxValue(DNO, 5);
            g.ExecSql(string.Format(dbdHead, DBD_NO, DateTime.Now.ToString("yyyy-MM-dd"), nw), connOLE1);
          }
          g.ExecSql(string.Format(dbdList,
              DBD_NO, string.Format("{0:00}", n), dt.Rows[j]["hgxh"], dt.Rows[j]["cn_family"], dt.Rows[j]["sku0"], dt.Rows[j]["pcs"],
              dt.Rows[j]["g_unit"], dt.Rows[j]["nw"], "", dt.Rows[j]["pcs"], "", dt.Rows[j]["zcxh"], dt.Rows[j]["zcxh"],
              dt.Rows[j]["hscode"], dt.Rows[j]["code_s"], "0", "", dt.Rows[j]["amount"]), connOLE1);

          ic++;
          k++;
          n++;
        }
        sb.Append("\r\n </WBK_PDE_LIST_ORG>\r\n</WBK_PDE_ROOT>");
        xmlPath = string.Format("WBK_COP_PDE_3122610003_{0}_{1}.xml", wbook, dtNow.ToString("yyyyMMddHHmmss"));
        g.CreateFile(string.Format("{0}/{1}", workFolder, xmlPath), sb);

        wb.Save(string.Format("{0}/调拨单{1}.xls", workFolder, i), SaveFormat.Excel97To2003);
      }

      File.Copy(dbPath, string.Format("{0}/QYSJ.mdb", workFolder));
      File.Copy(dbPath1, string.Format("{0}/区内调拨.mdb", workFolder));
    }
  }
}