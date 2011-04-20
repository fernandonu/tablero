<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="nomenclador.aspx.vb" Inherits="Renacer.nomenclador" %>

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
    <script src="js/nomenclador.js" type="text/javascript"></script>

    <title>Tablero de Comando :: EPCSS Chaco :: Nomenclador</title>
</head>
<body>
<div id="maincontainer" class="ui-widget ui-widget-content ui-corner-all">
    <div id="mainmenu">
        <img src="css/img/logo.png" alt="Tablero de Comando" class="logo"/>
        
        <div id="menuv">
            <ul>
                <li><a href="indicadores.aspx">Indicadores Provincia</a></li>
                <li><a href="efector.aspx">Indicadores por Efector</a></li>
                <li><a href="beneficiarios.aspx?tipo=1">Totalizadores</a></li>
                <li><a href="datosefector.aspx">Datos Efectores</a></li>
                <li><a href="facturacion.aspx">Control de Gestión</a></li>
                <li><a href="administrar.aspx">Administrar</a></li>
                <li><a href="reports.aspx">Reportes</a></li>
                <li><a href="monitor.aspx">Monitor</a></li>
            </ul>
        </div>
        
    </div>
    <div id="content">
        <p id="amb" class="maintitle">Nomenclador</p>
        <p class="subtitle1">Listado de Prácticas</p>
        <input id="idamb" type="hidden" />
        <div class="search">
            <label for="txtBuscar">Buscar:</label>
            <input id="txtBuscar" class="ui-widget-content ui-corner-all" type="text" name="txtBuscar" onkeyup="gridReload()"/>
        </div>
        <table id="gridnom"></table> 
        <div id="pagernom"></div>
    </div>
    <div id="options">
    </div>  
</div>
        
<div id="dialog-info" title="Informaci&oacute;n">
    <p><span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span></p>
    <p id="dialog-info-msj"></p>
</div>

<%--<div id="dialog-ind" title="Informaci&oacute;n">
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
</div>--%>

</body>
</html>
