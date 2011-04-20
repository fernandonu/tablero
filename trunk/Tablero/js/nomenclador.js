var lastsel;
jQuery(document).ready(function() {

    var pers = get_url_param('pers');
    var nom = get_url_param('nom');

    //Cargar Grid Indicadores
    jQuery("#gridnom").jqGrid({
        url: 'dataservices/srvNomenclador.aspx',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Codigo', 'Prestacion', 'Precio'],
        colModel: [
            { name: 'Codigo', index: 'Codigo', width: 55, sortable: false, align: 'center' },
            { name: 'Prestacion', index: 'Prestacion', width: 475, sortable: false },
            { name: 'Precio', index: 'Precio', width: 90, sortable: false, align: 'center'}],
        //pager: jQuery('#pagernom'),
        rowNum: 250,
        rowList: [15, 30, 50],
        viewrecords: true,
        sortname: 'IndNom',
        height: 375,
        caption: 'Nomenclador',
        loadui: 'block'
    });

});

function gridReload() {
    var algo = jQuery("#txtBuscar").val();
    jQuery("#gridnom").setGridParam({ url: "dataservices/srvNomenclador.aspx?cd_mask=" + algo + "&nm_mask=" + algo, page: 1 }).trigger("reloadGrid");
}


function get_url_param(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null) return "";
    else return results[1];
}

function updateChart(valor, meta1, meta2, meta3) {
                var myChart = new FusionCharts("FusionWidgets/Charts/HLinearGauge.swf", "myChartId", "200", "75", "0", "0");
                myChart.setDataURL("dataservices/srvTermometro.aspx?nro=" + valor + "&m1=" + meta1 + "&m2=" + meta2 + "&m3=" + meta3);
                myChart.render("chartdiv");
            }
            
$(function() {
    //definicion de lo controles
    //-------------------------------------------------------------------
    var showej_name = $("#showej-name"),
        showej_id = $("#showej-id"),
		showej_allFields = $([]).add(showej_name).add(showej_id),
		showej_tips = $("#showej-validateTips");


    //validacion de los formularios
    //----------------------------------------------------------------------------
    function updateTips(o, t) {
        o.addClass('ui-state-highlight');
        o.text(t).effect("highlight", {}, 1500);
    }

    function checkLength(o, n, min, max, tip) {
        //chequea que el objeto (o) llamado n tenga entre min y max chars
        if (o.val().length > max || o.val().length < min) {
            o.addClass('ui-state-error');
            updateTips(tip, n + " debe tener entre " + min + " y " + max + " caracteres.");
            return false;
        } else {
            return true;
        }
    }

    function checkRegexp(o, regexp, n, tip) {
        if (!(regexp.test(o.val()))) {
            o.addClass('ui-state-error');
            updateTips(tip, n);
            return false;
        } else {
            return true;
        }
    }

    //Definicion de los dialog
    //----------------------------------------------------------------------------
    $("#dialog-showej").dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        buttons: {
            Ok: function() {
                $(this).dialog('close');
            }
        }
    });

    $("#dialog-info").dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        buttons: {
            Ok: function() {
                $(this).dialog('close');
            }
        }
    });

    $("#dialog-ind").dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        buttons: {
            Ok: function() {
                $(this).dialog('close');
            }
        }
    });
    
    function showinfo(msj) {
        $("#dialog-info-msj").text(msj);
        $("#dialog-info").dialog('open');
    }

    function showconfirm(msj) {
        $("#dialog-info-msj").text(msj);
        $("#dialog-info").dialog('open');
        gridReload();
    }

    function showerror() {
        $("#dialog-info-msj").text("La operación no se pudo completar correctamente.");
        $("#dialog-info").dialog('open');
    }

    //Acciones de los botones
    //--------------------------------------------------------------------------------------
    //Mostrar la informacion de la linea seleccionada cuando se haga click en este boton
    $('#ver').click(function() {
        var id = jQuery("#gridind").getGridParam('selrow');
        if (id) {
            var ret = jQuery("#gridind").getRowData(id);
            var ind = ret.IndCod;
            var gra = "1";
            var nom = ret.IndNom;
            window.location = "detalle.aspx?ind=" + ind + "&gra=" + gra + "&indnom=" + nom;
        } else {
            showinfo("Debe seleccionar un indicador");
        };
    }).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });

});