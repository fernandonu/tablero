<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="beneficiarios.aspx.vb" Inherits="Renacer.beneficiarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="css/themes/flickr/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="js/ui/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <script src="js/beneficiarios.js" type="text/javascript"></script>
    <script src="js/FusionCharts.js" type="text/javascript"></script>
    <title>Tablero de Comando :: EPCSS Chaco :: Beneficiarios</title>
</head>
<body>
  <div id="maincontainer" class="ui-widget ui-widget-content ui-corner-all">
    <div id="mainmenu">
        <img src="css/img/logo.png" alt="Tablero de Comando" class="logo"/>
        <div id="menuv">
            <ul>
                <li><a href="#" onclick="gridReload(1, 'Por Efector')">Por Efector</a></li>
                <li><a href="#" onclick="gridReload(2, 'Por Categoría')">Por Categoría</a></li>
                <li><a href="#" onclick="gridReload(3, 'Por Zonas')">Por Zonas</a></li>
                <li><a href="#" onclick="gridReload(4, 'Por Edades')">Por Edades</a></li>
                <%--<li><a href="#" onclick="gridReload(4, 'Facturación')">Facturación</a></li>
                <li><a href="#" onclick="gridReload(5, 'No facturadas')">Prácticas no Facturadas</a></li>
                <li><a href="#" onclick="gridReload(6, 'Prácticas Habilitadas')">Prácticas Habilitadas</a></li>--%>
            </ul>
        </div>
        
        <div id="menus">
            <ul>
                <li><a href="indicadores.aspx">Indicadores Provincia</a></li>
                <li><a href="efector.aspx">Indicadores por Efector</a></li>
                <li><a href="nomenclador.aspx">Nomenclador</a></li>
                <li><a href="datosefector.aspx">Datos Efectores</a></li>
                <li><a href="facturacion.aspx">Control de Gestión</a></li>
                <li><a href="administrar.aspx">Administrar</a></li>
                <li><a href="reports.aspx">Reportes</a></li>
                <li><a href="monitor.aspx">Monitor</a></li>
            </ul>
        </div>
        
    </div>
    <div id="content">
        <p id="amb" class="maintitle">Por Efector</p>
        <p id="st" class="subtitle1">Beneficiarios Activos</p>
       
        <table id="treegrid"></table> 
        <div id="ptreegrid"></div>
    </div>
    <div id="options">
    <ul>
        <li><p id="Estimada" class="maintitle">Valores Estimados</p></li>
        <li><p id="P3" class="subtitle">Total</p>
        <asp:Label ID="TP" runat="server" Text="Label" cssclass="maintitle"></asp:Label></li>        
        <li><p id="P1" class="subtitle">Transferencia Capitada Mensual</p>
        <asp:Label ID="TCM" runat="server" Text="Label" cssclass="maintitle"></asp:Label></li>
        <li><p id="P2" class="subtitle">Cumplimiento de Trazadoras</p>
        <asp:Label ID="CT" runat="server" Text="Label" cssclass="maintitle"></asp:Label></li>
    </ul>
    <ul>
        <li><p id="Potencial" class="maintitle">Valores Potenciales</p></li>
        <li><p id="P6" class="subtitle">Total</p>
        <asp:Label ID="TPP" runat="server" Text="Label" cssclass="maintitle"></asp:Label></li>
        <li><p id="P4" class="subtitle">Transferencia Capitada Mensual</p>
        <asp:Label ID="TCMP" runat="server" Text="Label" cssclass="maintitle"></asp:Label></li>
        <li><p id="P5" class="subtitle">Cumplimiento de Trazadoras</p>
        <asp:Label ID="CTP" runat="server" Text="Label" cssclass="maintitle"></asp:Label></li>
    </ul>
</div>
        
<div id="dialog-info" title="Informaci&oacute;n">
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    <p id="dialog-info-msj"></p>
</div>

<%--<div id="dialog-ind" title="Informaci&oacute;n">
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    <span><b> Indicador: </b></span><span id="indicador"></span> <br/>
    <span><b>Código: </b></span><span  id="codigo"></span> <br/>
    <span><b>Meta 1: </b></span><span  id="meta1"></span> <br/>
    <span><b>Meta 2: </b></span><span  id="meta2"></span> <br/>
    <span><b>Meta 3: </b></span><span  id="meta3"></span> <br/>
    <span><b>Valor Actual: </b></span><span  id="valor"></span> <br/>
    <div id="Div1" align="center" class="chartarea1">
        <div id="chartdiv" align="center" class="hidetitle1">
             The chart will appear within this DIV. This text will be replaced by the chart.
        </div>
    </div>--%>
</div>
</body>
</html>
