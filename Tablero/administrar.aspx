<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="administrar.aspx.vb" Inherits="Renacer.administrar" %>

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
    <script src="js/administracion.js" type="text/javascript"></script>
    <script src="js/FusionCharts.js" type="text/javascript"></script>
    <title>Tablero de Comando :: EPCSS Chaco :: Administrar</title>
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
                <li><a href="nomenclador.aspx">Nomenclador</a></li>
                <li><a href="datosefector.aspx">Datos Efectores</a></li>
                <li><a href="facturacion.aspx">Control de Gestión</a></li>
                <li><a href="reports.aspx">Reportes</a></li>
                <li><a href="monitor.aspx">Monitor</a></li>
            </ul>
        </div>
        
    </div>
    <div id="content">
        <p id="amb" class="maintitle">Todos</p>
        <p class="subtitle1">Listado de Indicadores.</p>
        <input id="idamb" type="hidden" />
        <div class="search">
            <label for="txtBuscar">Buscar:</label>
            <input id="txtBuscar" class="ui-widget-content ui-corner-all" type="text" name="txtBuscar" onkeyup="gridReloadi()"/>
        </div>
        <table id="gridind"></table> 
        <div id="pagerind"></div>
    </div>
    <div id="options">
        <button id="actual" class="button first ui-state-default ui-corner-all" type="button">Administrar</button>
        <%--
        <asp:Button ID="actDatos" runat="server" Text="Actualizar" CssClass="button first ui-state-default ui-corner-all" />
        --%>    
    </div>  
</div>
        
<div id="dialog-info" title="Informaci&oacute;n">
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    <p id="dialog-info-msj"></p>
</div>

<div id="dialog-ind" title="Informaci&oacute;n">
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    <span><b> Indicador: </b></span><span id="nombre"></span> <br/>
    <span><b>Código: </b></span><span  id="codigo"></span> <br/>
    <span><b>Meta 1: </b></span><span  id="meta1"></span> <br/>
    <span><b>Meta 2: </b></span><span  id="meta2"></span> <br/>
    <span><b>Meta 3: </b></span><span  id="meta3"></span> <br/>
    <span><b>Valor: </b></span><span  id="valor"></span> <br/>
     <div id="Div1" align="center" class="chartarea1">
    <div id="chartdiv" align="center" class="hidetitle1">
         The chart will appear within this DIV. This text will be replaced by the chart.
    </div>
    </div>
</div>

<div id="dialog-act" title="Actualizar">
    <div id="act-validateTips">Todos los campos son requeridos.</div>
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    
    <span><b> Indicador: </b></span><span id="indicador-act"></span> <br/>
    <span><b>Código: </b></span><span  id="codigo-act"></span> <br/>
   
        
    <label for="edititem-M1"><b>Meta 1:</b></label>
	<input name="edititem-M1" id="edititem-M1" type="text" class="mediumtext ui-widget-content ui-corner-all"/><br/>
	
    <label for="edititem-M2"><b>Meta 2:</b></label>
	<input name="edititem-M2" id="edititem-M2" type="text" class="mediumtext ui-widget-content ui-corner-all"/><br/>
	
    <label for="edititem-M3"><b>Meta 3:</b></label>
	<input name="edititem-M3" id="edititem-M3" type="text" class="mediumtext ui-widget-content ui-corner-all"/><br/>
	
</div>

</body>
</html>
