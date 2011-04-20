var lastsel;
jQuery(document).ready(function() {

    var area = get_url_param('area');
    var nom = get_url_param('nom');

    //Cargar Grid Indicadores
    jQuery("#gridind").jqGrid({
        url: 'dataservices/srvPeriodo.aspx',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Codigo', 'Indicador', 'Período', 'Meta1', 'Meta2', 'Meta3', 'Area'],
        colModel: [
            { name: 'IndCod', index: 'IndCod', width: 50, sortable: false },
            { name: 'IndNom', index: 'IndNom', width: 288, sortable: false },
            { name: 'PerId', index: 'PerId', width: 75, sortable: false },
            { name: 'permeta1', index: 'permeta1', width: 75, sortable: false },
            { name: 'permeta2', index: 'permeta2', width: 75, sortable: false},
            { name: 'permeta3', index: 'permeta3', width: 75, sortable: false },
            { name: 'AreId', index: 'AreId', width: 100, sortable: false, hidden: true}],
        
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
                $("#nombre").text(ret.IndNom);
                $("#codigo").text(ret.IndCod);
                $("#meta1").text(parseFloat(ret.Meta1));
                $("#meta2").text(parseFloat(ret.Meta2));
                $("#meta3").text(parseFloat(ret.Meta3));
                //updateChart(parseFloat(ret.Valor), parseFloat(ret.Meta1), parseFloat(ret.Meta2), parseFloat(ret.Meta3));
                $("#dialog-ind").dialog('open');
            } else {
                showinfo("Debe seleccionar un indicador");
            };
        }
    });



    if (area != "") {
        gridReload(area, nom);
    }


});

function gridReload(algo, nombre) {
    //var traz = jQuery("#txtBuscar").val();
    jQuery("#gridind").setGridParam({ url: "dataservices/srvPeriodo.aspx?nm_mask=" + algo, page: 1 }).trigger("reloadGrid");
    $("#amb").text(nombre);
    $("#idamb").val(algo);
}

function gridReloadi() {
    var traz = jQuery("#txtBuscar").val();
    var algo = jQuery("#idamb").val();
    jQuery("#gridind").setGridParam({ url: "dataservices/srvPeriodo.aspx?nm_mask=" + traz, page: 1 }).trigger("reloadGrid");
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
		showej_tips = $("#showej-validateTips"),
		act_cod = $("#codigo-act"),
		act_m1 = $("#edititem-M1"),
        act_m2 = $("#edititem-M2"),
        act_m3 = $("#edititem-M3"),
		act_allFields = $([]).add(act_cod).add(act_m1).add(act_m2).add(act_m3),
		act_tips = $("#act-validateTips");


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

    $("#dialog-act").dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        buttons: {
            Ok: function() {
                var isValid = true;
                act_allFields.removeClass('ui-state-error');
                isValid = isValid && checkRegexp(act_m1, /^[0-9]{1,10}$/, "La meta 1 debe ser un número valido.", act_tips);
                isValid = isValid && checkRegexp(act_m2, /^[0-9]{1,10}$/, "La meta 2 debe ser un número valido.", act_tips);
                isValid = isValid && checkRegexp(act_m3, /^[0-9]{1,10}$/, "La meta 3 debe ser un número valido.", act_tips);

                if (isValid) {

                    $.ajax({
                        type: "POST",
                        url: "control/wsMetas.asmx/AgregarPeriodo",
                        data: "indcod=" + act_cod.text() + "&perid=" + "2010-1" + "&m1=" + act_m1.val() + "&m2=" + act_m2.val() + "&m3=" + act_m3.val(),
                        timeout: 1000,
                        async: false,
                        success: showinfo
                    });
                    gridReload("", "");
                    $(this).dialog('close');
                }

            },
            'Cancelar': function() {
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

    $('#actual').click(function() {
        var id = jQuery("#gridind").getGridParam('selrow');
        if (id) {
            var ret = jQuery("#gridind").getRowData(id);
            jQuery("#codigo-act").text(ret.IndCod);
            jQuery("#indicador-act").text(ret.IndNom);

            $("#dialog-act").dialog('open');
        } else {
            showinfo("Debe seleccionar un indicador");
        };
    }).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });

});