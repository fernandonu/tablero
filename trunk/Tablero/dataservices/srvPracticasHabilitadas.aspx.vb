Imports System.Data
Imports System.Data.SqlClient

Partial Public Class srvPracticasHabilitadas
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindPracticas()
        End If
    End Sub

    Sub bindPracticas()

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
        wherestr = " Where n.[Codigo NU Nuevo] = p.NU AND e.Nombre = p.cuie AND e.Nombre = '" & cd_mask & "' "

        If Not (String.IsNullOrEmpty(nm_mask) Or (nm_mask Is Nothing)) Then
            w2 = " AND (Prestación LIKE '%" & nm_mask & "%' OR n.[Codigo NU Nuevo] LIKE '%" & nm_mask & "%') "
            wherestr = wherestr & w2
        End If


        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "Select n.[Codigo NU Nuevo] Codigo, n.Prestación " & _
        "From facturacion.dbo.NomencladorCompleto n, facturacion.dbo.PracticasHabilitadas p, " & _
        "Nacer_chaco.dbo.Efectores e " & wherestr & " Order By Codigo"
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Practicas")

        'contar el numero de registros de la tabla
        Dim count As Integer
        Dim x As New SqlCommand
        x.Connection = myConnection
        x.CommandText = "Select Count(*) From (Select n.[Codigo NU Nuevo] Codigo, n.Prestación " & _
        "From facturacion.dbo.NomencladorCompleto n, facturacion.dbo.PracticasHabilitadas p, " & _
        "Nacer_chaco.dbo.Efectores e " & wherestr & ") t"
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

        Dim Codigo As String
        Dim Prestacion As String


        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            Codigo = myDataSet.Tables(0).Rows(i).Item("Codigo").ToString()
            Prestacion = myDataSet.Tables(0).Rows(i).Item("Prestación").ToString()

            Response.Write("<row id='" & Codigo & "'>")
            Response.Write("<cell>" & Codigo & "</cell>")
            Response.Write("<cell>" & Prestacion & "</cell>")


            Response.Write("</row>")
        Next
        Response.Write("</rows>")


    End Sub
End Class