jQuery(document).ready(function() {
 
 $("#accordion").accordion({ fillSpace: true });
 
 jQuery("#gridexp").jqGrid({
        url: 'dataservices/srvExpedientes.aspx?tipo=1',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Facturas', 'Practicas', 'Dias'],
        colModel: [
            { name: 'AntFac', index: 'AntFac', width: 100, sortable: false, align: 'center'},
            { name: 'AntPra', index: 'AntPra', width: 100, sortable: false, align: 'center'},
            { name: 'AntDia', index: 'AntDia', width: 100, sortable: false, align: 'center'}],
        rowNum: 100,
        viewrecords: true,
        sortname: 'AntFac',
        height: 100,
        caption: 'Antiguedad de Expedientes',
        loadui: 'block'
    });
    
    jQuery("#gridrech").jqGrid({
        url: 'dataservices/srvExpedientes.aspx?tipo=2',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Observaciones', 'Cantidad'],
        colModel: [
            { name: 'Observaciones', index: 'Observaciones', width: 200, sortable: false},
            { name: 'Cantidad', index: 'Cantidad', width: 100, sortable: false, align: 'center'}],
        rowNum: 50,
        viewrecords: true,
        sortname: 'Cantidad',
        height: 150,
        caption: 'Grilla de Rechazos',
        loadui: 'block'
    });   
    
    jQuery("#gridefe").jqGrid({
        url: 'dataservices/srvExpedientes.aspx?tipo=3',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Cuie', 'Efector'],
        colModel: [
            { name: 'Cuie', index: 'Cuie', width: 50, sortable: false},
            { name: 'Efector', index: 'Efector', width: 200, sortable: false, align: 'center'}],
        rowNum: 200,
        viewrecords: true,
        sortname: 'Cantidad',
        height: 150,
        caption: 'Efectores con Factura',
        loadui: 'block'
    });     
});

       
function gridReload(tipo, exp) {
    if (tipo=1) {
    jQuery("#gridexp").setGridParam({ url: "dataservices/srvExpedientes.aspx?tipo=1&nm_mask=" + exp, page: 1 }).trigger("reloadGrid");
    jQuery("#gridefe").setGridParam({ url: "dataservices/srvExpedientes.aspx?tipo=3&nm_mask=" + exp, page: 1 }).trigger("reloadGrid");

    }
    if (tipo=2) {
    jQuery("#gridrech").setGridParam({ url: "dataservices/srvExpedientes.aspx?tipo=2&nm_mask=" + exp, page: 1 }).trigger("reloadGrid");
    }
}
    


$(function() {

    $('#btnexp').click(function() {
        var exp = jQuery("#txtexp").val();
        gridReload(1, exp);
    }).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });

});


$(function() {

    $('#btnrech').click(function() {
        var exp = jQuery("#txtrech").val();
        gridReload(2, exp);
    }).hover(function() { $(this).addClass("ui-state-hover"); },
		     function() { $(this).removeClass("ui-state-hover"); }
	).mousedown(function() { $(this).addClass("ui-state-active"); }
	).mouseup(function() { $(this).removeClass("ui-state-active"); });

});


  