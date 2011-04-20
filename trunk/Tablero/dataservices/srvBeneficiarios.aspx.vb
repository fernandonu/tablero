Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Globalization
Imports System.Threading

Partial Public Class srvBeneficiarios
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString1").ConnectionString)
    Dim leafnodes() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Select Case Context.Request("tipo")
                Case 1
                    bindArbolEfectores()
                Case 2
                    bindArbolCategorias()
                Case 3
                    bindArbolZonas()
                Case 4
                    bindArbolEdad()
            End Select
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

    Sub bindArbolZonas()
        Dim sqlString As String
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet

        ' We need first to determine the leaf nodes 
        sqlString = "SELECT t1.id FROM V_ArbolZonas AS t1 LEFT JOIN V_ArbolZonas as t2 " & _
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


        'Build the XML doc
        Dim wh As String

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

        Else
            wh = " padre is null "
            'Roots
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolZonas WHERE " & wh
        End If
        If nlvl <> 0 Then
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolZonas WHERE " & wh
        End If
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myAdapter.Fill(myDataSet, "Arbol")

        Dim id As String
        Dim nombre As String
        Dim total As String
        Dim potencial As String

        Dim padre As String
        Dim leaf As String
        For i = 0 To myDataSet.Tables("Arbol").Rows.Count - 1
            id = myDataSet.Tables("Arbol").Rows(i).Item("Id").ToString()
            nombre = myDataSet.Tables("Arbol").Rows(i).Item("Nombre").ToString()
            total = myDataSet.Tables("Arbol").Rows(i).Item("Total").ToString()
            potencial = myDataSet.Tables("Arbol").Rows(i).Item("Potencial").ToString()

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
            Response.Write("<cell>" & total & "</cell>")
            Response.Write("<cell>" & potencial & "</cell>")

            Response.Write("<cell>" & nlvl & "</cell>")
            Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
            Response.Write("<cell>" & leaf & "</cell>")
            Response.Write("<cell>false</cell>")
            Response.Write("</row>")
        Next

        Response.Write("</rows>")
    End Sub

    Sub bindArbolEfectores()
        Dim sqlString As String
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet

        ' We need first to determine the leaf nodes 
        sqlString = "SELECT t1.id FROM V_ArbolEfector AS t1 LEFT JOIN V_ArbolEfector as t2 " & _
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


        'Build the XML doc
        Dim wh As String

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

        Else
            wh = " padre is null "
            'Roots
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolEfector WHERE " & wh
        End If
        If nlvl <> 0 Then
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolEfector WHERE " & wh
        End If
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myAdapter.Fill(myDataSet, "Arbol")

        Dim id As String
        Dim nombre As String
        Dim total As String
        Dim potencial As String

        Dim padre As String
        Dim leaf As String
        For i = 0 To myDataSet.Tables("Arbol").Rows.Count - 1
            id = myDataSet.Tables("Arbol").Rows(i).Item("Id").ToString()
            nombre = myDataSet.Tables("Arbol").Rows(i).Item("Nombre").ToString()
            total = myDataSet.Tables("Arbol").Rows(i).Item("Total").ToString()
            potencial = myDataSet.Tables("Arbol").Rows(i).Item("Potencial").ToString()

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
            Response.Write("<cell>" & total & "</cell>")
            Response.Write("<cell>" & potencial & "</cell>")

            Response.Write("<cell>" & nlvl & "</cell>")
            Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
            Response.Write("<cell>" & leaf & "</cell>")
            Response.Write("<cell>false</cell>")
            Response.Write("</row>")
        Next

        Response.Write("</rows>")
    End Sub

    Sub bindArbolCategorias()
        Dim sqlString As String
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet

        ' We need first to determine the leaf nodes 
        sqlString = "SELECT t1.id FROM V_ArbolCategorias AS t1 LEFT JOIN V_ArbolCategorias as t2 " & _
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


        'Build the XML doc
        Dim wh As String

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

        Else
            wh = " padre is null "
            'Roots
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolCategorias WHERE " & wh
        End If
        If nlvl <> 0 Then
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolCategorias WHERE " & wh
        End If
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myAdapter.Fill(myDataSet, "Arbol")

        Dim id As String
        Dim nombre As String
        Dim total As String
        Dim potencial As String

        Dim padre As String
        Dim leaf As String
        For i = 0 To myDataSet.Tables("Arbol").Rows.Count - 1
            id = myDataSet.Tables("Arbol").Rows(i).Item("Id").ToString()
            nombre = myDataSet.Tables("Arbol").Rows(i).Item("Nombre").ToString()
            total = myDataSet.Tables("Arbol").Rows(i).Item("Total").ToString()
            potencial = myDataSet.Tables("Arbol").Rows(i).Item("Potencial").ToString()

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
            Response.Write("<cell>" & total & "</cell>")
            Response.Write("<cell>" & potencial & "</cell>")

            Response.Write("<cell>" & nlvl & "</cell>")
            Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
            Response.Write("<cell>" & leaf & "</cell>")
            Response.Write("<cell>false</cell>")
            Response.Write("</row>")
        Next

        Response.Write("</rows>")
    End Sub

    Sub bindArbolEdad()
        Dim sqlString As String
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet

        ' We need first to determine the leaf nodes 
        sqlString = "SELECT t1.id FROM V_ArbolEdad AS t1 LEFT JOIN V_ArbolEdad as t2 " & _
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


        'Build the XML doc
        Dim wh As String

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

        Else
            wh = " padre is null "
            'Roots
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolEdad WHERE " & wh
        End If
        If nlvl <> 0 Then
            sqlString = "SELECT Id, Nombre, Total, Potencial, Padre FROM V_ArbolEdad WHERE " & wh
        End If
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myAdapter.Fill(myDataSet, "Arbol")

        Dim id As String
        Dim nombre As String
        Dim total As String
        Dim potencial As String

        Dim padre As String
        Dim leaf As String
        For i = 0 To myDataSet.Tables("Arbol").Rows.Count - 1
            id = myDataSet.Tables("Arbol").Rows(i).Item("Id").ToString()
            nombre = myDataSet.Tables("Arbol").Rows(i).Item("Nombre").ToString()
            total = myDataSet.Tables("Arbol").Rows(i).Item("Total").ToString()
            potencial = myDataSet.Tables("Arbol").Rows(i).Item("Potencial").ToString()

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
            Response.Write("<cell>" & total & "</cell>")
            Response.Write("<cell>" & potencial & "</cell>")

            Response.Write("<cell>" & nlvl & "</cell>")
            Response.Write("<cell><![CDATA[" & padre & "]]></cell>")
            Response.Write("<cell>" & leaf & "</cell>")
            Response.Write("<cell>false</cell>")
            Response.Write("</row>")
        Next

        Response.Write("</rows>")
    End Sub

End Class