var cuie;
jQuery(document).ready(function() {
    
    //Cargar Grid Indicadores
    jQuery("#gridnom").jqGrid({
        url: 'dataservices/srvDatosEfector.aspx',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Zona', 'Localidad', 'CUIE', 'Efector', 'Direccion', 'Tipo'],
        colModel: [
            { name: 'Zona', index: 'Zona', width: 40, sortable: false },
            { name: 'Localidad', index: 'Localidad', width: 125, sortable: false },
            { name: 'CUIE', index: 'CUIE', width: 40, sortable: false },
            { name: 'Efector', index: 'Efector', width: 275, sortable: false },
            { name: 'Direccion', index: 'Direccion', width: 150, sortable: false },
            { name: 'Tipo', index: 'Tipo', width: 120, sortable: false, hidden: true}],
        //pager: jQuery('#pagernom'),
        rowNum: 500,
        rowList: [15, 30, 50],
        viewrecords: true,
        sortname: 'IndNom',
        height: 375,
        caption: 'Nomenclador',
        loadui: 'block'
    });

jQuery("#gridpract").jqGrid({
        url: 'dataservices/srvPracticasHabilitadas.aspx?cd_mask=0',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Codigo', 'Prestación'],
        colModel: [
            { name: 'Codigo', index: 'Codigo', width: 75, sortable: false },
            { name: 'Prestación', index: 'Prestación', width: 420, sortable: false }],
        //pager: jQuery('#ppract'),
        rowNum: 250,
        viewrecords: true,
        sortname: 'Codigo',
        height: 220,
        loadui: 'block'
    });
});

function gridReload() {
    var algo = jQuery("#txtBuscar").val();
    jQuery("#gridnom").setGridParam({ url: "dataservices/srvDatosEfector.aspx?cd_mask=" + algo + "&nm_mask=" + algo, page: 1 }).trigger("reloadGrid");
}

function gridPraReload() {
    var algo = jQuery("#txtBusPra").val();
    jQuery("#gridpract").setGridParam({ url: "dataservices/srvPracticasHabilitadas.aspx?cd_mask=" + cuie + "&nm_mask=" + algo, page: 1 }).trigger("reloadGrid");
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
    var pra_name = $("#pra-name"),
		pra_allFields = $([]).add(pra_name),
		pra_tips = $("#pra-validateTips");


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
    
    $("#dialog-pra").dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 540,
        resizable: true,
        buttons: {
            'Aceptar': function() {
                $(this).dialog('close');
            }
        },
        close: function() {
            pra_tips.removeClass('ui-state-highlight');
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
        var id = jQuery("#gridnom").getGridParam('selrow');
        if (id) {
            var ret = jQuery("#gridnom").getRowData(id);
            cuie = ret.CUIE;
            gridPraReload();
            $("#pra-efector").text(ret.Efector);
            $("#dialog-pra").dialog('open');
        } else {
            showinfo("Debe seleccionar un efector");
        };
    }).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });

});