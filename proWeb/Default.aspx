<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


 <h1 style="margin-bottom: 30px;">Products management</h1>
    
    <table style="border-collapse: separate; border-spacing: 0 15px; margin-bottom: 30px; font-family: 'Times New Roman', Times, serif; font-size: 16px;">
        <tr>
            <td style="width: 120px;">Code</td>
            <td><asp:TextBox ID="txtCode" runat="server" Width="250px" MaxLength="16"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Name</td>
            <td><asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="32"></asp:TextBox></td>
        </tr>  
        <tr>
            <td>Amount</td>
            <td><asp:TextBox ID="txtAmount" runat="server" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" Width="150px">
                    <asp:ListItem Value="0" Text="Computing"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Telephony"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Gaming"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Home appliances"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Price</td>
            <td><asp:TextBox ID="txtPrice" runat="server" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Creation Date</td>
            <td><asp:TextBox ID="txtCreationDate" runat="server" Width="250px"></asp:TextBox></td>
        </tr>
    </table>

    <div style="margin-bottom: 30px;">
        <asp:Button ID="btnCreate" runat="server" Text="Create" Width="80px" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" style="margin-left: 5px;" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" style="margin-left: 5px;" />
        <asp:Button ID="btnRead" runat="server" Text="Read" Width="80px" style="margin-left: 5px;" />
        <asp:Button ID="btnReadFirst" runat="server" Text="Read First" Width="90px" style="margin-left: 5px;" />
        <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" Width="90px" style="margin-left: 5px;" />
        <asp:Button ID="btnReadNext" runat="server" Text="Read Next" Width="90px" style="margin-left: 5px;" />
    </div>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>

</asp:Content>