<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L5.aspx.cs"
    Inherits="Lab5.L5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .S1 {
            animation-name: animacija;
            background-color: chocolate;
            animation-duration: 5s;
            animation-iteration-count: infinite;
            font-size: 24px;
        }

        .S2 {
            background-color: aquamarine;
            border-color: black;
            border-style: dashed;
            font-size: 18px;
            float: left;
            margin-left: 5px;
        }

        .S3 {
            background-color: lavender;
            border-color: black;
            border-style: dashed;
            border-width: 1px;
            font-size: 24px;
            text-align: center
        }

        @keyframes animacija {
            0% {
                background-color: aliceblue;
            }

            25% {
                background-color: lightskyblue;
            }

            50% {
                background-color: skyblue;
            }

            100% {
                background-color: royalblue;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                Text="Initial data" CssClass="S1" Width="223px" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click"
                Text="Results" CssClass="S1" Width="225px" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click"
                Text="Program kaput" CssClass="S1" Width="260px" />
        </p>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" 
                CssClass="S3"></asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server"
                CssClass="S3" OnTextChanged="TextBox5_TextChanged">Enter price</asp:TextBox>
        </p>
        <p>
            <asp:Table ID="Table1" runat="server" Caption="InitialData"
                GridLines="Both" CssClass="S2" Height="51px" Width="402px">
            </asp:Table>
            <asp:Table ID="Table2" runat="server" Caption="Results"
                GridLines="Both" CssClass="S2" Height="51px" Width="297px" HorizontalAlign="Justify">
            </asp:Table>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="S3" 
                Width="449px" ></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="S3"
                Width="451px"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="TextBox4" runat="server" CssClass="S3"
                Width="900px"></asp:TextBox>
        </p>
    </form>
</body>
</html>
