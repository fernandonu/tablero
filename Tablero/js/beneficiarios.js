jQuery(document).ready(function() {

    var pers = get_url_param('tipo');
    var nom = get_url_param('nom');
    
    jQuery("#treegrid").jqGrid({
        treeGrid: true,
        treeGridModel: 'adjacency',
        ExpandColumn: 'Nombre',
        url: 'dataservices/srvBeneficiarios.aspx?tipo=' + pers,
        datatype: "xml",
        mtype: "POST",
        colNames: ["id", "Nombre", "Total", "Potencial"],
        colModel: [
        { name: 'Id', index: 'Id', width: 100, hidden: true, key: true },
        { name: 'Nombre', index: 'Nombre', width: 400, align: "left"},
        { name: 'Total', index: 'Total', width: 110, sortable: false, align: 'center' },
        { name: 'Potencial', index: 'Potencial', width: 110, sortable: false, align: 'center' }],
        height: '400',
        pager: "#ptreegrid",
        caption: "Arbol de Beneficiarios",
        rowNum: 200
    });
    
      if (pers != "") {
        gridReloadLabel(pers, nom);
    }
    
});


function gridReload(algo, nombre) {
    window.location = "beneficiarios.aspx?tipo=" + algo + "&nom=" + nombre
}

function gridReloadLabel(algo, nombre) {
    if (!nombre) {
    nombre = "Por Efector";
    }
    $("#amb").text(nombre);
}

function get_url_param(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null) return "";
    else return results[1];
}

$(function() {
    //definicion de lo controles
    //-------------------------------------------------------------------
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
    
        function showinfo(msj) {
        $("#dialog-info-msj").text(msj);
        $("#dialog-info").dialog('open');
    }

    //Acciones de los botones

    //Mostrar la informacion de la linea seleccionada cuando se haga click en este boton
    $('#vernodo').click(function() {
        var id = jQuery("#treegrid").getGridParam('selrow');
        if (id) {
            var ret = jQuery("#treegrid").getRowData(id);
            alert(ret.Nombre);
        } else {
            alert("Debe seleccionar una zona, localidad o efector");
        };
    }
    ).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });



    // Funciones Varias
    //-----------------------------------------------------------------------------------------



});
