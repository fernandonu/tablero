<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reports.aspx.vb" Inherits="Renacer.reports" %>

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
    <script src="js/reportes.js" type="text/javascript"></script>
    <script src="js/FusionCharts.js" type="text/javascript"></script>
    <title>Tablero de Comando :: EPCSS Chaco :: Reportes</title>
</head>
<body>
<div id="maincontainer" class="ui-widget ui-widget-content ui-corner-all">
    <div id="mainmenu">
        <img src="css/img/logo.png" alt="Tablero de Comando" class="logo"/>
        <div id="menus">
            <ul>
                <li><a href="indicadores.aspx">Indicadores Provincia</a></li>
                <li><a href="efector.aspx">Indicadores por Efector</a></li>
                <li><a href="beneficiarios.aspx?tipo=1">Totalizadores</a></li>
                <li><a href="nomenclador.aspx">Nomenclador</a></li>
                <li><a href="datosefector.aspx">Datos Efectores</a></li>
                <li><a href="facturacion.aspx">Control de Gestión</a></li>
                <li><a href="administrar.aspx">Administrar</a></li>
                <li><a href="monitor.aspx">Monitor</a></li>
            </ul>
        </div>
    </div>
    <div id="content">
        <div id="reportes">
            <p class="title">Lista de reportes disponibles:</p>
            <ul>
            <%--<li><a href="#" id="rpt-extracciones">Extracciones</a></li>--%>
            <%--<li><a href="reports/crystal.aspx?rep=1" target="_blank" id="A1">Embarazadas por edades</a></li>--%>
            <li><a href="reports/crystal.aspx?rep=0" target="_blank" id="A10">Ranking</a></li>
            <li><a href="reports/crystal.aspx?rep=14" target="_blank" id="A13">Ranking por Zonas</a></li>
            <li><a href="#" id="A1">Embarazadas por edades</a></li>
            <li><a href="reports/crystal.aspx?rep=2" target="_blank" id="A2">Detalle de Cargas de Ipos</a></li>
            <li><a href="#" id="A3">Facturación por Efector por año</a></li>
            <li><a href="reports/crystal.aspx?rep=4" target="_blank" id="A4">Historial de Inscripciones</a></li>
            <li><a href="reports/crystal.aspx?rep=5" target="_blank" id="A5">Historial de Controles</a></li>
            <li><a href="#" id="A11">Indicadores por Efector</a></li>
            <li><a href="#" id="A12">Practicas Habilitadas por Efector</a></li>
            </ul>
            <p class="subtitle">Informes:</p>
            <ul>
            <li><a href="#" id="A6">Padrón de Activos</a></li>
            <li><a href="#" target="_blank" id="A7">Padrón de Rechazados</a></li>
            <li><a href="#" target="_blank" id="A8"></a></li>
            <li><a href="#" target="_blank" id="A9"></a></li>
            </ul>
        </div>
    </div>
    <div id="options">
    </div>  
</div>

<div id="dialog-param" title="Ingresar Parametros">
    <div id="param-validateTips">Todos los campos son requeridos.</div>
    <fieldset>
        <p><b>Edad mínima:</b></p>
        <input name="param-FecIni" id="param-FecIni" type="text" class="ui-widget-content ui-corner-all"/>
        <p><b>Edad máxima:</b></p>
        <input name="param-FecFin" id="param-FecFin" type="text" class="ui-widget-content ui-corner-all"/>      
    </fieldset>
</div>

<div id="dialog-searchefe" title="Buscar Efector">
    <div id="searchefe-validateTips">Todos los campos son requeridos.</div>
    <input id="idee" type="hidden" />
    <fieldset>
    <div>
        <div>
            <label for="txtBusEfe">Buscar Efector:</label>
            <input id="txtBusEfe" type="text" name="txtBusEfe" onkeyup="gridEfeReload()"/>
        </div>
        <table id="gridefec"></table>
        <div id="pefec"></div>
    </div>
    </fieldset>
    <p><b>Año:</b></p>
    <input name="param-ano" id="param-ano" type="text" class="ui-widget-content ui-corner-all"/>
</div>

<div id="dialog-mes" title="Ingresar Parametros">
    <div id="mes-validateTips">Todos los campos son requeridos.</div>
    <fieldset>
        <p><b>Mes y Año:</b></p>
        <input name="param-mes" id="param-mes" type="text" class="ui-widget-content ui-corner-all"/>
        <p>Ej: Enero de 2010</p>
    </fieldset>
</div>


</body>
</html>
