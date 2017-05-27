<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomCountList.aspx.cs"
    Inherits="Export.Clasup.CustomCountList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            text-align: center;
        }
        table {
            margin: 0 auto;
            border-collapse: collapse;
            border-spacing: 10px;
            width: 100%;
        }
        th {
            text-align: center;
        }
        thead {
            border: 1px solid #ccc;
            border-top-width: 2px;
        }
        td {
            padding: 0 20px;
            text-align: left;
        }
        tr:nth-child(3n+0) td {
            padding-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }
        td:first-child {
            padding-left: 3px;
            border-left: 1px solid #ccc;
        }
        td:last-child {
            padding-right: 3px;
            border-right: 1px solid #ccc;
        }
        ul {
            margin: 20px auto;
            list-style: none;
        }
        li {
            display: inline-block;
            margin: 0 5px;
        }
        li span  {
            display: block;
            padding: 3px 8px;
            border: 1px solid #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="passList" runat="server" />
        </div>
    </form>
</body>
</html>
