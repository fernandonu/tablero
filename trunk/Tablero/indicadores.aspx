<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="indicadores.aspx.vb" Inherits="Renacer.indicadores" %>

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
    <script src="js/indicadores.js" type="text/javascript"></script>
    <script src="js/FusionCharts.js" type="text/javascript"></script>
    <title>Tablero de Comando :: EPCSS Chaco :: Indicadores</title>
    
    <script type="text/javascript">
            function updateChart(valor, meta1, meta2, meta3) {
                var myChart = new FusionCharts("FusionWidgets/Charts/HLinearGauge.swf", "myChartId", "275", "75", "0", "100");
                var ur = "dataservices/srvTermometro.aspx?nro=" + valor + "%26ma=" + meta1 + "%26mb=" + meta2 + "%26mc=" + meta3;
                myChart.setDataURL(ur);
                myChart.render("chartdiv");
            }
    </script>
   
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
        <button id="ver" class="button first ui-state-default ui-corner-all" type="button">Ver Detalles</button>
    </div>  
</div>
        
<div id="dialog-info" title="Informaci&oacute;n">
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    <p id="dialog-info-msj"></p>
</div>

<div id="dialog-ind" title="Informaci&oacute;n">
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    <span><b>Indicador: </b></span><span id="indicador-a"></span> <br/>
    <span><b>Código: </b></span><span  id="codigo"></span> <br/>
    <span><b>Meta 1: </b></span><span  id="meta1"></span> <br/>
    <span><b>Meta 2: </b></span><span  id="meta2"></span> <br/>
    <span><b>Meta 3: </b></span><span  id="meta3"></span> <br/>
    <span><b>Valor Actual: </b></span><span  id="valor"></span> <br/>
    <div id="Div1" align="center" class="chartarea1">
        <div id="chartdiv" align="center" class="hidetitle1">
             The chart will appear within this DIV. This text will be replaced by the chart.
        </div>
    </div>
</div>

</body>
</html>
