using System;
using System.Web.SessionState;
using System.Web;

namespace Export.clasup
{
  public class PassSetStatus : IHttpHandler, IRequiresSessionState
  {
    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/plain";
      context.Response.Write("Skip");
      var passid = g.GetRequest("passid");
      if (passid == "") return;

      var statusName = g.GetRequest("stsname");
      if (statusName == "")
      {
        var passData = g.getTable(sqltPass.FormatSql(passid));
        if (passData.Rows.Count == 0) return;

        var record = passData.Rows[0];

        Predicate<string> isCompleted = x => record[x].ToString() == "1";
        for (int i = statusFlow.Length - 1; i >= 0; i--)
        {
          if (!isCompleted(statusFlow[i])) continue;
          if (i + 1 > statusFlow.Length - 1)
          {
            context.Response.Clear();
            context.Response.Write("Already completed all.");
            return;
          }
          statusName = statusFlow[i + 1];
          break;
        }
        if (statusName == "") statusName = statusFlow[0];
      }

      g.Exec(sqltUpdateStatus.FormatSql(statusName, passid));

      context.Response.Clear();
      context.Response.Write("Updated " + statusName + ".");
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }

    private static readonly SQLTemplate sqltPass = SQLTemplate.New(
      "SELECT [ISTRANSFEROUT], [ISDECLARED], [ISDECLARECOMMITTED], [ISPASSED], [ISTRANSFERIN], [ISFINISHED]",
      "  FROM CLASUP_PASS WHERE [ID] = {0S}"
    );
    private static readonly SQLTemplate sqltUpdateStatus = SQLTemplate.New("UPDATE CLASUP_PASS SET {0W} = 1 WHERE ID = {1S}");
    private static readonly string[] statusFlow = {
      "ISTRANSFEROUT", "ISDECLARED", "ISDECLARECOMMITTED", "ISPASSED", "ISTRANSFERIN", "ISFINISHED"
    };
  }
}