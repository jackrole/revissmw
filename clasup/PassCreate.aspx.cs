using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using Aspose.Cells;
using System.Web.UI.WebControls;

namespace Export.Clasup
{
  public partial class PassCreate : System.Web.UI.Page
  {
    SQLTemplate sqlTemplate = null;
    protected string kind = "1";

    protected void Page_Load(object sender, System.EventArgs e)
    {
      g.chkSession();
      if (!Page.IsPostBack)
        LoadData();
      if (g.GetRequest("kind") == "1")
        btnSave.Enabled = false;
      if (g.getSession("URole") == "客户")
      {
        btnSave.Visible = btnBGD.Visible = btnLock.Visible = btnSearch.Visible = false;
      }
    }

    private bool ChkNW(string FileName)
    {
      bool b = false;
      if (FileName.ToLower().EndsWith(".xls"))
      {
        Workbook wb = new Workbook(FileName);
        Worksheet ws = wb.Worksheets[0];
        ws.Name = "sheet1";
        FileName = FileName.ToLower() + "x";
        wb.Save(FileName, SaveFormat.Xlsx);
        System.Threading.Thread.Sleep(3000);
      }

      //检查数据为0的情况
      b = g.getInt(g.getValue("select count(1) from [sheet1$] where cdbl([TOT_NET_WEIGHT])=0", string.Format(g.getConfig("dbxls"), FileName))) > 0;
      return b;
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
      bool IsAdd = false;
      if (iptPassNo.Text.Trim() == "")
      {
        iptPassNo.Text = "2" + g.MakeOrderNo("pass");
        iptPassNo.ReadOnly = IsAdd = true;
      }
      string fileName = UploadFile(tFile.PostedFile, false);
      string xlsPath = hFile.Text.Trim();
      if (IsAdd)
      {
        sqlTemplate = SQLTemplate.New(
            "INSERT INTO SHP_MAST(RKNO,CW) VALUES({0S},{1I}); INSERT INTO CLASUP_PASS(ID) VALUES ({0S})"
        );
        g.Exec(sqlTemplate.FormatSql(g.ToSql(iptPassNo.Text), g.ToDecimal(iptCw.Text)));
      }

      sqlTemplate = SQLTemplate.New(
          "UPDATE CLASUP_PASS SET PASSTIME={1D}, BOXNUM={2I}, PCS={3I}, CW={4N}, NW={5N},",
          "PAY={6N}, CURRKIND={7S}, CID={8S}, ISUNDERBOUNDED={9B}, CREATOR={10S}, isTransferOut={11B},",
          "isDeclared={12B}, isDeclareCommitted={13B}, isPassed={14B}, isTransferIn={15B}",
          ", PI_SPECIAL={16B}, PI_PRICE={17B}, PI_PAY={18B}",
          " WHERE ID={0S}"
      );

      g.Exec(sqlTemplate.FormatSql(
          this.iptPassNo.Text, this.iptPassTime.Text, this.iptBoxNum.Text, this.iptPcs.Text,
          this.iptCw.Text, this.iptNw.Text, this.iptPay.Text, this.iptCurrKind.Text, this.ddlCid.Text,
          this.ddlIsUnderBounded.SelectedValue == "1",
          g.getSession("UID"), this.cb1.Checked, this.cb2.Checked, this.cb3.Checked,
          this.cb4.Checked, this.cb5.Checked,
          PI_SPECIAL.Checked, PI_PRICE.Checked, PI_PAY.Checked
      ));

      if (fileName != "0" && fileName != "-1")
      {
        xlsPath = Server.MapPath("../upload/");
        if (!fileName.StartsWith(xlsPath))
          fileName = xlsPath + fileName;

        string xlsName = fileName;
        if (fileName.EndsWith(".xls"))
          xlsName = fileName + "x";
        if (IsAdded(xlsName, fileName))
        {
          g.Alert("您已经导入过该入库箱单了！");
          File.Delete(fileName);
          return;
        }
        #region add detail

        xlsPath = g.getConfig("XLSPath");
        if (xlsName != xlsPath)
          System.IO.File.Copy(xlsName, xlsPath, true);

        try
        {
          AddDetail(true);

          if (g.getSValue(string.Format("SELECT 1 FROM CLASUP_PASSDETAIL WHERE PASSID='{0}' AND PCS=0", iptPassNo.Text)) == "1")
          {
            g.Alert("上传的附件里面有些件数为0！");
          }
          string sku = g.getSValue(string.Format("exec pSKU_CHG '{0}'", iptPassNo.Text));
          if (sku != "")
          {
            if (!ClientScript.IsClientScriptBlockRegistered("jsSKU"))
              ClientScript.RegisterClientScriptBlock(this.GetType(), "jsSKU",
                  "<script type='text/javascript'>alert('上传成功，存在没有海关编码的SKU数据，请完善！');popResizable('../public/DataContainer.aspx?Obj=items_sku0&kind=1',600,500);</script>");
          }
          else
            g.Alert("箱单上传成功！");
        }
        catch (Exception ex)
        {
          g.Alert("数据导入失败：" + ex.Message);
        }
        #endregion
      }
    }

    private void AddDetail(bool isAdd)
    {
      sqlTemplate = SQLTemplate.New(
          "INSERT INTO CLASUP_PASSDETAIL (PASSID, SEQ, SKU, PCS, NW, CW, PAY, CURRKIND, ORIGIN, CONTAINERNO, COLOR, SIZE, CREATETIME)",
          "SELECT {0s},ROW_NUMBER() OVER (ORDER BY [料号]),[料号],[件数],[毛重],[净重],[金额],[币种],[国家],[箱号],[颜色],[尺寸],{1d} ",
          "FROM HGBBD...[sheet1$] WHERE [料号]<>''"
      );
      g.Exec(sqlTemplate.FormatSql(iptPassNo.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
    }

    private bool IsAdded(string xlsName, string fileName)
    {
      Workbook wb = new Workbook(fileName);
      Worksheet ws = wb.Worksheets[0];
      string bundle = ws.Cells[0].StringValue;
      ws.Name = "sheet1";
      //wb.Save(xlsName, SaveFormat.Xlsx);
      wb.Save(xlsName, SaveFormat.Excel97To2003);
      System.Threading.Thread.Sleep(3000);

      sqlTemplate = SQLTemplate.New("SELECT 1 FROM CLASUP_PASSDETAIL WHERE SKU={0S} AND PASSID={1S}");
      return g.getSValue(sqlTemplate.FormatSql(bundle, iptPassNo.Text)) == "1";
    }

    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
      LoadData(iptPassNo.Text, "");
    }

    protected void btnBGD_Click(object sender, EventArgs e)
    {
      if (iptPassNo.Text.Trim() == "")
      {
        g.Alert("入库单还没有录入！");
        return;
      }

      string sku = g.getSValue(string.Format("exec wms2_p_CHK '{0}'", g.ToSql(iptPassNo.Text)));
      string skude = g.getSValue("select 1 from Cls_Desc where code<>'' and isnull(cname,'')=''");
      string skuor = g.getSValue("select 1 from Cls_Origin where cname<>'' and isnull(code,'')=''");
      string skuta = g.getSValue("select 1 from Cls_tariff where code<>'' and isnull(cname,'')=''");
      string skucu = g.getSValue("select 1 from CURRENCY where cname<>'' and isnull(code,'')=''");
      string skubgd = g.getSValue(string.Format("select 1 from vRK_PLAN_ORI where iptPassNo='{0}'", g.ToSql(iptPassNo.Text)));
      jsLoad.Text = "";
      if (sku + skude + skuor + skuta + skucu != "")
      {
        StringBuilder sb = new StringBuilder();
        sb.Append("<h4>请完善下列信息：<h4/><br /><script language=\"javascript\">alert('请完善下列信息！');</script>");
        sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
            sku == "1" ? "<a href=\"javascript:popResizable('../public/DataContainer.aspx?Obj=items_sku10&kind=1&no=" + iptPassNo.Text + "',600,500);\">完善料号信息<a>" : "",
            skude == "1" ? "<a href=\"javascript:popResizable('../public/DataContainer.aspx?Obj=Cls_Desc1&kind=1',600,500);\">完善款式信息<a>" : "",
            skuor == "1" ? "<a href=\"javascript:popResizable('../public/DataContainer.aspx?Obj=Cls_Origin1&kind=1',600,500);\">完善国家代码<a>" : "",
            skuta == "1" ? "<a href=\"javascript:popResizable('../public/DataContainer.aspx?Obj=Cls_tariff1&kind=1',600,500);\">完善制造方式<a>" : "",
            skucu == "1" ? "<a href=\"javascript:popResizable('../public/DataContainer.aspx?Obj=CURRENCY1&kind=1',600,500);\">完善币种信息<a>" : "",
            skubgd == "1" ? "<a href=\"javascript:popResizable('../public/DataContainer.aspx?Obj=vRK_PLAN_ORI&iptPassNo=" + iptPassNo.Text + "&kind=1',600,500);\">完善箱单信息<a>" : "");
        jsLoad.Text = sb.ToString();
        sb.Remove(0, sb.Length);
      }
      else
      {
        string bgd = g.getSValue(string.Format("select 1 from form_head where rkno='{0}'", g.ToSql(iptPassNo.Text)));
        int i = g.ExecSql(string.Format("exec pWMS_pass_BGD '{0}',{1}", g.ToSql(iptPassNo.Text), g.getInt(bgd)));
        if (i > 0)
        {
          g.Alert("报关单生成成功！");
          string xmlPath = Server.MapPath("../xml/") + iptPassNo.Text + "/";
          if (Directory.Exists(xmlPath))
            g.DeleteADirectory(xmlPath);
          Directory.CreateDirectory(xmlPath);
        }
      }
    }

    private void LoadData()
    {
      btnBGD.Visible = false;
      string iptPassNo = g.GetRequest("no");
      string sSigleTestID = g.GetRequest("id");
      //cid.Text = "inditex";
      //SigleTestID.Text = sSigleTestID;
      iptPassTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

      DataTable dt = g.getTable("SELECT CODE,CCNAME FROM OUTCUST ORDER BY CODE");
      ddlCid.DataSource = dt;
      ddlCid.DataTextField = "CCNAME";
      ddlCid.DataValueField = "CODE";
      ddlCid.DataBind();
      ddlCid.Items.Insert(0, new ListItem("", ""));

      dt.Dispose();

      if (iptPassNo != "" || sSigleTestID != "")
      {
        LoadData(iptPassNo, sSigleTestID);
        string searchFile = iptPassNo + "*.xlsx";
        if (!Directory.Exists(Server.MapPath("../upload/") + hFile.Text))
          return;
        string[] arrList = Directory.GetFiles(Server.MapPath("../upload/") + hFile.Text, searchFile, SearchOption.AllDirectories);
        if (arrList.Length > 0)
        {
          StringBuilder sb = new StringBuilder();
          sb.Append("<b>custom file：</b>");
          foreach (string s in arrList)
          {
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;<a href=\"../upload/{0}\">{1}</a>",
                s.Replace("\\", "/").Substring(s.IndexOf("upload") + 7), s.Substring(s.IndexOf(iptPassNo)));
          }

          jsLoad.Text = sb.ToString();
          sb.Remove(0, sb.Length);
        }
      }
    }

    private void LoadData(string passID, string sSigleTestID)
    {
      string sql = "Select * from CLASUP_PASS";
      if (passID != "")
        sql += string.Format(" WHERE ID='{0}'", g.ToSql(passID));
      else
        //sql += string.Format(" WHERE SIGLETESTID={0}", g.ToSql(sSigleTestID));
        return;

      DataTable dt = g.getTable(sql);

      if (dt.Rows.Count > 0)
      {
        Func<string, string> colVal = cName => dt.Rows[0][cName].ToString();

        g.getSelect(ddlIsUnderBounded, colVal("ISUNDERBOUNDED").ToLower() == "true" ? "1" : "0");
        g.getSelect(ddlCid, colVal("CID"));

        iptPassTime.Text = colVal("PASSTIME");
        iptBoxNum.Text = colVal("BOXNUM");
        iptPcs.Text = colVal("PCS");
        iptCw.Text = colVal("CW");
        iptNw.Text = colVal("NW");
        iptPay.Text = colVal("PAY");
        iptCurrKind.Text = colVal("CURRKIND");

        cb1.Checked = g.ToBool(colVal("isTransferOut"));
        cb2.Checked = g.ToBool(colVal("isDeclared"));
        cb3.Checked = g.ToBool(colVal("isDeclareCommitted"));
        cb4.Checked = g.ToBool(colVal("isPassed"));
        cb5.Checked = g.ToBool(colVal("isTransferIn"));

        PI_SPECIAL.Checked = g.ToBool(colVal("PI_SPECIAL"));
        PI_PRICE.Checked = g.ToBool(colVal("PI_PRICE"));
        PI_PAY.Checked = g.ToBool(colVal("PI_PAY"));

        //cb1.Enabled = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canfx1,0)=1", g.ToSql(g.getSession("UID")))) == "1";
        //cb2.Enabled = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canfx2,0)=1", g.ToSql(g.getSession("UID")))) == "1";
        //cb3.Enabled = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canfx3,0)=1", g.ToSql(g.getSession("UID")))) == "1";
        //cb4.Enabled = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canfx4,0)=1", g.ToSql(g.getSession("UID")))) == "1";
        //cb5.Enabled = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canfx5,0)=1", g.ToSql(g.getSession("UID")))) == "1";

        btnBGD.Visible = true;
        string bgd = g.getSValue(string.Format("select 1 from form_head where rkno='{0}'", g.ToSql(iptPassNo.Text)));
        if (g.getInt(bgd) > 0)
        {
          btnBGD.Text = "重新生成报关单";
          btnBGD.Attributes.Add("onclick", "return confirm('报关单已经存在了，您确定要重新生成报关单吗？');");
        }

        /*bool IsLocked = g.ToBool(dt.Rows[0]["IsLocked"].ToString());
        if (IsLocked)
        {
            btnBGD.Visible = btnQZRK.Visible = btnSave.Visible = btnSingle.Visible = Button1.Visible = false;
            bgd = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canqzrk,0)=1", g.ToSql(g.getSession("UID"))));
            btnLock.Visible = bgd == "1";
        }
        else
            btnBGD.Visible = btnSave.Visible = btnSingle.Visible = true;
        bgd = dt.Rows[0]["status"].ToString();
        if (g.getInt(bgd) == 0)
        {
            bgd = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canqzrk,0)=1", g.ToSql(g.getSession("UID"))));
            btnQZRK.Visible = bgd == "1";
        }*/
      }
      dt.Dispose();
    }

    private string UploadFile(HttpPostedFile hpf, bool IsSKU)
    {
      if (hpf.ContentLength <= 0) return "0";
      string extendName = g.GetExt(hpf.FileName);

      string fileName = iptPassNo.Text + extendName;
      if (IsSKU)
        fileName = iptPassNo.Text + "_cc" + extendName;
      if (extendName.EndsWith(".xls") || extendName.EndsWith(".xlsx") || extendName.EndsWith(".zip"))
      {
        string filePath = hFile.Text;
        if (string.IsNullOrEmpty(filePath))
          hFile.Text = filePath = DateTime.Now.ToString("yyyyMM");
        string xlsPath = Server.MapPath("../upload/");

        if (!System.IO.Directory.Exists(xlsPath + filePath))
          System.IO.Directory.CreateDirectory(xlsPath + filePath);
        if (File.Exists(xlsPath + filePath + "/" + fileName))
          fileName = iptPassNo.Text + "_" + DateTime.Now.ToString("HHmmss") + extendName;

        if (hpf.ContentLength < 2048000)
          hpf.SaveAs(xlsPath + filePath + "/" + fileName);
        else
        {
          //处理上载的文件流信息。
          byte[] b = new byte[hpf.ContentLength];
          System.IO.Stream fs = (System.IO.Stream)hpf.InputStream;
          fs.Read(b, 0, hpf.ContentLength);
          UploadBigFile ubf = new UploadBigFile();
          ubf.UploadFile(b, xlsPath + filePath + "/" + fileName);
        }
        if (extendName.EndsWith(".zip"))
        {
          ClsZip.UnZip(xlsPath + filePath + "/" + fileName, xlsPath + filePath + "/" + fileName.Replace(".zip", ".xls"), 1024);
          fileName = fileName.Replace(".zip", ".xls");
        }
        return filePath + "/" + fileName;
      }
      else
      {
        g.Alert("必须是xlsx文件！"); return "-1";
      }
    }

    protected void btnSingle_Click(object sender, System.EventArgs e)
    {
      if (iptPassNo.Text.Trim() == "")
      {
        g.Alert("入库单还没有录入！");
      }
      else
      {
        int i = g.getInt(g.getValue(string.Format("select SigleTestID from InBound where iptPassNo='{0}'", g.ToSql(iptPassNo.Text))).ToString());
        if (i > 0)
        {
          if (!ClientScript.IsClientScriptBlockRegistered("jsOpen"))
            ClientScript.RegisterClientScriptBlock(this.GetType(), "jsOpen", "openwindow(\"../public/DetailEdit.aspx?keyValues=" + i + "&Obj=vSigleTests&&col=6&isread=1\",600,400);");
        }
        else
        {
          g.Alert("没有相关联的单证信息，请确认是否录入单证信息！");
        }
      }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      if (iptPassNo.Text.Trim() == "")
      {
        g.Alert("入库单还没有录入！");
        return;
      }
      string xmlPath = Server.MapPath("../xml/") + iptPassNo.Text + "/";
      if (Directory.Exists(xmlPath))
        Directory.Delete(xmlPath, true);
      Directory.CreateDirectory(xmlPath);
      g.Alert("删除成功！");
    }

    protected void btnQZRK_Click(object sender, EventArgs e)
    {
      if (iptPassNo.Text.Trim() == "")
      {
        g.Alert("入库单还没有录入！");
        return;
      }
      int i = g.ExecSql(string.Format("exec pWMS_QZRK '{0}','{1}'", g.ToSql(iptPassNo.Text), g.ToSql(g.getSession("UID"))));
      if (i > 0)
      {
        g.Alert("强制入库成功！");
      }
      else
        g.Alert("强制入库失败，请确认该入库单是否被删除或者已经入库了！");
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
      if (iptPassNo.Text.Trim() == "")
      {
        g.Alert("入库单还没有录入！");
        return;
      }
      int i = g.ExecSql(string.Format("update inbound set islocked=0 where iptPassNo='{0}'", g.ToSql(iptPassNo.Text)));
      if (i > 0)
      {
        btnLock.Visible = false;
        btnBGD.Visible = btnSave.Visible = Button1.Visible = true;
        string bgd = g.getSValue(string.Format("select 1 from g_users where logname='{0}' and isnull(canqzrk,0)=1", g.ToSql(g.getSession("UID"))));
        g.Alert("解锁成功！");
      }
      else
        g.Alert("解锁失败，请重试！");
    }
  }
}
