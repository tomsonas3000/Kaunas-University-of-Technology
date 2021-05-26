<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lab2.aspx.cs" Inherits="LD_5.Lab2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .S1{
            background-color:#FFFF99;
            border-color:blue;
            border-style:dotted;
            font-size:24px;
            height:58px;
            width:359px;
        }
        .S2{
            background-color:#FFFF99;
            border-color:#0000CC;
            border-style:dotted;
            font-size:24px;
            height:59px;
            width:537px;
        }
        .S3{
            background-color:#99FF99;
            border-color:#660066;
            border-style:dashed;
            font-size:24px;
            height:42px;
            width:528px;
        }
        .S4{
            background-color:#FF5050;
            border-style:groove;
            font-size:24px;
            height:29px;
            width:528px;
        }
        .S5{
            background-color:#FFFF99;
            border-color:#660066;
            border-width:10px;
            font-size:20px;
            height:446px;
            width:549px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Button ID="Button1" runat="server" CssClass="S2" OnClick="Button1_Click" Text="Parodyti pradinius duomenis ir rezultatus"/>
            <asp:Button ID="Button2" runat="server" CssClass="S1" OnClick="Button2_Click" Text="Pašalinti trikampį"/>
            <br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" CssClass="S3">Įveskite trikampio koordinates, kurį norite šalinti</asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TextBox2" runat="server" CssClass="S4" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
            <br />
            <br />
            <br />
        <asp:Table ID="Table3" runat="server" CssClass="S5" Caption="Pradiniai duomenys" Font-Names="Arial Black" GridLines="Both" HorizontalAlign="Left" Visible="False"  >
        </asp:Table>
        <asp:Table ID="Table1" runat="server" CssClass="S5" Caption="Rezultatai" Font-Names="Arial Black" GridLines="Both" HorizontalAlign="Left" Visible="False">
        </asp:Table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
