using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Export.Clasup
{
  public partial class CustomCountList : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      DrawList(g.GetRequest("passid"));
    }

    private void DrawList(string passId)
    {
      gList.DataSource = g.getTable("select *, passid code from [clasup_passdetail]");
      gList.DataBind();
      if (passId == "") return;


    }

    public string getCurr(string name)
    {
      return name;
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
      return name;
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

    protected string connGS = "";
    protected string connOLE = g.getConfig("ConnectStringOle");
  }
}