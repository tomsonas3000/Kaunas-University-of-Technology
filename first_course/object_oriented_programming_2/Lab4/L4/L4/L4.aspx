<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L4.aspx.cs"
    Inherits="L4.L4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            width: 1318px;
        }
        .S1{
            animation-name: animacija;
            background-color: chocolate;
            animation-duration: 5s;
            animation-iteration-count:infinite;
            font-size : 24px;
        }
        .S2{
            background-color: aquamarine;
            border-color : black;
            border-style : dashed;
            font-size : 18px;
            float:left;
            margin-left: 5px;
        }
        .S3{
            background-color : lavender;
            border-color : black;
            border-style : dashed;
            border-width : 1px;
            font-size : 24px;
            text-align:center
        }
        @keyframes animacija{
            0%   {background-color: aliceblue;}
            25%  {background-color: lightskyblue;}
            50%  {background-color: skyblue;}
            100% {background-color: royalblue;}
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                ControlToValidate="TextBox1" MaximumValue="9999999999999999999
                9999999999999999999999999999999999999999999999999999999999999
                99999999999999999999999999999999999999999999999999999"
                MinimumValue="0.1">Reikia įvesti teigiamą skaičių

            </asp:RangeValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
        <p>
            <asp:TextBox ID="TextBox5" runat="server" Width="500px" 
                CssClass ="S3" >Įveskite užsakymo sumą</asp:TextBox>
            <asp:TextBox ID="TextBox1" runat="server" Width="500px"
                CssClass="S3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                runat="server" ControlToValidate="TextBox1" 
                ErrorMessage="Įvesti teigiamą reikšmę">*

            </asp:RequiredFieldValidator>
        
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" 
            Text="Parodyti pradinius duomenis ir rezultatus" Width="1008px"
            CssClass ="S1" />
        </p>
        <asp:Table ID="Table1" runat="server" Caption="Pradiniai Duomenys" 
            GridLines="Both" Height="541px" Width="499px" CssClass="S2">
        </asp:Table>
        &nbsp;<asp:Table ID="Table3" runat="server" Caption="Rezultatai" 
            Height="543px" Width="500px" GridLines="Both" CssClass="S2" 
            HorizontalAlign="Justify">
        </asp:Table>
        <asp:TextBox ID="TextBox4" runat="server" Width="1003px" 
            CssClass="S3" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
&nbsp;&nbsp;<p>
            <asp:TextBox ID="TextBox2" runat="server" Width="500px" 
                CssClass="S3" ></asp:TextBox>
        
            <asp:TextBox ID="TextBox3" runat="server" Visible="False"
                Width="500px" CssClass="S3"></asp:TextBox>
        </p>
    </form>
</body>
</html>
