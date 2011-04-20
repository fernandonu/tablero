Imports System.Data
Imports System.Data.SqlClient

Partial Public Class srvCargados
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindCargados()
        End If
    End Sub

    Sub bindCargados()

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


        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "exec Cargados"
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Cargados")

        'contar el numero de registros de la tabla
        Dim count As Integer = 4


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

        Dim Tipo As String
        Dim Cargado As String
        Dim Potencial As String
        Dim Porcentaje As String


        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            Tipo = myDataSet.Tables(0).Rows(i).Item("Tipo").ToString()
            Cargado = myDataSet.Tables(0).Rows(i).Item("Cargado").ToString()
            Potencial = myDataSet.Tables(0).Rows(i).Item("Potencial").ToString()
            Porcentaje = CInt(myDataSet.Tables(0).Rows(i).Item("Porcentaje").ToString())

            If Tipo = "E" Then
                Tipo = "Embarazadas"
            ElseIf Tipo = "N" Then
                Tipo = "Niños"
            ElseIf Tipo = "V" Then
                Tipo = "Vacunas"
            ElseIf Tipo = "P" Then
                Tipo = "Partos"
            End If

            Response.Write("<row id='" & Tipo & "'>")
            Response.Write("<cell>" & Tipo & "</cell>")
            Response.Write("<cell>" & Cargado & "</cell>")
            Response.Write("<cell>" & Potencial & "</cell>")
            Response.Write("<cell>" & Porcentaje & "</cell>")

            Response.Write("</row>")
        Next
        Response.Write("</rows>")


    End Sub
End Class