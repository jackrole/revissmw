using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace Export.Clasup
{
    public partial class JYS : System.Web.UI.Page
    {
        protected string connOLE = g.getConfig("ConnectStringOle");
        protected System.Web.UI.WebControls.Repeater rList;
        protected string connGS = "";

        private static readonly string wms2_pass_BGD = "wms2_pass_BGD";
        private static readonly string pWMS_pass_BGD_ALL = "pWMS_pass_BGD";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack) LoadData();
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
            if (passID.StartsWith("0"))
                g.ExecSql(string.Format("exec {1} '{0}'", g.ToSql(passID), wms2_pass_BGD));

            TRANS_PRE_ID.Text = passID;
            var strsql = SQLTemplate.New(
              "SELECT IE_FLAG, PRE_ENTRY_ID, CUSTOMS_ID, MANUAL_NO, CONTR_NO, I_E_DATE, D_DATE, ",
              "  TRADE_CO, TRADE_NAME, OWNER_CODE, OWNER_NAME, AGENT_CODE, AGENT_NAME, ",
              "  TRAF_MODE, TRAF_NAME, VOYAGE_NO, BILL_NO, TRADE_MODE, CUT_MODE, IN_RATIO, PAY_WAY, ",
              "  LISENCE_NO, TRADE_COUNTRY, DISTINATE_PORT, DISTRICT_CODE, APPR_NO, TRANS_MODE, ",
              "  FEE_MARK, FEE_RATE, FEE_CURR, INSUR_MARK, INSUR_RATE, INSUR_CURR, OTHER_MARK, ",
              "  OTHER_RATE, OTHER_CURR, PACK_NO, WRAP_TYPE, GROSS_WT, NET_WT, EX_SOURCE, RADECLNO, RAMANUALNO, ",
              "  STORENO, I_E_PORT, NOTE_S,SYN",
              "FROM FORM_HEAD WHERE YWNO={0S}"
            ).FormatSql(passID);

            DataView dataView = g.getTable(strsql).DefaultView;
            StringBuilder sb = new StringBuilder();
            #region read data
            if (dataView.Count > 0)
            {
                pre_entry_id.Enabled = false;
                pre_entry_id.Text = dataView[0]["pre_entry_id"].ToString();
                Code_2.Text = string.Format("<img src='../QRcode.aspx?no={0}' style='height:14mm' />", pre_entry_id.Text);
                Code_39.Text = string.Format("<img src='../Codes.aspx?no={0}' /><br /><font size=4>{0}</font>", pre_entry_id.Text);
                customs_id.Text = dataView[0]["customs_id"].ToString();
                strsql = dataView[0]["contr_no"].ToString();
                if (strsql.Length > 27)
                    contr_no.Text = strsql.Substring(0, 27);
                else
                    contr_no.Text = strsql;
                i_e_date.Text = dataView[0]["i_e_date"].ToString();
                d_date.Text = dataView[0]["d_date"].ToString();

                cut_modecc.Text = dataView[0]["cut_mode"].ToString();
                cut_mode.Text = getName("cut_mode", cut_modecc.Text);

                bill_no.Text = dataView[0]["bill_no"].ToString();
                lisence_no.Text = dataView[0]["lisence_no"].ToString();

                trade_countrycc.Text = dataView[0]["trade_country"].ToString();
                trade_country.Text = getName("trade_country", trade_countrycc.Text);

                distinate_portcc.Text = dataView[0]["distinate_port"].ToString();
                distinate_port.Text = getName("trade_country", distinate_portcc.Text);

                district_codecc.Text = dataView[0]["district_code"].ToString();
                district_code.Text = getName("district_code", district_codecc.Text);
                if (district_code.Text.Length > 7)
                    district_code.Text = district_code.Text.Substring(0, 7);

                appr_no.Text = dataView[0]["appr_no"].ToString();
                pack_no.Text = dataView[0]["pack_no"].ToString();

                gross_wt.Text = dataView[0]["gross_wt"].ToString();
                net_wt.Text = dataView[0]["net_wt"].ToString();
                ex_source.Text = dataView[0]["ex_source"].ToString();

                string RaDeclNo = dataView[0]["RaDeclNo"].ToString();
                if (RaDeclNo.Trim() != "")
                    sb.AppendFormat("关联报关单号：{0}<br />", RaDeclNo);

                if (dataView[0]["note_s"].ToString() != "")
                    sb.AppendFormat("{0}<br />", dataView[0]["note_s"].ToString().Replace("<", "〈").Replace(">", "〉"));
            }
            #endregion
            dataView.Dispose();

            // Table clasup_pass has no `memo` field, skipping append memo string here.
            //strsql = g.getSValue(string.Format("SELECT memo FROM inbound WHERE rkno = '{0}'", g.ToSql(transPreID)));
            //if (!string.IsNullOrEmpty(strsql)) sb.Append(strsql);
            note_s.Text = sb.ToString();
            sb.Remove(0, sb.Length);

            DataTable dt;
            strsql = string.Format("EXEC {1} '{0}'", g.ToSql(g.GetRequest("no")), pWMS_pass_BGD_ALL);
            dataView = g.getTable(strsql).DefaultView;
            if (dataView.Count > 0)
            {
                int iRec = 5;
                int k = dataView.Count;

                if (k < iRec)
                {
                    dt = dataView.Table;
                    for (int i = k; i < iRec; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["unit_1"] = "";
                        dr["unit_2"] = "";
                        dr["g_unit"] = "";
                        dr["origin_country"] = "";
                        dr["trade_curr"] = "";
                        dr["duty_mode"] = "";
                        dr["use_to"] = "";
                        dt.Rows.Add(dr);
                    }
                    gList.DataSource = dt;
                    gList.DataBind();
                }
                else
                {
                    Label lList = new Label();

                    System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ZH-CN", true);
                    System.IO.StringWriter stringWriter = new System.IO.StringWriter(ci);
                    HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                    pTOP.RenderControl(htmlTextWriter);
                    string xlsData = stringWriter.ToString();

                    sb = new StringBuilder(@"
<table class='prtTab' cellspacing='0' cellpadding='0' align='Center' rules='all' bordercolor='Black' border='1' style='border-color:Black;border-width:1px;border-style:solid;width:99%;border-collapse:collapse;'>
<tr class=gtop><td>项号</td><td>商品编号</td><td>商品名称、规格型号</td><td>数量及单位</td><td>原产国（地区）</td><td>单价</td><td>总价</td><td>币制</td><td>征免</td></tr>");

                    for (int i = 0; i < dataView.Count; i++)
                    {
                        sb.AppendFormat("<tr class=gbody><td>{0}<br />{1}</td><td>{2}{3}</td><td>{4}<br />{5}</td><td>{6}{7}<br />{8}{9}<br />{10}{11}</td><td>{12}<br />{13}</td><td>{14}</td><td>{15}</td><td>{16}<br />{17}</td><td>{18}<br />{19}</td></tr>",
                            dataView[i]["g_no"], dataView[i]["contr_item"], dataView[i]["code_t"], dataView[i]["code_s"],
                            dataView[i]["g_name"], dataView[i]["g_model"],
                            dataView[i]["qty_conv"], getName("unit", dataView[i]["unit_1"].ToString()),
                            dataView[i]["qty_2"], getName("unit", dataView[i]["unit_2"].ToString()),
                            dataView[i]["qty_1"], getName("unit", dataView[i]["g_unit"].ToString()),
                            dataView[i]["origin"], dataView[i]["origin_country"],
                            string.Format("{0:#.0000}", g.ToDecimal(dataView[i]["decl_price"].ToString())),
                            string.Format("{0:#.00}", g.ToDecimal(dataView[i]["trade_total"].ToString())),
                            dataView[i]["trade_curr"], dataView[i]["currency"],
                            getName("duty_mode", dataView[i]["duty_mode"].ToString()), getName("use_to", dataView[i]["use_to"].ToString()));

                        if (i > 0 && (i + 1) % iRec == 0)
                        {
                            sb.Append("</table>");

                            if (phList.Controls.Count > 0)
                            {
                                lList.Text = "<div class=newPage></div>";
                                phList.Controls.Add(lList);
                                lList = new Label();
                            }
                            lList.Text = "<p>" + xlsData.Replace("Page 1", "Page " + Math.Floor(Convert.ToDouble(i / iRec + 1)).ToString()) + sb.ToString() + pFoot.Text + "</p>";
                            phList.Controls.Add(lList);

                            lList = new Label();
                            sb = new StringBuilder(@"
<table class='prtTab' cellspacing='0' cellpadding='0' align='Center' rules='all' bordercolor='Black' border='1' style='border-color:Black;border-width:1px;border-style:solid;width:99%;border-collapse:collapse;'>
<tr class=gtop><td>项号</td><td>商品编号</td><td>商品名称、规格型号</td><td>数量及单位</td><td>原产国（地区）</td><td>单价</td><td>总价</td><td>币制</td><td>征免</td></tr>");
                        }
                    }
                    if (k % iRec > 0)
                    {
                        lList.Text = "<div class=newPage></div>";
                        phList.Controls.Add(lList);
                        lList = new Label();

                        if (k % iRec > 0)
                        {
                            for (int i = (k % iRec); i < iRec; i++)
                            {
                                sb.Append("<tr class=gbody><td colspan=9>&nbsp;<br />&nbsp;<br />&nbsp;</td></tr>");
                            }
                            sb.Append("</table>");
                        }

                        lList.Text = "<p>" + xlsData.Replace("Page 1", "Page " + Math.Floor(Convert.ToDouble(k / iRec + 1)).ToString()) + sb.ToString() + pFoot.Text + "</p>";
                        phList.Controls.Add(lList);
                    }
                    pTOP.Visible = pFoot.Visible = gList.Visible = false;
                }
            }
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