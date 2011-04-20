jQuery(document).ready(function() {

    jQuery("#treegrid").jqGrid({
        treeGrid: true,
        treeGridModel: 'adjacency',
        ExpandColumn: 'Nombre',
        url: 'dataservices/srvArbol.aspx?indcod=' + $("#indcod").val(),
        datatype: "xml",
        mtype: "POST",
        colNames: ["id", "Nombre", " ", " "],
        colModel: [
        { name: 'Id', index: 'Id', width: 100, hidden: true, key: true },
        { name: 'Nombre', index: 'Nombre', width: 232, align: "left"},
        { name: 'Tendencia', index: 'Tendencia', width: 30, sortable: false, align: 'center' },
        { name: 'Estado', index: 'Estado', width: 30, sortable: false, align: 'center' }],
        height: '400',
        pager: "#ptreegrid",
        caption: "Arbol de Efectores",
        rowNum: 200,
        onSelectRow: function(id) {
            var id = jQuery("#treegrid").getGridParam('selrow');
            if (id) {
                var ret = jQuery("#treegrid").getRowData(id);
                $("#cuie").val(ret.Id);
                $("#nombre").val(ret.Nombre);
            }
        }
    });
});


function gridReload(algo, nombre) {
    window.location = "indicadores.aspx?pers=" + algo + "&nom=" + nombre
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
