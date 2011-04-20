<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="detalle.aspx.vb" Inherits="Renacer.detalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Tablero de Comando :: EPCSS Chaco :: Indicadores</title>
    <link href="css/themes/flickr/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script src="js/FusionCharts.js" type="text/javascript"></script>
    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="js/ui/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <script src="js/arbol.js" type="text/javascript"></script>
</head>
<body>
<div id="maincontainer" class="ui-widget ui-widget-content ui-corner-all">
    <div id="mainmenu">
        <img src="css/img/logo.png" alt="Tablero de Comando" class="logo"/>
        <div id="menuv">
            <ul>
                <li><a href="#" onclick="gridReload(0, 'Todas')">Todas</a></li>
                <li><a href="#" onclick="gridReload(1, 'Trazadoras')">Trazadoras</a></li>
                <li><a href="#" onclick="gridReload(4, 'Inscripciones')">Inscripciones</a></li>
                <li><a href="#" onclick="gridReload(8, 'Facturación')">Facturación</a></li>
                <li><a href="#" onclick="gridReload(2, 'Modelo de Atención')">Modelo de Atención</a></li>
                <li><a href="#" onclick="gridReload(3, 'Calidad de Atención')">Calidad de Atención</a></li>
                <li><a href="#" onclick="gridReload(5, 'Gestión de Padrones')">Gestión de Padrones</a></li>
                <li><a href="#" onclick="gridReload(6, 'Legales')">Legales</a></li>
                <li><a href="#" onclick="gridReload(7, 'Liquidez')">Liquidez</a></li>
            </ul>
        </div>
                       
        <div id="menus">
            <ul>
                <li><a href="indicadores.aspx">Indicadores Provincia</a></li>
                <li><a href="efector.aspx">Indicadores por Efector</a></li>
                <li><a href="beneficiarios.aspx?tipo=1">Totalizadores</a></li>
                <li><a href="nomenclador.aspx">Nomenclador</a></li>
                <li><a href="datosefector.aspx">Datos Efectores</a></li>
                <li><a href="facturacion.aspx">Control de Gestión</a></li>
                <li><a href="administrar.aspx">Administrar</a></li>
                <li><a href="reports.aspx">Reportes</a></li>
                <li><a href="monitor.aspx">Monitor</a></li>
            </ul>
        </div>
        
    </div>
    <div id="content" class="nooptions">
        
        <form id="form1" runat="server">
            <div class="tree">
                <p class="maintitle">Detalles</p>
                <asp:Label ID="lblInd" runat="server" Text="" CssClass="subtitle1"></asp:Label>
                <%--<p class="subtitle1">Subtitulo del Arbol. Este es un subtitulo grande con una descripcion larga.</p>--%>
                <table id="treegrid" cellpadding="0" cellspacing="0"></table> 
                <%--<div id="ptreegrid"></div>--%>
                <button id="vernodo" class="ui-state-default ui-corner-all" type="button">Ver Nodo</button>
            </div>
            <div class="chart">
                <asp:UpdatePanel ID="FusionChartsLP" runat="server">
                    <ContentTemplate>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <div class="chartarea">
                            <asp:Panel ID="Panel2" runat="server" CssClass="hidetitle">
                            </asp:Panel>
                        </div>
                        <div class="chartarea">
                            <asp:Panel ID="Panel1" runat="server" CssClass="hidetitle">
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="vergraf" runat="server" Text="Ver Gráfico" CssClass="ui-state-default ui-corner-all" />
                    </ContentTemplate>
                </asp:UpdatePanel>  
            </div>
            <asp:TextBox ID="cuie" runat="server"></asp:TextBox>
            <asp:TextBox ID="nombre" runat="server"></asp:TextBox>
            <asp:HiddenField ID="indcod" runat="server" />
            <asp:HiddenField ID="graid" runat="server" />
        </form> 
    </div>  
</div>
</body>
</html>
