<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="facturacion.aspx.vb" Inherits="Renacer.facturacion1" %>

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
    <script src="js/facturacion.js" type="text/javascript"></script>
    <script src="js/FusionCharts.js" type="text/javascript"></script>
    <title>Tablero de Comando :: EPCSS Chaco :: Control de Gestión</title>
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
                <li><a href="monitor.aspx">Monitor</a></li>
                <li><a href="administrar.aspx">Administrar</a></li>
                <li><a href="reports.aspx">Reportes</a></li>
            </ul>
        </div>
        
    </div>
    <div id="content">
        <p id="amb" class="maintitle">Control de Facturación</p>
         
<div id="accordion">
	<h3><a href="#">Expedientes</a></h3>
	<div>
        <input id="txtexp" type="text" class="ui-widget-content ui-corner-all"/>
        <input id="btnexp" type="button" class="button first ui-state-default ui-corner-all" value="Actualizar" />
		<div class="medios">
		    <table id="gridexp"></table> 
            <div id="pagerexp"></div>
		</div>
        <div class="medios2">
            <table id="gridefe"></table> 
            <div id="pagerefe"></div>
        </div> 
	</div>
	<h3><a href="#">Rechazos</a></h3>
	<div>
	    <input id="txtrech" type="text" class="ui-widget-content ui-corner-all"/>
        <input id="btnrech" type="button" class="button first ui-state-default ui-corner-all" value="Actualizar" />

	    <div class="medios">
	        <table id="gridrech"></table> 
            <div id="pagerrech"></div>
        </div>
        <div class="medios2">
            <table id="grideferech"></table> 
            <div id="pagereferech"></div>
        </div>    
	</div>
	
	<h3><a href="#">Estado de Carga</a></h3>
	<div>
	    <div class="medios">
		    <table id="gridec"></table> 
		</div>
        <div class="medios2">
            <img src="css/img/ajax-loader.gif" alt="Tablero de Comando" class="temporizador"/>
            <div>
                <input id="contador" type="text" class="label" />
            </div>
        </div>
        
	</div>
</div>
        
        
        
    </div>
    <div id="options">
    </div>  
</div>
</body>
</html>
