var idee = 0;
jQuery(document).ready(function() {

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

});

function gridEfeReload() {
    var algo = jQuery("#txtBusEfe").val();
    jQuery("#gridefec").setGridParam({ url: "dataservices/srvEfectores.aspx?nm_mask=" + algo, page: 1 }).trigger("reloadGrid");
}
 

$(function() {

    //definicion de lo controles
    //-------------------------------------------------------------------
    var searchefe_ano = $("#param-ano"),
		searchefe_allFields = $([]).add(searchefe_ano), 
        searchefe_tips = $("#searchefe-validateTips");
        
      function updateTips(o, t) {
        o.addClass('ui-state-highlight');
        o.text(t).effect("highlight", {}, 1500);
    }
    
    function checkLength(o, n, min, max, tip) {
        //chequea que el objeto (o) llamado n tenga entre min y max chars
        if (o.val().length > max || o.val().length < min) {
            o.addClass('ui-state-error');
            updateTips(tip, n + " debe tener entre " + max + " caracteres.");
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

   $("#dialog-param").dialog({
        bgiframe: true,
        autoOpen: false,
        width: 380,
        modal: true,
        zIndex: 10,
        buttons: {
            'Generar Reporte': function() {
                window.open("reports/crystal.aspx?rep=1&desde=" + $("#param-FecIni").val() + "&hasta=" + $("#param-FecFin").val(), "_blank");
                $(this).dialog('close');
            }
        },
        close: function() {
            $("#param-FecIni").val("");
            $("#param-FecFin").val("");
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
    
    $("#dialog-searchefe").dialog({
        bgiframe: true,
        autoOpen: false,
        modal: true,
        width: 540,
        resizable: true,
        buttons: {
             'Generar Reporte': function() {
                var isValid = true;
                searchefe_allFields.removeClass('ui-state-error');
                if (idee == 3){
                isValid = isValid && checkLength(searchefe_ano, "Año", 4, 4, searchefe_tips);
                isValid = isValid && checkRegexp(searchefe_ano, /^[0-9]{1,10}$/, "El año debe ser un número.", searchefe_tips);
                 };
                if (isValid) {
                    var id = $("#gridefec").getGridParam('selrow');
                    if (id) {
                        var ret = jQuery("#gridefec").getRowData(id);
                        window.open("reports/crystal.aspx?rep=" + idee + "&cuie=" + ret.CUIE + "&ano=" + $("#param-ano").val() + "&efec=" + ret.EfeNom, "_blank");
                        $(this).dialog('close');
                        } else {
                            updateTips(searchefe_tips, "Debe seleccionar un efector.");
                    };
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
    
    $("#dialog-mes").dialog({
        bgiframe: true,
        autoOpen: false,
        width: 380,
        modal: true,
        zIndex: 10,
        buttons: {
            'Generar Reporte': function() {
                window.open("reports/crystal.aspx?rep=" + idee + "&mes=" + $("#param-mes").val(), "_blank");
                $(this).dialog('close');
            }
        },
        close: function() {
            $("#param-mes").val("");
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


    // Funciones Varias
    //-----------------------------------------------------------------------------------------
    
    //definicion y control de ingreso de fecha: datepickers relacionados
//    $.datepicker.setDefaults({ showMonthAfterYear: false }, $.datepicker.regional['es']);
//    $("#param-FecIni").datepicker({ changeMonth: true, changeYear: true });
//    $("#param-FecFin").datepicker({ changeMonth: true, changeYear: true });

    $("#A1").click(function() {
        $("#dialog-param").dialog('open');
    });

//    $("#param-FecIni").change(function() {
//        var d1 = $("#param-FecIni").val();
//        var xMonth = parseInt(d1.substring(3, 5), 10) - 1;
//        var xDay = d1.substring(0, 2);
//        var xYear = d1.substring(6, 10);
//        var fecha = new Date(xYear, xMonth, xDay);
//        $("#param-FecFin").datepicker('option', 'minDate', fecha);
//    });

//    $("#param-FecFin").change(function() {
//        var d2 = $("#param-FecFin").val();
//        var yMonth = parseInt(d2.substring(3, 5), 10) - 1;
//        var yDay = d2.substring(0, 2);
//        var yYear = d2.substring(6, 10);
//        var fecha1 = new Date(yYear, yMonth, yDay);
//        $("#param-FecIni").datepicker('option', 'maxDate', fecha1);
//    });

    $('#A3').click(function() {
        idee = 3;
        $("#dialog-searchefe").dialog('open');
    });
    
    $('#A11').click(function() {
        idee = 11;
        $("#dialog-searchefe").dialog('open');
    });
    
    $('#A12').click(function() {
        idee = 12;
        $("#dialog-searchefe").dialog('open');
    });
    
    $("#A6").click(function() {
        idee = 13
        $("#dialog-mes").dialog('open');
    });

});