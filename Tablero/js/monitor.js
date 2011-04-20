jQuery(document).ready(function() {
 
 $("#accordion").accordion({ fillSpace: true });
 
 jQuery("#gridrec").jqGrid({
        url: 'dataservices/srvRecibidos.aspx',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Tipo', 'Potencial', 'Esperado', 'Recibido', 'Porcentaje'],
        colModel: [
            { name: 'Tipo', index: 'Tipo', width: 100, sortable: false },
            { name: 'Potencial', index: 'Potencial', width: 120, sortable: false, align: 'center' },
            { name: 'Esperado', index: 'Esperado', width: 120, sortable: false, align: 'center' },
            { name: 'Recibido', index: 'Recibido', width: 120, sortable: false, align: 'center'},
            { name: 'Porcentaje', index: 'Porcentaje', width: 100, sortable: false, align: 'center'}],
        //pager: jQuery('#pefec'),
        rowNum: 10,
        viewrecords: true,
        sortname: 'Porcentaje',
        height: 100,
        loadui: 'block'
    });
    
    jQuery("#gridcd").jqGrid({
        url: 'dataservices/srvCargaDiaria.aspx',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Usuario', 'Puntos'],
        colModel: [
            { name: 'Usuario', index: 'Usuario', width: 100, sortable: false },
            { name: 'Puntos', index: 'Puntos', width: 120, sortable: false, align: 'center' }],
        rowNum: 15,
        viewrecords: true,
        sortname: 'Puntos',
        height: 250,
        loadui: 'block'
    });
    
        jQuery("#gridcar").jqGrid({
        url: 'dataservices/srvCargados.aspx',
        datatype: 'xml',
        mtype: 'GET',
        colNames: ['Tipo', 'Cargado','Potencial','Porcentaje'],
        colModel: [
            { name: 'Tipo', index: 'Tipo', width: 100, sortable: false },
            { name: 'Cargado', index: 'Cargado', width: 120, sortable: false, align: 'center' },
            { name: 'Potencial', index: 'Potencial', width: 120, sortable: false, align: 'center' },
            { name: 'Porcentaje', index: 'Porcentaje', width: 120, sortable: false, align: 'center' }],        
        rowNum: 15,
        viewrecords: true,
        sortname: 'Tipo',
        height: 150,
        loadui: 'block'
    });
    
      
});

       
    //Temporizadores
    var count = 0;
    var max = 5;
    function contar(){ 
        count++;             
        if (count == max)
        count = 0;
        setTimeout("contar()", 60000);
        $("#contador").val('Próxima actualización en ' + (max - count) + ' min');
    }
    setTimeout("contar()", 60000);
    $("#contador").val('Próxima actualización en ' + (max - count) + ' min');

    
    var countT = 0;
    var maxT = 5;
    function actualizarcd(){ 
        countT++;             
        if (countT == maxT)
        countT = 0;
        jQuery("#gridcd").setGridParam({ url: "dataservices/srvCargaDiaria.aspx", page: 1 }).trigger("reloadGrid");
        setTimeout("actualizarcd()", 300000);
    }
    setTimeout("actualizarcd()", 300000);
    
  