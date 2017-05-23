using System;
using System.Data;
using System.Web.UI;
using System.IO;
using System.Text;

namespace Export.Clasup
{
    public partial class PassDN : System.Web.UI.Page
    {
        #region xml example
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

        #region xml template
        string telegraphRoot = @"
<?xml version=""1.0"" encoding=""gb2312"" ?>
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
        <IE_FLAG>I</IE_FLAG>
        <PRDTID></PRDTID>
        <STORENO></STORENO>
        <RAMANUALNO></RAMANUALNO>
        <RADECLNO></RADECLNO>
        <PRE_ENTRY_ID></PRE_ENTRY_ID>
        <WBOOK_TYPE>WBOOK_AUTO</WBOOK_TYPE>
        <ACTION_DIR>+</ACTION_DIR>
        <CLASSIFY_TYPE>I</CLASSIFY_TYPE>
        <BUSINESS_TYPE>0</BUSINESS_TYPE>
        <IE_TYPE>0</IE_TYPE>
    </WBK_PDE_HEAD>
    <WBK_PDE_LIST_ORG COUNT = ""{7}"">
      {8}
	  </WBK_PDE_LIST_ORG>
</WBK_PDE_ROOT>";
        string telegraphItem = @"
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
                return Server.MapPath("../xml/") + passID + "/in";
            }
        }
        private string targetZipFilename
        {
            get
            {
                return workFolder + ".zip";
            }
        }

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
                Response.Write("<a href='../public/datacontainer.aspx?Obj=items_chk&no=" + passID + "&kind=1'>料号信息不完善，请先完善好料号信息再进行归并新增申请操作！</a>");
                return;
            }

            if (!Directory.Exists(workFolder) || Directory.GetFiles(workFolder).Length == 0)
            {
                if (!Directory.Exists(workFolder)) Directory.CreateDirectory(workFolder);
                CreateXML();
            }

            if (!File.Exists(targetZipFilename)) ClsZip.PackFiles(targetZipFilename, workFolder);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='../xml/{0}/in.zip'>全部下载</a><br />", passID);
            foreach (string t in Directory.GetFiles(workFolder))
            {
                FileInfo fi = new FileInfo(t);
                sb.AppendFormat("<a href='../xml/{0}/in/{1}'>{1}</a><br />", passID, fi.Name);
            }
            if (sb.Length > 0)
            {
                xmlList.Text = sb.ToString();
                sb.Remove(0, sb.Length);
            }
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
    }
}