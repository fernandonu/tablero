Imports System.Data
Imports System.Data.SqlClient

Partial Public Class srvDatosEfector
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString1").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindNomenclador()
        End If
    End Sub
    Sub bindNomenclador()

        Dim sqlString As String
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet

        'obtener la pagina solicitada dentro del grid
        Dim page As String = Integer.Parse(Request.QueryString("page"))
        'obtener el número de filas que queremos tener en el grid
        Dim limit As Integer = Integer.Parse(Context.Request.QueryString("rows"))
        'obtener índice de la fila
        Dim sidx As String = Request.QueryString.Get("sidx")
        'obtener la dirección
        Dim sord As String = Request.QueryString.Get("sord")
        'obtengo parametros de busqueda
        Dim nm_mask As String = Request.QueryString.Get("nm_mask")
        Dim cd_mask As String = Request.QueryString.Get("cd_mask")

        'si no existe índice por utilizar la primera columna
        If String.IsNullOrEmpty(sidx) Then
            sidx = 1
        End If

        'construyo las sentencias where
        Dim w2, w3 As String
        Dim wherestr As String
        wherestr = " WHERE 1 = 1"

        If Not (String.IsNullOrEmpty(nm_mask) Or (nm_mask Is Nothing)) Then
            w2 = " AND ( Efector LIKE '%" & nm_mask & "%' OR Zona Like '%" & nm_mask & "%' OR Localidad Like '%" & nm_mask & "%' )"
            wherestr = wherestr & w2
        End If
        'If Not (String.IsNullOrEmpty(cd_mask) Or (cd_mask Is Nothing)) Then

        '    w3 = " AND Tipo Like '%" & cd_mask & "%'"
        '    wherestr = wherestr & w3

        'End If

        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "Select Zona, Localidad, Nombre as CUIE, Efector, Direccion, " & _
        "(Select Case TipoEfector When 'PSA' Then 'Puesto Sanitario' When 'CSA' Then 'Centro de Salud' Else 'Hospital' End) Tipo " & _
        "From Efectores " & wherestr & "Order By Zona, Localidad, CUIE"
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Efector")

        'contar el numero de registros de la tabla
        Dim count As Integer
        Dim x As New SqlCommand
        x.Connection = myConnection
        x.CommandText = "Select Count(*) From Efectores " & wherestr
        myConnection.Open()
        count = x.ExecuteScalar
        myConnection.Close()

        'calculo el numero de paginas
        Dim total_pages As Integer
        If count > 0 Then
            total_pages = -Int(-(count / limit))
            If total_pages = 0 Then
                total_pages = 1
            End If
        Else
            total_pages = 0
        End If

        'en caso de que la pagina sea mayor que el total.. voy al la ultima
        If page > total_pages Then
            page = total_pages
        End If

        'Construyo el documento XML
        Response.ContentType = "text/xml"
        Response.Write("<?xml version='1.0' encoding='utf-8'?>")
        'Response.Write(myDataSet.GetXml())

        Response.Write("<rows>")
        Response.Write("<page>" & page.ToString & "</page>")
        Response.Write("<total>" & total_pages.ToString & "</total>")
        Response.Write("<records>" & count.ToString & "</records>")

        Dim Zona As String
        Dim CUIE As String
        Dim Efector As String
        Dim Localidad As String
        Dim Direccion As String
        Dim Tipo As String

        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            Zona = myDataSet.Tables(0).Rows(i).Item("Zona").ToString()
            CUIE = myDataSet.Tables(0).Rows(i).Item("CUIE").ToString()
            Efector = myDataSet.Tables(0).Rows(i).Item("Efector").ToString()
            Localidad = myDataSet.Tables(0).Rows(i).Item("Localidad").ToString()
            Direccion = myDataSet.Tables(0).Rows(i).Item("Direccion").ToString()
            Tipo = myDataSet.Tables(0).Rows(i).Item("Tipo").ToString()

            Response.Write("<row id='" & CUIE & "'>")
            Response.Write("<cell>" & Zona & "</cell>")
            Response.Write("<cell>" & Localidad & "</cell>")
            Response.Write("<cell>" & CUIE & "</cell>")
            Response.Write("<cell>" & Efector & "</cell>")
            Response.Write("<cell>" & Direccion & "</cell>")
            Response.Write("<cell>" & Tipo & "</cell>")

            Response.Write("</row>")
        Next
        Response.Write("</rows>")


    End Sub
End Class