var lastsel;
jQuery(document).ready(function() {

    var pers = get_url_param('pers');
    var nom = get_url_param('nom');

    //Cargar Grid Indicadores
    jQuery("#gridind").jqGrid({
        url: 'dataservices/srvIndicadoresF.aspx?cd_mask=' + pers,
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Codigo', 'Indicador', 'Valor', 'Tendencia', 'Estado', 'Meta1', 'Meta2', 'Meta3'],
        colModel: [
            { name: 'IndCod', index: 'IndCod', width: 50, sortable: false },
            { name: 'IndNom', index: 'IndNom', width: 288, sortable: false },
            { name: 'Valor', index: 'Valor', width: 100, sortable: false },
            { name: 'Tendencia', index: 'Tendencia', width: 100, sortable: false, align: 'center' },
            { name: 'Estado', index: 'Estado', width: 100, sortable: false, align: 'center' },
            { name: 'Meta1', index: 'Meta1', width: 100, sortable: false, hidden: true },
            { name: 'Meta2', index: 'Meta2', width: 100, sortable: false, hidden: true },
            { name: 'Meta3', index: 'Meta3', width: 100, sortable: false, hidden: true}],
        //pager: jQuery('#pagerind'),
        rowNum: 50,
        rowList: [15, 30, 50],
        viewrecords: true,
        sortname: 'IndNom',
        height: 330,
        caption: 'Grilla de Indicadores',
        loadui: 'block',
        ondblClickRow: function() {
            var id = jQuery("#gridind").getGridParam('selrow');
            if (id) {
                var ret = jQuery("#gridind").getRowData(id);
                $("#indicador-a").text(ret.IndNom);
                $("#codigo").text(ret.IndCod);
                $("#valor").text(ret.Valor + "%");
                $("#meta1").text(parseFloat(ret.Meta1) + "%");
                $("#meta2").text(parseFloat(ret.Meta2) + "%");
                $("#meta3").text(parseFloat(ret.Meta3) + "%");
                updateChart(parseFloat(ret.Valor), parseFloat(ret.Meta1), parseFloat(ret.Meta2), parseFloat(ret.Meta3));
                $("#dialog-ind").dialog('open');
            } else {
                showinfo("Debe seleccionar un indicador");
            };
        }
    });


    jQuery("#gridefec").jqGrid({
        url: 'dataservices/srvEfectores.aspx',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['CUIE', 'Efector', 'Localidad', 'Zona'],
        colModel: [
            { name: 'CUIE', index: 'CUIE', width: 50, sortable: false },
            { name: 'EfeNom', index: 'EfeNom', width: 300, sortable: false },
            { name: 'LocNom', index: 'LocNom', width: 150, sortable: false },
            { name: 'ZonNom', index: 'ZonNom', width: 100, sortable: false, hidden: true }],
        //pager: jQuery('#pefec'),
        rowNum: 10,
        viewrecords: true,
        sortname: 'EfeNom',
        height: 220,
        loadui: 'block'
    });


    if (pers != "") {
        gridReload(pers, nom);
    }


});

function gridReload(algo, nombre) {
    var cuie = jQuery("#cuiehidden").val();
    jQuery("#gridind").setGridParam({ url: "dataservices/srvIndicadoresF.aspx?cd_mask=" + algo + "&cuie=" + cuie, page: 1 }).trigger("reloadGrid");
    $("#amb").text(nombre);
    $("#idamb").val(algo);
}

function gridReloadi() {
    var traz = jQuery("#txtBuscar").val();
    var algo = jQuery("#idamb").val();
    var cuie = jQuery("#cuiehidden").val();
    jQuery("#gridind").setGridParam({ url: "dataservices/srvIndicadoresF.aspx?cd_mask=" + algo + "&nm_mask=" + traz + "&cuie=" + cuie, page: 1 }).trigger("reloadGrid");
}

function gridReloade(algo) {
    jQuery("#gridind").setGridParam({ url: "dataservices/srvIndicadoresF.aspx?cuie=" + algo, page: 1 }).trigger("reloadGrid");
    $("#cuiehidden").val(algo);
}

function gridEfeReload() {
    var algo = jQuery("#txtBusEfe").val();
    jQuery("#gridefec").setGridParam({ url: "dataservices/srvEfectores.aspx?nm_mask=" + algo, page: 1 }).trigger("reloadGrid");
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
    var myChart = new FusionCharts("FusionWidgets/Charts/HLinearGauge.swf", "myChartId", "275", "75", "0", "100");
    var ur = "dataservices/srvTermometro.aspx?nro=" + valor + "%26ma=" + meta1 + "%26mb=" + meta2 + "%26mc=" + meta3;
    myChart.setDataURL(ur);
    myChart.render("chartdiv");
}

$(function() {
    //definicion de lo controles
    //-------------------------------------------------------------------
    var showej_name = $("#showej-name"),
        showej_id = $("#showej-id"),
		showej_allFields = $([]).add(showej_name).add(showej_id),
		showej_tips = $("#showej-validateTips"),
        searchefe_tips = $("#searchefe-validateTips");


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

    $("#dialog-searchefe").dialog({
        bgiframe: true,
        autoOpen: true,
        modal: true,
        width: 540,
        resizable: true,
        buttons: {
            'Aceptar': function() {
                var id = $("#gridefec").getGridParam('selrow');
                if (id) {
                    var ret = jQuery("#gridefec").getRowData(id);
                    $("#efector").text(ret.EfeNom);
                    gridReloade(ret.CUIE);
                    $(this).dialog('close');
                } else {
                    updateTips(searchefe_tips, "Debe seleccionar un efector.");
                };
            },
            'Cancelar': function() {
                $(this).dialog('close');
            }
        },
        close: function() {
            searchefe_tips.removeClass('ui-state-highlight');
            searchefe_tips.text("Todos los campos son requeridos");
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
            var cuie = jQuery("#cuiehidden").val();
            window.location = "detalle.aspx?ind=" + ind + "&gra=" + gra + "&indnom=" + nom + "&cuie=" + cuie;
        } else {
            showinfo("Debe seleccionar un indicador");
        };
    }).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });


    $('#busefe').click(function() {
        $("#dialog-searchefe").dialog('open');
    }
    ).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });

});