using System;
using System.Web.UI;
using System.Data;
using System.Text;

namespace Export.Clasup
{
    public partial class CJJYS : System.Web.UI.Page
    {
        protected string connOLE = g.getConfig("ConnectStringOle");
        protected System.Web.UI.WebControls.Repeater rList;
        protected string connGS = "";

        private static readonly string wms2_pass_BGDOut = "wms2_pass_BGDOut";
        private static readonly string pWMS_pass_BGDOut = "pWMS_pass_BGDOut";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            connGS = string.Format(connOLE, g.getConfig("ConnBG_SB") + "companydb.mdb");
            connOLE = string.Format(connOLE, g.getConfig("ConnBG_SB") + "parameterdb.mdb");

            string passID = g.GetRequest("no");
            if (passID == "")
            {
                passID = g.GetRequest("ckno");
                if (passID == "")
                {
                    g.Alert("参数不能为空！", true);
                    return;
                }
            }
            if (passID.StartsWith("1"))
                g.ExecSql(string.Format("exec {1} '{0}'", g.ToSql(passID), wms2_pass_BGDOut));

            string strsql = SQLTemplate.New(
              "SELECT IE_FLAG, PRE_ENTRY_ID, CUSTOMS_ID, MANUAL_NO, CONTR_NO, I_E_DATE, D_DATE, ",
              "  TRADE_CO, TRADE_NAME, OWNER_CODE, OWNER_NAME, AGENT_CODE, AGENT_NAME, ",
              "  TRAF_MODE, TRAF_NAME, VOYAGE_NO, BILL_NO, TRADE_MODE, CUT_MODE, IN_RATIO, PAY_WAY, ",
              "  LISENCE_NO, TRADE_COUNTRY, DISTINATE_PORT, DISTRICT_CODE, APPR_NO, TRANS_MODE, ",
              "  FEE_MARK, FEE_RATE, FEE_CURR, INSUR_MARK, INSUR_RATE, INSUR_CURR, OTHER_MARK, ",
              "  OTHER_RATE, OTHER_CURR, PACK_NO, WRAP_TYPE, GROSS_WT, NET_WT, EX_SOURCE, RADECLNO, RAMANUALNO, ",
              "  STORENO, I_E_PORT, NOTE_S,SYN ",
              "FROM FORM_HEAD ",
              "WHERE RKNO= {0S} ORDER BY CONVERT(INT,REPLACE(PRE_ENTRY_ID, RKNO+'_',''))"
            ).FormatSql(passID);

            string mbHead = pTOP.Text;
            string mbFoot = pFoot.Text;
            string rgTop = "";
            DataView dataView = g.getTable(strsql).DefaultView;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb0 = new StringBuilder();
            int iPage = 1;
            #region read data
            for (int n = 0; n < dataView.Count; n++)
            {
                rgTop = mbHead;
                string bgdID = dataView[n]["pre_entry_id"].ToString();

                #region head
                rgTop = rgTop.Replace("#pre_entry_id", bgdID);
                rgTop = rgTop.Replace("#Code_2", string.Format("<img src='../QRcode.aspx?no={0}' style='height:14mm' />", bgdID));
                rgTop = rgTop.Replace("#Code_39", string.Format("<img src='../Codes.aspx?no={0}' /><br /><font size=4>{0}</font>", bgdID));
                rgTop = rgTop.Replace("#customs_id", dataView[n]["customs_id"].ToString());
                rgTop = rgTop.Replace("#manual_no", dataView[n]["manual_no"].ToString());

                rgTop = rgTop.Replace("#contr_no", dataView[n]["contr_no"].ToString());
                strsql = dataView[n]["contr_no"].ToString();
                if (strsql.Length > 27)
                    strsql = strsql.Substring(0, 27);

                rgTop = rgTop.Replace("#i_e_date", dataView[n]["i_e_date"].ToString());
                rgTop = rgTop.Replace("#d_date", dataView[n]["d_date"].ToString());
                rgTop = rgTop.Replace("#trade_co", dataView[n]["trade_co"].ToString());
                rgTop = rgTop.Replace("#trade_name", dataView[n]["trade_name"].ToString());
                rgTop = rgTop.Replace("#owner_code", dataView[n]["owner_code"].ToString());
                rgTop = rgTop.Replace("#owner_name", dataView[n]["owner_name"].ToString());

                rgTop = rgTop.Replace("#traf_modecc", dataView[n]["traf_mode"].ToString());
                rgTop = rgTop.Replace("#traf_mode", getName("traf_mode", dataView[n]["traf_mode"].ToString()));

                rgTop = rgTop.Replace("#trade_modecc", dataView[n]["trade_mode"].ToString());
                rgTop = rgTop.Replace("#trade_mode", getName("trade_mode", dataView[n]["trade_mode"].ToString()));

                rgTop = rgTop.Replace("#cut_modecc", dataView[n]["cut_mode"].ToString());
                rgTop = rgTop.Replace("#cut_mode", getName("cut_mode", dataView[n]["cut_mode"].ToString()));
                //
                rgTop = rgTop.Replace("#agent_code", dataView[n]["agent_code"].ToString());
                rgTop = rgTop.Replace("#agent_name", dataView[n]["agent_name"].ToString());
                rgTop = rgTop.Replace("#traf_name", dataView[n]["traf_name"].ToString());
                rgTop = rgTop.Replace("#voyage_no", dataView[n]["voyage_no"].ToString());

                rgTop = rgTop.Replace("#bill_no", dataView[n]["bill_no"].ToString());
                rgTop = rgTop.Replace("#lisence_no", dataView[n]["lisence_no"].ToString());

                rgTop = rgTop.Replace("#trade_countrycc", dataView[n]["trade_country"].ToString());
                rgTop = rgTop.Replace("#trade_country", getName("trade_country", dataView[n]["trade_country"].ToString()));

                rgTop = rgTop.Replace("#distinate_portcc", dataView[n]["distinate_port"].ToString());
                rgTop = rgTop.Replace("#distinate_port", getName("trade_country", dataView[n]["distinate_port"].ToString()));

                strsql = dataView[n]["district_code"].ToString();
                rgTop = rgTop.Replace("#district_codecc", strsql);
                strsql = getName("district_code", strsql);
                if (strsql.Length > 7)
                    strsql = strsql.Substring(0, 7);
                rgTop = rgTop.Replace("#district_code", getName("district_code", dataView[n]["district_code"].ToString()));


                rgTop = rgTop.Replace("#appr_no", dataView[n]["appr_no"].ToString());
                rgTop = rgTop.Replace("#trans_modecc", dataView[n]["trans_mode"].ToString());
                rgTop = rgTop.Replace("#trans_mode", getName("trans_mode", dataView[n]["trans_mode"].ToString()));

                rgTop = rgTop.Replace("#fee_markcc", dataView[n]["fee_mark"].ToString());
                rgTop = rgTop.Replace("#fee_mark", getName("fee_mark", dataView[n]["fee_mark"].ToString()));
                rgTop = rgTop.Replace("#fee_rate", dataView[n]["fee_rate"].ToString());

                rgTop = rgTop.Replace("#fee_currcc", dataView[n]["fee_curr"].ToString());
                rgTop = rgTop.Replace("#fee_curr", getName("fee_curr", dataView[n]["fee_curr"].ToString()));

                rgTop = rgTop.Replace("#insur_markcc", dataView[n]["insur_mark"].ToString());
                rgTop = rgTop.Replace("#insur_mark", getName("insur_mark", dataView[n]["insur_mark"].ToString()));
                rgTop = rgTop.Replace("#insur_rate", dataView[n]["insur_rate"].ToString());

                rgTop = rgTop.Replace("#insur_currcc", dataView[n]["insur_curr"].ToString());
                rgTop = rgTop.Replace("#insur_curr", getName("insur_curr", dataView[n]["insur_curr"].ToString()));

                rgTop = rgTop.Replace("#other_markcc", dataView[n]["other_mark"].ToString());
                rgTop = rgTop.Replace("#other_mark", getName("other_mark", dataView[n]["other_mark"].ToString()));
                rgTop = rgTop.Replace("#other_rate", dataView[n]["other_rate"].ToString());

                rgTop = rgTop.Replace("#other_currcc", dataView[n]["other_curr"].ToString());
                rgTop = rgTop.Replace("#other_curr", getName("other_curr", dataView[n]["other_curr"].ToString()));

                rgTop = rgTop.Replace("#wrap_typecc", dataView[n]["wrap_type"].ToString());
                rgTop = rgTop.Replace("#wrap_type", getName("wrap_type", dataView[n]["wrap_type"].ToString()));

                rgTop = rgTop.Replace("#pack_no", dataView[n]["pack_no"].ToString());
                rgTop = rgTop.Replace("#gross_wt", dataView[n]["gross_wt"].ToString());
                rgTop = rgTop.Replace("#net_wt", dataView[n]["net_wt"].ToString());
                rgTop = rgTop.Replace("#ex_source", dataView[n]["ex_source"].ToString());
                rgTop = rgTop.Replace("#other_rate", dataView[n]["other_rate"].ToString());
                //
                rgTop = rgTop.Replace("#RaDeclNo", dataView[n]["RaDeclNo"].ToString());
                rgTop = rgTop.Replace("#StoreNo", dataView[n]["StoreNo"].ToString());
                rgTop = rgTop.Replace("#i_e_portcc", dataView[n]["i_e_port"].ToString());
                rgTop = rgTop.Replace("#i_e_port", getName("i_e_port", dataView[n]["i_e_port"].ToString()));

                sb0 = new StringBuilder();
                string RaDeclNo = dataView[n]["RaDeclNo"].ToString();
                if (RaDeclNo.Trim() != "")
                    sb0.AppendFormat("关联报关单号：{0}<br />", RaDeclNo);

                if (dataView[n]["note_s"].ToString() != "")
                    sb0.AppendFormat("{0}<br />", dataView[n]["note_s"].ToString().Replace("<", "〈").Replace(">", "〉"));

                rgTop = rgTop.Replace("#note_s", sb0.ToString());
                sb0.Remove(0, sb0.Length);
                #endregion

                #region list

                strsql = string.Format("EXEC {1} '{0}'", g.ToSql(bgdID), pWMS_pass_BGDOut);
                DataView dv0 = g.getTable(strsql).DefaultView;
                if (dv0.Count > 0)
                {
                    int iRec = 5;
                    int k = dv0.Count;
                    string xlsData = rgTop;

                    #region ll
                    sb0 = new StringBuilder(@"
<table class='prtTab' cellspacing='0' cellpadding='0' align='Center' rules='all' bordercolor='Black' border='1' style='border-color:Black;border-width:1px;border-style:solid;width:99%;border-collapse:collapse;'>
<tr class=gtop><td>项号</td><td>商品编号</td><td>商品名称、规格型号</td><td>数量及单位</td><td>最终目的国(地区)</td><td>单价</td><td>总价</td><td>币制</td></tr>");

                    for (int i = 0; i < dv0.Count; i++)
                    {
                        sb0.AppendFormat("<tr class=gbody><td>{0}<br />{1}</td><td>{2}{3}</td><td>{4}<br />{5}</td><td>{6}{7}<br />{8}{9}<br />{10}{11}</td><td>{12}<br />{13}</td><td>{14}</td><td>{15}</td><td>{16}<br />{17}</td></tr>",
                            dv0[i]["g_no"], dv0[i]["contr_item"], dv0[i]["code_t"], dv0[i]["code_s"],
                            dv0[i]["g_name"], dv0[i]["g_model"],
                            dv0[i]["qty_conv"], getName("unit", dv0[i]["unit_1"].ToString()),
                            dv0[i]["qty_2"], getName("unit", dv0[i]["unit_2"].ToString()),
                            dv0[i]["qty_1"], getName("unit", dv0[i]["g_unit"].ToString()),
                            dv0[i]["origin"], dv0[i]["origin_country"],
                            string.Format("{0:#.0000}", g.ToDecimal(dv0[i]["decl_price"].ToString())),
                            string.Format("{0:#.00}", g.ToDecimal(dv0[i]["trade_total"].ToString())),
                            dv0[i]["trade_curr"].ToString(), dv0[i]["currency"].ToString());

                        if (i > 0 && (i + 1) % iRec == 0)
                        {
                            sb0.Append("</table>");

                            if (iPage > 1)
                            {
                                sb.Append("<div class=newPage></div>");
                            }
                            sb.Append("<p>" + xlsData.Replace("Page 1", "Page " + iPage.ToString()) + sb0.ToString() + mbFoot + "</p>");
                            iPage++;

                            if (i < k - 1)
                                sb0 = new StringBuilder(@"
<table class='prtTab' cellspacing='0' cellpadding='0' align='Center' rules='all' bordercolor='Black' border='1' style='border-color:Black;border-width:1px;border-style:solid;width:99%;border-collapse:collapse;'>
<tr class=gtop><td>项号</td><td>商品编号</td><td>商品名称、规格型号</td><td>数量及单位</td><td>最终目的国(地区)</td><td>单价</td><td>总价</td><td>币制</td></tr>");
                        }
                    }
                    dv0.Dispose();

                    if (k % iRec > 0)
                    {
                        if (k % iRec > 0)
                        {
                            for (int j = (k % iRec); j < iRec; j++)
                            {
                                sb0.Append("<tr class=gbody><td colspan=8>&nbsp;<br />&nbsp;<br />&nbsp;</td></tr>");
                            }
                            sb0.Append("</table>");
                        }
                        if (iPage > 1)
                        {
                            sb.Append("<div class=newPage></div>");
                        }
                        sb.Append("<p>" + xlsData.Replace("Page 1", "Page " + iPage.ToString()) + sb0.ToString() + mbFoot + "</p>");
                        iPage++;
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
            dataView.Dispose();
            phList.Text = sb.ToString();
            sb.Remove(0, sb.Length);
        }

        public string getCurr(string name)
        {
            if (name == "") return "";
            string strsql = "", conn = connOLE;
            strsql = string.Format("select CURR_SYMB from curr where CURR_CODE='{0}'", name);
            try
            {
                return g.getValue(strsql, conn);//connOLE
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return string.Empty;
            }
        }

        public string getName(string obj, string name)
        {
            if (name == "") return "";
            string strsql = "", conn = connOLE;
            switch (obj)
            {
                case "wrap_type"://包装种类表
                    strsql = string.Format("select WRAP_NAME from WRAP_TYPE where WRAP_CODE='{0}'", name);
                    break;
                case "use_to"://用途表
                    strsql = string.Format("select USE_TO_NAM from use_to where USE_TO_COD='{0}'", name);
                    break;
                case "unit"://计量单位表
                case "unit_1":
                case "unit_2":
                case "g_unit":
                    strsql = string.Format("select UNIT_NAME from unit where UNIT_CODE='{0}'", name);
                    break;
                case "transf"://运输方式
                case "traf_mode":
                    strsql = string.Format("select TRAF_SPEC from transf where TRAF_CODE='{0}'", name);
                    break;
                case "transac"://成交方式
                case "trans_mode":
                    strsql = string.Format("select TRANS_SPEC from transac where TRANS_MODE='{0}'", name);
                    break;
                case "trade"://贸易方式
                case "trade_mode":
                    strsql = string.Format("select ABBR_TRADE from trade where TRADE_MODE='{0}'", name);
                    break;
                case "port"://港口代码表
                    strsql = string.Format("select PORT_NAME from port where PORT_CODE='{0}'", name);
                    break;
                case "pay_mode"://纳税方式
                    strsql = string.Format("select PAY_NAME from pay_mode where PAY_CODE='{0}'", name);
                    break;
                case "other_mark"://杂费标记表
                    strsql = string.Format("select NAME from other_mark where Code='{0}'", name);
                    break;
                case "levytype"://征免性质表
                case "cut_mode":
                    strsql = string.Format("select ABBR_CUT from levytype where CUT_MODE='{0}'", name);
                    break;
                case "levymode"://征免方式表
                case "duty_mode":
                    strsql = string.Format("select DUTY_SPEC from levymode where DUTY_MODE='{0}'", name);
                    break;
                case "lc_type"://结汇方式
                    strsql = string.Format("select PAY_NAME from lc_type where PAY_WAY='{0}'", name);
                    break;
                case "insur_mark"://保费标记表
                    strsql = string.Format("select NAME from insur_mark where Code='{0}'", name);
                    break;
                case "fee_mark"://运费标记表 
                    strsql = string.Format("select NAME from Fee_Mark where Code='{0}'", name);
                    break;
                case "district"://地区代码表
                case "district_code":
                    strsql = string.Format("select DISTRICT_N from district where DISTRICT_C='{0}'", name);
                    break;
                case "customs"://关区代码表
                case "i_e_port":
                    strsql = string.Format("select CUSTOMS_NA from CUSTOMS where CUSTOMS_CO='{0}'", name);
                    break;
                case "curr"://币制表
                case "trade_curr":
                case "fee_curr":
                case "insur_curr":
                case "other_curr":
                    strsql = string.Format("select CURR_NAME from curr where CURR_CODE='{0}'", name);
                    break;
                case "country"://国别地区代码表
                case "distinate_port":
                case "trade_country":
                case "origin_country":
                    strsql = string.Format("select COUNTRY_NA from country where COUNTRY_CO='{0}'", name);
                    break;
                case "conta_model"://箱型信息
                case "container_model":
                    strsql = string.Format("select CONTA_MODEL_NAME from conta_model where CONTA_MODEL_CO='{0}'", name);
                    break;
                case "co_type"://企业性质
                case "trade_type"://企业性质
                    strsql = string.Format("select CO_OWNERSH from CO_TYPE where CO_OWNER='{0}'", name);
                    break;
                case "company"://公司信息   
                case "trade_co": //经营单位
                case "owner_code": //收货单位
                case "agent_code": //申报单位
                    strsql = string.Format("select FULL_CO from company where TRADE_CO='{0}'", name);
                    conn = connGS;
                    break;
                default:
                    strsql = "";
                    break;
            }
            try
            {
                return g.getValue(strsql, conn);//connOLE
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return string.Empty;
            }

        }
    }
}