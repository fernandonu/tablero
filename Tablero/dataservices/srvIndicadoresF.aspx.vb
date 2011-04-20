Imports System.Data
Imports System.Data.SqlClient

Partial Public Class srvIndicadoresF
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindIndicador()
        End If
    End Sub

    Sub bindIndicador()

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
        Dim cuie As String = Context.Request("cuie")

        'si no existe índice por utilizar la primera columna
        If String.IsNullOrEmpty(sidx) Then
            sidx = 1
        End If

        'construyo las sentencias where
        Dim w2, w3 As String
        Dim wherestr As String
        wherestr = " WHERE 1 = 1"

        If Not (String.IsNullOrEmpty(nm_mask) Or (nm_mask Is Nothing)) Then
            w2 = " AND IndNom LIKE '%" & nm_mask & "%'"
            wherestr = wherestr & w2
        End If
        If Not (String.IsNullOrEmpty(cd_mask) Or (cd_mask Is Nothing)) Then
            If cd_mask <> 0 Then
                w3 = " AND AmbId = '" & cd_mask & "'"
                wherestr = wherestr & w3
            End If
        End If

        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "Select Distinct IndCod, IndNom, Valor, Tendencia, Estado, Meta1, Meta2, Meta3 From f_indEfector('" & cuie & "') " & wherestr
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Indicadores")

        'contar el numero de registros de la tabla
        Dim count As Integer
        Dim x As New SqlCommand
        x.Connection = myConnection
        x.CommandText = "Select Count(*) From f_indEfector('" & cuie & "') " & wherestr
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

        Dim IndCod As String
        Dim IndNom As String
        Dim Valor As String
        Dim Tendencia As String
        Dim Estado As String
        Dim Meta1 As String
        Dim Meta2 As String
        Dim Meta3 As String


        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            IndCod = myDataSet.Tables(0).Rows(i).Item("IndCod").ToString()
            IndNom = myDataSet.Tables(0).Rows(i).Item("IndNom").ToString()
            Valor = myDataSet.Tables(0).Rows(i).Item("Valor").ToString()
            Tendencia = myDataSet.Tables(0).Rows(i).Item("Tendencia").ToString()
            Estado = myDataSet.Tables(0).Rows(i).Item("Estado").ToString()
            Meta1 = myDataSet.Tables(0).Rows(i).Item("Meta1").ToString()
            Meta2 = myDataSet.Tables(0).Rows(i).Item("Meta2").ToString()
            Meta3 = myDataSet.Tables(0).Rows(i).Item("Meta3").ToString()


            Response.Write("<row id='" & IndCod & "-" & IndNom & "'>")
            Response.Write("<cell>" & IndCod & "</cell>")
            Response.Write("<cell>" & IndNom & "</cell>")
            Response.Write("<cell>" & Valor & "</cell>")

            If Tendencia = "" Then
                Response.Write("<cell><![CDATA[<font color='gray'>Sin Datos</font>]]></cell>")
            ElseIf Tendencia = 2 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/down.png"" />]]></cell>")
            ElseIf Tendencia = 1 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/eq.png"" />]]></cell>")
            ElseIf Tendencia = 0 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/up.png"" />]]></cell>")
            End If

            If Estado = "" Then
                Response.Write("<cell><![CDATA[<font color='gray'>Sin Datos</font>]]></cell>")
            ElseIf Estado = 0 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/red.png"" />]]></cell>")
            ElseIf Estado = 1 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/orange.png"" />]]></cell>")
            ElseIf Estado = 2 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/yellow.png"" />]]></cell>")
            ElseIf Estado = 3 Then
                Response.Write("<cell><![CDATA[<img src=""css/img/green.png"" />]]></cell>")
            End If

            Response.Write("<cell>" & Meta1 & "</cell>")
            Response.Write("<cell>" & Meta2 & "</cell>")
            Response.Write("<cell>" & Meta3 & "</cell>")

            Response.Write("</row>")
        Next
        Response.Write("</rows>")


    End Sub

End Class