<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Renacer.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <div id="maincontainer">
    <div id="somizq"></div>
    <div id="somarr"></div>
        <img id="isotipo" alt="Logo" src="css/img/logo.png" />
        <form id="formlogin" runat="server">
            <p class="label">Nombre de Usuario</p>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="input"></asp:TextBox>
            <p class="label">Contrase&ntilde;a</p>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input"></asp:TextBox>
            <p><asp:Button ID="btnLogin" runat="server" Text="Iniciar" CssClass="button" /></p>
            <p class="errormsj"><asp:Label ID="lblerror" runat="server" Text=""></asp:Label></p>
            &nbsp;</form>
        <div id="mainmid">
        &nbsp;
        </div>
        <div id="maindown"></div>
    <div id="somaba"></div>
    <div id="somder"></div>
    <img id="iniciar" alt="ini" src="css/img/iniciar.png" /></div>
</body>
</html>
