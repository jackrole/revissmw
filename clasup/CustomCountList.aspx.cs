using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Export.Clasup
{
  public partial class CustomCountList : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      // var passid = "1507130008"
      DrawList(g.GetRequest("passid"), g.GetRequest("page"));
    }

    class SuperTable
    {
      int _maxCols = 0;
      List<string> _head = new List<string>();
      List<List<string>> _cellMatrix = new List<List<string>>();

      public SuperTable()
      {
        _maxCols = 0;
      }

      public SuperTable(int maxCols)
      {
        _maxCols = maxCols;
      }

      public void AddHead(string head, string spliter = ",")
      {
        AddHead(SplitLineString(head, spliter).ToArray());
      }

      public void AddHead(params string[] colNames)
      {
        _head.Clear();
        foreach (var colName in colNames)
        {
          _head.Add(colName);
        }
      }

      public void AddRow(string cellsString, string spliter = ",")
      {
        AddRow(SplitLineString(cellsString, spliter).ToArray());
      }

      public void AddRow(params string[] cells)
      {
        var row = new List<string>();
        foreach (var cell in cells)
        {
          row.Add(cell);
        }
        _cellMatrix.Add(row);
      }

      private List<string> SplitLineString(string colNames, string spliter = ",")
      {
        return new List<string>(Regex.Split(colNames, spliter));
      }

      private string HeadStr(string colNames, string spliter = ",")
      {
        return HeadStr(SplitLineString(colNames, spliter));
      }

      private string HeadStr(List<string> colNames)
      {
        return "<tr>" + string.Join("", colNames.ConvertAll(x => "<th>" + x.Trim() + "</th>").ToArray()) + "</tr>";
      }

      private string RowStr(string cellValues, string spliter = ",")
      {
        return RowStr(SplitLineString(cellValues, spliter));
      }

      private string RowStr(List<string> cellValues)
      {
        return "<tr>" + string.Join("", cellValues.ConvertAll(x => "<td>" + x + "</td>").ToArray()) + "</tr>";
      }

      public string ToTableString()
      {
        int maxCols;
        if (_maxCols == 0)
        {
          var wholeMatrix = (new List<List<string>>(_cellMatrix) { _head }).ConvertAll(x => x.Count);
          wholeMatrix.Sort();
          maxCols = wholeMatrix[wholeMatrix.Count - 1];
        }
        else
        {
          maxCols = _maxCols;
        }

        Action<List<string>> converter = x => { if (x.Count < maxCols) x.AddRange(new string[maxCols - x.Count]); };

        converter(_head);
        _cellMatrix.ForEach(x => converter(x));

        var headString = HeadStr(_head);
        var bodyString = string.Join("", _cellMatrix.ConvertAll(x => RowStr(x)).ToArray());

        return string.Format(
          "<table><thead>{0}</thead><tbody>{1}</tbody></table>",
          headString, bodyString
        );
      }
    }

    private void DrawList(string passId, string page = "1")
    {
      var superTable = new SuperTable();
      var dataView = g.getTable(sqlTemplate.FormatSql(passId, countryID)).DefaultView;

      for (int i = 0; i < dataView.Count; i++)
      {
        Func<string, string> row = x => dataView[i][x].ToString();
        var cells = new List<string>();

        cells.Add((i + 1).ToString());
        cells.Add(row("CODE_T") + row("CODE_S"));
        cells.Add(row("G_NAME"));
        cells.Add(row("QTY_CONV") + getName("unit", row("UNIT_1")));  // 数量及单位   dv0[i]["qty_conv"], getName("unit", dv0[i]["unit_1"))
        cells.Add(row("ORIGIN_NAME"));
        cells.Add(row("CURRENCY"));

        //cells.Add(string.Format("{0:#.0000}", g.ToDecimal(row("decl_price"))));  // 单价
        //cells.Add(string.Format("{0:#.00}", g.ToDecimal(row("trade_total"))));  // 总价
        superTable.AddRow(cells.ToArray());
        cells.Clear();

        cells.Add(row("CONTR_ITEM"));
        cells.Add("");
        cells.Add(row("G_MODEL"));
        cells.Add(row("QTY_2") + getName("unit", row("UNIT_2")));  // 数量及单位   dv0[i]["qty_2"], getName("unit", dv0[i]["unit_2"))
        cells.Add(row("ORIGIN"));
        cells.Add(row("CURRENCY_NAME"));
        superTable.AddRow(cells.ToArray());
        cells.Clear();

        cells.Add("");
        cells.Add("");
        cells.Add("");
        cells.Add(row("QTY_1") + getName("unit", row("G_UNIT")));  // 数量及单位   dv0[i]["qty_1"], getName("unit", dv0[i]["g_unit"))
        superTable.AddRow(cells.ToArray());
      }
      
      superTable.AddHead("项号,商品编号,商品名称、规格型号,数量及单位,原产国（地区）,币制,征免");
      passList.Text = superTable.ToTableString();
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

    private string countryID = "142";

    private SQLTemplate sqlTemplate = SQLTemplate.New(
        "SELECT L.*",
        ", {1S} AS ORIGIN",
        ", DBO.F_ORIGIN0({1S}) AS ORIGIN_NAME",
        ", DBO.F_CURR0(TRADE_CURR) AS CURRENCY",
        ", DBO.F_CURR1(DBO.F_CURR0(TRADE_CURR)) AS CURRENCY_NAME",
        " FROM FORM_HEAD AS H, FORM_LIST AS L WHERE H.PRE_ENTRY_ID = L.PRE_ENTRY_ID AND H.RKNO = {0S}"
    );
  }
}