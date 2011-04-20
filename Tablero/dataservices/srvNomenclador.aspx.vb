Imports System.Data
Imports System.Data.SqlClient

Partial Public Class srvNomenclador
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)

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
            w2 = " AND (Prestacion LIKE '%" & nm_mask & "%' OR [Codigo NU Nuevo] LIKE '%" & nm_mask & "%') "
            wherestr = wherestr & w2
        End If
        'If Not (String.IsNullOrEmpty(cd_mask) Or (cd_mask Is Nothing)) Then

        '    w3 = " AND Codigo Like '%" & cd_mask & "%'"
        '    wherestr = wherestr & w3

        'End If

        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "Select [Codigo NU Nuevo] As Codigo, Prestación as Prestacion, PrecioSMip4 as Precio From facturacion.dbo.NomencladorCompleto  Inner Join facturacion.dbo.PreciosNomenclador" & _
                    " On facturacion.dbo.NomencladorCompleto.[Codigo NU Nuevo]= facturacion.dbo.PreciosNomenclador.[Codigo NU] " & _
                    " And FechaDesde <= Getdate() And FechaHasta >= Getdate()" & wherestr & " Order By Codigo"
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Nomenclador")

        'contar el numero de registros de la tabla
        Dim count As Integer
        Dim x As New SqlCommand
        x.Connection = myConnection
        x.CommandText = "Select Count(*) From facturacion.dbo.NomencladorCompleto " & wherestr
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
        Response.Write("<rows>")

        Response.Write("<page>" & page.ToString & "</page>")
        Response.Write("<total>" & total_pages.ToString & "</total>")
        Response.Write("<records>" & count.ToString & "</records>")
       
        Dim Codigo As String
        Dim Prestacion As String
        Dim Precio As String


        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            Codigo = myDataSet.Tables(0).Rows(i).Item("Codigo").ToString()
            Prestacion = myDataSet.Tables(0).Rows(i).Item("Prestacion").ToString()
            Precio = myDataSet.Tables(0).Rows(i).Item("Precio").ToString()


            Response.Write("<row id='" & Codigo & "'>")
            Response.Write("<cell>" & Codigo & "</cell>")
            Response.Write("<cell>" & Prestacion & "</cell>")
            Response.Write("<cell>" & Precio & "</cell>")
            Response.Write("</row>")

        Next
        Response.Write("</rows>")
        

    End Sub
End Class