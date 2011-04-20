<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="users.aspx.vb" Inherits="Renacer.users" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        Nombre de usuario:  <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
         <br />
        Contraseña: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
         <br />
        Nombre y Apellido:
        <asp:TextBox ID="txtNya" runat="server"></asp:TextBox>
        <br />
        Area:
        <asp:DropDownList ID="ddlAreas" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="AreNom" DataValueField="AreId">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:dbConnectionString %>" 
            SelectCommand="SELECT [AreId], [AreNom] FROM [Area]"></asp:SqlDataSource>
        <br />
    </div>
    <asp:Button ID="Button1" runat="server" Text="Agregar" />
    </form>
</body>
</html>
