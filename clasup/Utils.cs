using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Export
{
  public class SQLTemplate
  {
    private string _innerString = null;

    public static SQLTemplate New(params string[] s)
    {
      return new SQLTemplate(string.Join("", s));
    }

    public SQLTemplate(string s)
    {
      _innerString = s;
    }

    public string FormatSql(params object[] args)
    {
      var nonFlagString = _innerString;
      var parsedArgs = new object[args.Length];
      var quotationTypes = new List<string>() { "s" };

      var matches = Regex.Matches(_innerString, @"{(\d*)([s|d|n|i|b|w])}", RegexOptions.IgnoreCase);
      foreach (Match match in matches)
      {
        var index = int.Parse(match.Groups[1].ToString());
        var type = match.Groups[2].ToString().ToLower();
        switch (type)
        {
          #region Convert Type
          case "s":
            parsedArgs[index] = g.ToSql(args[index].ToString());
            break;
          case "d":
            parsedArgs[index] = g.ToDate(args[index].ToString());
            break;
          case "n":
            parsedArgs[index] = g.ToDecimal(args[index].ToString());
            break;
          case "i":
            parsedArgs[index] = g.ToDecimal(args[index].ToString());
            break;
          case "b":
            parsedArgs[index] = Convert.ToBoolean(args[index].ToString()) ? "1" : "0";
            break;
          case "w":
          default:
            parsedArgs[index] = args[index];
            break;
          #endregion
        }
        var holder = "{" + match.Groups[1].ToString() + "}";
        if (quotationTypes.Contains(type))
        {
          holder = "'" + holder + "'";
        }
        nonFlagString = nonFlagString.Replace(match.Groups[0].ToString(), holder);
      }
      return string.Format(nonFlagString, parsedArgs);
    }
  }

  public delegate TResult Func<T, TResult>(T obj);
  public delegate void Action();

  public class SuperviseStatus
  {
    public enum Status
    {
      None=0,
      Initial,
      WaitTransfer1,
      Declared,
      Submitted,
      Cleared,
      WaitTransfer2,
      Finished,
    }

    private Status _status = Status.None;
    private List<Status> flow = new List<Status>()
    {
      Status.Initial, Status.WaitTransfer1, Status.Declared, Status.Submitted,
      Status.Cleared, Status.WaitTransfer2, Status.Finished
    };

    public SuperviseStatus(Status flow)
    {
      _status = flow;
    }

    public SuperviseStatus Previous()
    {
      if (_status == Status.None) return this;
      var idx = flow.IndexOf(_status) - 1;
      try
      {
        return new SuperviseStatus(flow[idx]);
      }
      catch (IndexOutOfRangeException)
      {
        return this;
      }
    }

    public SuperviseStatus Next()
    {
      if (_status == Status.None) return this;
      var idx = flow.IndexOf(_status) + 1;
      try
      {
        return new SuperviseStatus(flow[idx]);
      }
      catch (IndexOutOfRangeException)
      {
        return this;
      }
    }

    public string GetDisplayName()
    {
      return Enum.GetName(typeof(Status), _status);
    }

    public int GetValue()
    {
      return flow.IndexOf(_status);
    }

    public string GetValueString()
    {
      return GetValue().ToString();
    }
  }
}