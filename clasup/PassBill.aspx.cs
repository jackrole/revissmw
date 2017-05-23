using System;
using System.Data;
using System.Web.UI;
using System.Text;
using System.IO;

namespace Export.Clasup
{
    public partial class PassBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            CreateBill(g.GetRequest("passid"));
        }

        private void CreateBill(string passID)
        {
            if (passID == "") return;

            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<div align=center style='height:50px;line-height:30px;padding-top:20px;'><h4><b>移库通知书</b></h4></div>");
            sb.AppendFormat("<div align=center style='height:50px'><img width='200' src='../codes.aspx?no={0}' /><br /><font size=4>{0}</font></div>", passID);

            string tableRowFormat = "<tr><td>{1}</td><td>{0}</td></tr>";
            string tableDetailHeader = "<tr align=center><td>序号</td><td>料号</td><td>货架区</td><td>货架存量</td><td>移库数量</td><td>余留数量</td></tr>";
            string tableDetailRow = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>";

            DataTable dateTable = g.getTable(sqltPass.FormatSql(passID));
            if (dateTable.Rows.Count > 0)
            {
                sb.Append("<table width=800 align=center border=1 bordercolor='#000' class='prtTab'>");
                sb.AppendFormat(tableRowFormat, dateTable.Rows[0]["boxNum"], "箱数");
                sb.AppendFormat(tableRowFormat, dateTable.Rows[0]["pcs"], "件数");
                sb.AppendFormat(tableRowFormat, dateTable.Rows[0]["isUnderBounded"].ToString() == "0" ? "非保税" : "保税", "货物属性");

                lRKD.Text = sb.ToString();
                sb.Remove(0, sb.Length);

                DataTable dataTableDetail = g.getTable(string.Format(sqlPassDetail, g.ToSql(passID)));
                sb.Append("<table width=800 align=center border=1 bordercolor='#000' class='prtTab'>");
                sb.Append(tableDetailHeader);
                for (int i = 0; i < dataTableDetail.Rows.Count; i++)
                {
                    var pcs = dataTableDetail.Rows[i]["pcs"].ToString();
                    var transferPcs = dataTableDetail.Rows[i]["transfer_pcs"].ToString();
                    sb.AppendFormat(tableDetailRow,
                      i + 1,
                      dataTableDetail.Rows[i]["sku"],
                      dataTableDetail.Rows[i]["kwcode"],
                      pcs,
                      transferPcs,
                      g.getDecimal(pcs) - g.getDecimal(transferPcs)
                    );
                }
                dataTableDetail.Dispose();
                sb.Append("</table>");
            }
            dateTable.Dispose();
            pSonghuo.Text = sb.ToString();
            sb.Remove(0, sb.Length);
        }

        protected void btnXLS_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "gb2312";

            Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/ms-execl";

            this.EnableViewState = false;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ZH-CN", true);
            StringWriter stringWriter = new StringWriter(ci);
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            pSonghuo.RenderControl(htmlTextWriter);
            string xlsData = stringWriter.ToString();

            Response.Write(xlsData);
            Response.End();
        }

        private static readonly SQLTemplate sqltPass = SQLTemplate.New(
          "SELECT A.PASSTIME, A.BOXNUM, A.PCS, A.ISUNDERBOUNDED FROM CLASUP_PASS A WHERE A.ID={0S}"
        );
        private static readonly string sqlPassDetail = "EXEC wms2_pass_billgoods '{0}'";
    }
}
