Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Globalization
Imports System.Threading

Partial Public Class srvArbol
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)
    Dim leafnodes() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindArbol()
        End If
    End Sub

    Private Function isLeaf(ByVal id As String, ByRef arr() As String) As Boolean
        Dim i As Integer
        i = 0
        Dim x As Boolean
        x = False
        While i < arr.Length And x = False
            If id = arr(i) Then
                x = True
            Else
                x = False
            End If
            i = i + 1
        End While
        Return x
    End Function

    Sub bindArbol()
        Dim sqlString As String
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet

        ' We need first to determine the leaf nodes 
        sqlString = "SELECT t1.id FROM V_Arbol AS t1 LEFT JOIN V_Arbol as t2 " & _
        "ON t1.id = t2.padre WHERE t2.id IS NULL"
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Hojas")
        Dim cont As Integer
        cont = myDataSet.Tables("Hojas").Rows.Count - 1
        ReDim leafnodes(cont)
        Dim i As Integer
        For i = 0 To cont
            leafnodes(i) = myDataSet.Tables("Hojas").Rows(i).Item(0)
        Next

        'Get parameters from the grid 
        Dim node As String
        If Request.Form("nodeid") IsNot Nothing Then
            node = Request.Form("nodeid")
        End If
        Dim nlvl As Integer
        nlvl = Request.Form("n_level")

        Dim indcod As String
        indcod = Context.Request("indcod")

        'Build the XML doc
        Dim wh As String
        Dim an As String
        Dim fu As String
        Response.ContentType = "text/xml"
        Response.Write("<?xml version='1.0' encoding='utf-8'?>")
        Response.Write("<rows>")
        Response.Write("<page>1</page>")
        Response.Write("<total>1</total>")
        Response.Write("<records>1</records>")
        If node IsNot Nothing Then
            wh = " padre = '" & node & "' "
            nlvl = nlvl + 1
            'we should ouput next level
            'Parents
            If nlvl = 1 Then
                fu = " ,f_indzon(" & indcod & ") d"
                an = "and d.zid=id"
            End If
            If nlvl = 2 Then
                fu = " ,f_indloc(" & indcod & "," & node.Substring(1) & ") d "
                an = "and d.lid=id "
            End If
            If nlvl = 3 Then
                fu = " ,f_indefe(" & indcod & "," & node.Substring(1) & ") d"
                an = "and d.cuie=id"
            End If


        Else
            wh = " padre is null "
            an = ""
            fu = ""
            'Roots
            sqlString = "SELECT Id, Nombre, Padre,null as tendencia,null as estado FROM V_Arbol" & fu & " WHERE " & wh & an
        End If
        If nlvl <> 0 Then
            sqlString = "SELECT Id, Nombre, Padre,tendencia,estado FROM V_Arbol" & fu & " WHERE " & wh & an
        End If
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myAdapter.Fill(myDataSet, "Arbol")

        Dim id As String
        Dim nombre As String
        Dim tendencia As String
        Dim estado As String
        Dim padre As String
        Dim leaf As String
        For i = 0 To myDataSet.Tables("Arbol").Rows.Count - 1
            id = myDataSet.Tables("Arbol").Rows(i).Item("Id").ToString()
            nombre = myDataSet.Tables("Arbol").Rows(i).Item("Nombre").ToString()
            tendencia = myDataSet.Tables("Arbol").Rows(i).Item("Tendencia").ToString()
            estado = myDataSet.Tables("Arbol").Rows(i).Item("Estado").ToString()

            If myDataSet.Tables("Arbol").Rows(i).Item("Padre") IsNot System.DBNull.Value Then
                padre = myDataSet.Tables("Arbol").Rows(i).Item("Padre").ToString()
            Else
                padre = "NULL"
            End If
            If isLeaf(id, leafnodes) Then
                leaf = "true"
            Else
                leaf = "false"
            End If

            Response.Write("<row>")
            Response.Write("<cell>" & id & "</cell>")
            Response.Write("<cell>" & nombre & "</cell>")

            If tendencia = "" Then
                Response.Write("<cell><![CDATA[<font color='gray'>Sin Datos</font>]]></cell>")
            ElseIf tendencia = 2 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/down.png"" />]]></cell>")
            ElseIf tendencia = 1 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/eq.png"" />]]></cell>")
            ElseIf tendencia = 0 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/up.png"" />]]></cell>")
            End If

            If estado = "" Then
                Response.Write("<cell><![CDATA[<font color='gray'>Sin Datos</font>]]></cell>")
            ElseIf estado = 0 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/red.png"" />]]></cell>")
            ElseIf estado = 1 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/orange.png"" />]]></cell>")
            ElseIf estado = 2 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/yellow.png"" />]]></cell>")
            ElseIf estado = 3 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/green.png"" />]]></cell>")
            End If


            Response.Write("<cell>" & nlvl & "</cell>")
            Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
            Response.Write("<cell>" & leaf & "</cell>")
            Response.Write("<cell>false</cell>")
            Response.Write("</row>")
        Next
        'display_node(node, nlvl)
        Response.Write("</rows>")
    End Sub

    'Sub bindTotalAreas()
    '    Dim sqlString As String
    '    Dim myAdapter As SqlDataAdapter
    '    Dim myDataSet As DataSet
    '    ' We need first to determine the leaf nodes 
    '    sqlString = "SELECT t1.id FROM V_TotalArbolArea AS t1 LEFT JOIN V_TotalArbolArea as t2 " & _
    '    "ON t1.id = t2.padre WHERE t2.id IS NULL"
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myDataSet = New DataSet
    '    myAdapter.Fill(myDataSet, "Hojas")
    '    Dim cont As Integer
    '    cont = myDataSet.Tables("Hojas").Rows.Count - 1
    '    ReDim leafnodes(cont)
    '    Dim i As Integer
    '    For i = 0 To cont
    '        leafnodes(i) = myDataSet.Tables("Hojas").Rows(i).Item(0)
    '    Next

    '    'Get parameters from the grid 
    '    Dim node As String
    '    If Request.Form("nodeid") IsNot Nothing Then
    '        node = Request.Form("nodeid")
    '    End If
    '    Dim nlvl As Integer
    '    nlvl = Request.Form("n_level")

    '    ''Build the XML doc
    '    Dim wh As String
    '    Response.ContentType = "text/xml"
    '    Response.Write("<?xml version='1.0' encoding='utf-8'?>")
    '    Response.Write("<rows>")
    '    Response.Write("<page>1</page>")
    '    Response.Write("<total>1</total>")
    '    Response.Write("<records>1</records>")
    '    If node IsNot Nothing Then
    '        wh = " padre = '" & node & "'"
    '        'Parents
    '        nlvl = nlvl + 1
    '        'we should ouput next level
    '    Else
    '        wh = " padre is null "
    '        'Roots
    '    End If
    '    sqlString = "SELECT id, tipo, total, padre FROM V_TotalArbolArea WHERE " & wh
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myAdapter.Fill(myDataSet, "TotalAreas")

    '    Dim id As String
    '    Dim tipo As String
    '    Dim total As String
    '    Dim padre As String
    '    Dim leaf As String
    '    For i = 0 To myDataSet.Tables("TotalAreas").Rows.Count - 1
    '        id = myDataSet.Tables("TotalAreas").Rows(i).Item("id").ToString()
    '        tipo = myDataSet.Tables("TotalAreas").Rows(i).Item("tipo").ToString()
    '        total = FormatCurrency(myDataSet.Tables("TotalAreas").Rows(i).Item("total").ToString(), 2)
    '        If myDataSet.Tables("TotalAreas").Rows(i).Item("padre") IsNot System.DBNull.Value Then
    '            padre = myDataSet.Tables("TotalAreas").Rows(i).Item("padre").ToString()
    '        Else
    '            padre = "NULL"
    '        End If
    '        If isLeaf(id, leafnodes) Then
    '            leaf = "true"
    '        Else
    '            leaf = "false"
    '        End If

    '        Response.Write("<row>")
    '        Response.Write("<cell>" & id & "</cell>")
    '        Response.Write("<cell>" & tipo & "</cell>")
    '        Response.Write("<cell>" & total & "</cell>")
    '        Response.Write("<cell>" & nlvl & "</cell>")
    '        Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
    '        Response.Write("<cell>" & leaf & "</cell>")
    '        Response.Write("<cell>false</cell>")
    '        Response.Write("</row>")
    '    Next
    '    Response.Write("</rows>")
    'End Sub
    'Sub bindTotalProgramas()
    '    Dim sqlString As String
    '    Dim myAdapter As SqlDataAdapter
    '    Dim myDataSet As DataSet
    '    ' We need first to determine the leaf nodes 
    '    sqlString = "SELECT t1.id FROM V_TotalArbolPrograma AS t1 LEFT JOIN V_TotalArbolPrograma as t2 " & _
    '    "ON t1.id = t2.padre WHERE t2.id IS NULL"
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myDataSet = New DataSet
    '    myAdapter.Fill(myDataSet, "Hojas")
    '    Dim cont As Integer
    '    cont = myDataSet.Tables("Hojas").Rows.Count - 1
    '    ReDim leafnodes(cont)
    '    Dim i As Integer
    '    For i = 0 To cont
    '        leafnodes(i) = myDataSet.Tables("Hojas").Rows(i).Item(0)
    '    Next

    '    'Get parameters from the grid 
    '    Dim node As String
    '    If Request.Form("nodeid") IsNot Nothing Then
    '        node = Request.Form("nodeid")
    '    End If
    '    Dim nlvl As Integer
    '    nlvl = Request.Form("n_level")

    '    ''Build the XML doc
    '    Dim wh As String
    '    Response.ContentType = "text/xml"
    '    Response.Write("<?xml version='1.0' encoding='utf-8'?>")
    '    Response.Write("<rows>")
    '    Response.Write("<page>1</page>")
    '    Response.Write("<total>1</total>")
    '    Response.Write("<records>1</records>")
    '    If node IsNot Nothing Then
    '        wh = " padre = '" & node & "'"
    '        'Parents
    '        nlvl = nlvl + 1
    '        'we should ouput next level
    '    Else
    '        wh = " padre is null "
    '        'Roots
    '    End If
    '    sqlString = "SELECT id, tipo, total, padre FROM V_TotalArbolPrograma WHERE " & wh
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myAdapter.Fill(myDataSet, "TotalProgramas")

    '    Dim id As String
    '    Dim tipo As String
    '    Dim total As String
    '    Dim padre As String
    '    Dim leaf As String
    '    For i = 0 To myDataSet.Tables("TotalProgramas").Rows.Count - 1
    '        id = myDataSet.Tables("TotalProgramas").Rows(i).Item("id").ToString()
    '        tipo = myDataSet.Tables("TotalProgramas").Rows(i).Item("tipo").ToString()
    '        total = FormatCurrency(myDataSet.Tables("TotalProgramas").Rows(i).Item("total").ToString(), 2)
    '        If myDataSet.Tables("TotalProgramas").Rows(i).Item("padre") IsNot System.DBNull.Value Then
    '            padre = myDataSet.Tables("TotalProgramas").Rows(i).Item("padre").ToString()
    '        Else
    '            padre = "NULL"
    '        End If
    '        If isLeaf(id, leafnodes) Then
    '            leaf = "true"
    '        Else
    '            leaf = "false"
    '        End If

    '        Response.Write("<row>")
    '        Response.Write("<cell>" & id & "</cell>")
    '        Response.Write("<cell>" & tipo & "</cell>")
    '        Response.Write("<cell>" & total & "</cell>")
    '        Response.Write("<cell>" & nlvl & "</cell>")
    '        Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
    '        Response.Write("<cell>" & leaf & "</cell>")
    '        Response.Write("<cell>false</cell>")
    '        Response.Write("</row>")
    '    Next
    '    Response.Write("</rows>")
    'End Sub
    'Sub bindTotalObjProg()
    '    Dim sqlString As String
    '    Dim myAdapter As SqlDataAdapter
    '    Dim myDataSet As DataSet
    '    ' We need first to determine the leaf nodes 
    '    sqlString = "SELECT t1.id FROM V_TotalArbolObjProg AS t1 LEFT JOIN V_TotalArbolObjProg as t2 " & _
    '    "ON t1.id = t2.padre WHERE t2.id IS NULL"
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myDataSet = New DataSet
    '    myAdapter.Fill(myDataSet, "Hojas")
    '    Dim cont As Integer
    '    cont = myDataSet.Tables("Hojas").Rows.Count - 1
    '    ReDim leafnodes(cont)
    '    Dim i As Integer
    '    For i = 0 To cont
    '        leafnodes(i) = myDataSet.Tables("Hojas").Rows(i).Item(0)
    '    Next

    '    'Get parameters from the grid 
    '    Dim node As String
    '    If Request.Form("nodeid") IsNot Nothing Then
    '        node = Request.Form("nodeid")
    '    End If
    '    Dim nlvl As Integer
    '    nlvl = Request.Form("n_level")

    '    ''Build the XML doc
    '    Dim wh As String
    '    Response.ContentType = "text/xml"
    '    Response.Write("<?xml version='1.0' encoding='utf-8'?>")
    '    Response.Write("<rows>")
    '    Response.Write("<page>1</page>")
    '    Response.Write("<total>1</total>")
    '    Response.Write("<records>1</records>")
    '    If node IsNot Nothing Then
    '        wh = " padre = '" & node & "'"
    '        'Parents
    '        nlvl = nlvl + 1
    '        'we should ouput next level
    '    Else
    '        wh = " padre is null "
    '        'Roots
    '    End If
    '    sqlString = "SELECT id, tipo, total, padre FROM V_TotalArbolObjProg WHERE " & wh
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myAdapter.Fill(myDataSet, "TotalObjProg")

    '    Dim id As String
    '    Dim tipo As String
    '    Dim total As String
    '    Dim padre As String
    '    Dim leaf As String
    '    For i = 0 To myDataSet.Tables("TotalObjProg").Rows.Count - 1
    '        id = myDataSet.Tables("TotalObjProg").Rows(i).Item("id").ToString()
    '        tipo = myDataSet.Tables("TotalObjProg").Rows(i).Item("tipo").ToString()
    '        total = FormatCurrency(myDataSet.Tables("TotalObjProg").Rows(i).Item("total").ToString(), 2)
    '        If myDataSet.Tables("TotalObjProg").Rows(i).Item("padre") IsNot System.DBNull.Value Then
    '            padre = myDataSet.Tables("TotalObjProg").Rows(i).Item("padre").ToString()
    '        Else
    '            padre = "NULL"
    '        End If
    '        If isLeaf(id, leafnodes) Then
    '            leaf = "true"
    '        Else
    '            leaf = "false"
    '        End If

    '        Response.Write("<row>")
    '        Response.Write("<cell>" & id & "</cell>")
    '        Response.Write("<cell>" & tipo & "</cell>")
    '        Response.Write("<cell>" & total & "</cell>")
    '        Response.Write("<cell>" & nlvl & "</cell>")
    '        Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
    '        Response.Write("<cell>" & leaf & "</cell>")
    '        Response.Write("<cell>false</cell>")
    '        Response.Write("</row>")
    '    Next
    '    Response.Write("</rows>")
    'End Sub
    'Sub bindTotalObjArea()
    '    Dim sqlString As String
    '    Dim myAdapter As SqlDataAdapter
    '    Dim myDataSet As DataSet
    '    ' We need first to determine the leaf nodes 
    '    sqlString = "SELECT t1.id FROM V_TotalArbolObjArea AS t1 LEFT JOIN V_TotalArbolObjArea as t2 " & _
    '    "ON t1.id = t2.padre WHERE t2.id IS NULL"
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myDataSet = New DataSet
    '    myAdapter.Fill(myDataSet, "Hojas")
    '    Dim cont As Integer
    '    cont = myDataSet.Tables("Hojas").Rows.Count - 1
    '    ReDim leafnodes(cont)
    '    Dim i As Integer
    '    For i = 0 To cont
    '        leafnodes(i) = myDataSet.Tables("Hojas").Rows(i).Item(0)
    '    Next

    '    'Get parameters from the grid 
    '    Dim node As String
    '    If Request.Form("nodeid") IsNot Nothing Then
    '        node = Request.Form("nodeid")
    '    End If
    '    Dim nlvl As Integer
    '    nlvl = Request.Form("n_level")

    '    ''Build the XML doc
    '    Dim wh As String
    '    Response.ContentType = "text/xml"
    '    Response.Write("<?xml version='1.0' encoding='utf-8'?>")
    '    Response.Write("<rows>")
    '    Response.Write("<page>1</page>")
    '    Response.Write("<total>1</total>")
    '    Response.Write("<records>1</records>")
    '    If node IsNot Nothing Then
    '        wh = " padre = '" & node & "'"
    '        'Parents
    '        nlvl = nlvl + 1
    '        'we should ouput next level
    '    Else
    '        wh = " padre is null "
    '        'Roots
    '    End If
    '    sqlString = "SELECT id, tipo, total, padre FROM V_TotalArbolObjArea WHERE " & wh
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myAdapter.Fill(myDataSet, "TotalObjArea")

    '    Dim id As String
    '    Dim tipo As String
    '    Dim total As String
    '    Dim padre As String
    '    Dim leaf As String
    '    For i = 0 To myDataSet.Tables("TotalObjArea").Rows.Count - 1
    '        id = myDataSet.Tables("TotalObjArea").Rows(i).Item("id").ToString()
    '        tipo = myDataSet.Tables("TotalObjArea").Rows(i).Item("tipo").ToString()
    '        total = FormatCurrency(myDataSet.Tables("TotalObjArea").Rows(i).Item("total").ToString(), 2)
    '        If myDataSet.Tables("TotalObjArea").Rows(i).Item("padre") IsNot System.DBNull.Value Then
    '            padre = myDataSet.Tables("TotalObjArea").Rows(i).Item("padre").ToString()
    '        Else
    '            padre = "NULL"
    '        End If
    '        If isLeaf(id, leafnodes) Then
    '            leaf = "true"
    '        Else
    '            leaf = "false"
    '        End If

    '        Response.Write("<row>")
    '        Response.Write("<cell>" & id & "</cell>")
    '        Response.Write("<cell>" & tipo & "</cell>")
    '        Response.Write("<cell>" & total & "</cell>")
    '        Response.Write("<cell>" & nlvl & "</cell>")
    '        Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
    '        Response.Write("<cell>" & leaf & "</cell>")
    '        Response.Write("<cell>false</cell>")
    '        Response.Write("</row>")
    '    Next
    '    Response.Write("</rows>")
    'End Sub

    'Sub bindTotalObjGastoNested()
    '    Dim sqlString As String
    '    Dim myAdapter As SqlDataAdapter
    '    Dim myCommand As SqlCommand
    '    Dim myDataSet As DataSet
    '    Dim count As Integer
    '    sqlString = "SELECT COUNT(*) as count FROM stack"
    '    myCommand = New SqlCommand(sqlString, myConnection)
    '    myConnection.Open()
    '    count = myCommand.ExecuteScalar
    '    myConnection.Close()
    '    ' We need first to determine the leaf nodes 
    '    sqlString = "SELECT t1.id FROM V_TotalesArbol AS t1 LEFT JOIN V_TotalesArbol as t2 " & _
    '    "ON t1.id = t2.padre WHERE t2.id IS NULL"
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myDataSet = New DataSet
    '    myAdapter.Fill(myDataSet, "Hojas")
    '    Dim cont As Integer
    '    cont = myDataSet.Tables("Hojas").Rows.Count - 1
    '    ReDim leafnodes(cont)
    '    Dim i As Integer
    '    For i = 0 To cont
    '        leafnodes(i) = myDataSet.Tables("Hojas").Rows(i).Item(0)
    '    Next

    '    'Get parameters from the grid 
    '    'Dim node As String
    '    'If Context.Request.QueryString("nodeid") IsNot Nothing Then
    '    '    node = Context.Request.QueryString("nodeid")
    '    'End If
    '    'Dim nlvl As Integer
    '    'nlvl = Context.Request.QueryString("n_level")
    '    Dim nlvl, nleft, nright As Integer
    '    Dim node As String
    '    nleft = Request.Form("n_left")
    '    nright = Request.Form("n_right")
    '    If Request.Form("nodeid") IsNot Nothing Then
    '        node = Request.Form("nodeid")
    '        nlvl = Request.Form("n_level")
    '    Else
    '        nlvl = 0
    '    End If


    '    ''Build the XML doc
    '    Dim wh As String
    '    Response.ContentType = "text/xml"
    '    Response.Write("<?xml version='1.0' encoding='utf-8'?>")
    '    Response.Write("<rows>")
    '    Response.Write("<page>1</page>")
    '    Response.Write("<total>1</total>")
    '    Response.Write("<records>1</records>")
    '    If node IsNot Nothing Then
    '        wh = " AND node.lft > " & nleft & " AND node.rgt < " & nright
    '    Else
    '        wh = ""
    '    End If
    '    sqlString = "SELECT node.id, node.tipo, node.total, (COUNT(parent.id) - 1) AS level, node.lft, node.rgt " & _
    '    "FROM V_Nested AS node, V_Nested AS parent " & _
    '    "WHERE node.lft BETWEEN parent.lft AND parent.rgt " & wh & _
    '    "GROUP BY node.id, node.tipo, node.total, node.lft, node.rgt " & _
    '    "ORDER BY node.lft"
    '    myAdapter = New SqlDataAdapter(sqlString, myConnection)
    '    myAdapter.Fill(myDataSet, "TotalOG")

    '    Dim id As Integer
    '    Dim tipo As String
    '    Dim total As String
    '    Dim left As String
    '    Dim right As String
    '    Dim leaf As String
    '    Dim level As String
    '    For i = 0 To myDataSet.Tables("TotalOG").Rows.Count - 1
    '        id = myDataSet.Tables("TotalOG").Rows(i).Item("id").ToString()
    '        tipo = myDataSet.Tables("TotalOG").Rows(i).Item("tipo").ToString()
    '        total = myDataSet.Tables("TotalOG").Rows(i).Item("total").ToString()
    '        left = myDataSet.Tables("TotalOG").Rows(i).Item("lft").ToString()
    '        right = myDataSet.Tables("TotalOG").Rows(i).Item("rgt").ToString()
    '        level = myDataSet.Tables("TotalOG").Rows(i).Item("level").ToString()
    '        If right = left + 1 Then
    '            leaf = "true"
    '        Else
    '            leaf = "false"
    '        End If

    '        Response.Write("<row>")
    '        Response.Write("<cell>" & id & "</cell>")
    '        Response.Write("<cell>" & tipo & "</cell>")
    '        Response.Write("<cell>" & total & "</cell>")
    '        Response.Write("<cell>" & level & "</cell>")
    '        Response.Write("<cell>" & left & "</cell>")
    '        Response.Write("<cell>" & right & "</cell>")
    '        Response.Write("<cell>" & leaf & "</cell>")
    '        Response.Write("<cell>false</cell>")
    '        Response.Write("</row>")
    '    Next
    '    'display_node(node, nlvl)
    '    Response.Write("</rows>")
    'End Sub
    'Private Sub display_node(ByVal parent As Integer, ByVal level As Integer)
    '    Dim myAdapter As SqlDataAdapter
    '    Dim myDataSet As DataSet
    '    Dim sqlstring As String
    '    Dim wh As String
    '    If parent >= 0 Then
    '        wh = " padre = " & parent
    '    Else
    '        wh = " padre is null "
    '    End If
    '    sqlstring = "SELECT id, tipo, total, padre FROM V_TotalesArbol WHERE " & wh
    '    myDataSet = New DataSet
    '    myAdapter = New SqlDataAdapter(sqlstring, myConnection)
    '    myAdapter.Fill(myDataSet, "TotalOG")

    '    Dim id As Integer
    '    Dim tipo As String
    '    Dim total As String
    '    Dim padre As String
    '    Dim leaf As String
    '    Dim i As Integer = 0
    '    While i < myDataSet.Tables("TotalOG").Rows.Count
    '        id = myDataSet.Tables("TotalOG").Rows(i).Item("id").ToString()
    '        tipo = myDataSet.Tables("TotalOG").Rows(i).Item("tipo").ToString()
    '        total = myDataSet.Tables("TotalOG").Rows(i).Item("total").ToString()
    '        If myDataSet.Tables("TotalOG").Rows(i).Item("padre") IsNot System.DBNull.Value Then
    '            padre = myDataSet.Tables("TotalOG").Rows(i).Item("padre").ToString()
    '        Else
    '            padre = "NULL"
    '        End If
    '        If Not isLeaf(id, leafnodes) Then
    '            leaf = "false"
    '        Else
    '            leaf = "true"
    '        End If

    '        Response.Write("<row>")
    '        Response.Write("<cell>" & id & "</cell>")
    '        Response.Write("<cell>" & tipo & "</cell>")
    '        Response.Write("<cell>" & total & "</cell>")
    '        Response.Write("<cell>" & padre & "</cell>")
    '        Response.Write("<cell>" & level & "</cell>")
    '        Response.Write("<cell>false</cell>")
    '        Response.Write("<cell>" & leaf & "</cell>")
    '        Response.Write("</row>")
    '        display_node(id, level + 1)
    '        i = i + 1
    '    End While

    'End Sub


End Class