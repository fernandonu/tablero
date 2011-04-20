Imports System.Data
Imports System.Data.SqlClient

Partial Public Class srvExpedientes
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Select Case Request.QueryString.Get("tipo")
                Case 1
                    bindAntiguedad()
                Case 2
                    bindRechazos()
                Case 3
                    bindEfectores()
            End Select

        End If


    End Sub

    Sub bindAntiguedad()

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
        Dim w2 As String

        If Not (String.IsNullOrEmpty(nm_mask) Or (nm_mask Is Nothing)) Then
            Dim arr As Array
            arr = nm_mask.Split(",")

            For Each elemento As String In arr
                w2 += " '" & elemento.Trim(" ") & "', "
            Next

            If arr.Length < 5 Then
                For cont As Integer = arr.Length + 1 To 5
                    If cont < 5 Then
                        w2 += "'',"
                    Else
                        w2 += "''"
                    End If
                Next
            End If

        Else
            w2 = "'', '', '', '', ''"
        End If


        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "Exec antiguedadExpedientes " & w2 & ""
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Expedientes")

        'contar el numero de registros de la tabla
        Dim count As Integer = 1


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

        Dim AntExp As String
        Dim AntPra As String
        Dim AntDia As String



        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            AntExp = myDataSet.Tables(0).Rows(i).Item(0).ToString()
            AntPra = myDataSet.Tables(0).Rows(i).Item(1).ToString()
            AntDia = myDataSet.Tables(0).Rows(i).Item(2).ToString()

            If AntExp <> "" Then
                AntExp = FormatDateTime(AntExp, DateFormat.ShortDate)
                AntPra = FormatDateTime(AntPra, DateFormat.ShortDate)
            End If

            Response.Write("<row id='" & AntExp & "'>")
            Response.Write("<cell>" & AntExp & "</cell>")
            Response.Write("<cell>" & AntPra & "</cell>")
            Response.Write("<cell>" & AntDia & "</cell>")


            Response.Write("</row>")
        Next
        Response.Write("</rows>")


    End Sub

    Sub bindRechazos()

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
        Dim w2 As String

        If Not (String.IsNullOrEmpty(nm_mask) Or (nm_mask Is Nothing)) Then
            Dim arr As Array
            arr = nm_mask.Split(",")

            For Each elemento As String In arr
                w2 += " '" & elemento.Trim(" ") & "', "
            Next

            If arr.Length < 5 Then
                For cont As Integer = arr.Length + 1 To 5
                    If cont < 5 Then
                        w2 += "'',"
                    Else
                        w2 += "''"
                    End If
                Next
            End If

        Else
            w2 = "'', '', '', '', ''"
        End If


        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "Exec rechazosExpedientes " & w2 & ""
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Rechazos")

        'contar el numero de registros de la tabla
        Dim count As Integer = 1


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

        Dim Observaciones As String
        Dim Cantidad As String




        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            Observaciones = myDataSet.Tables(0).Rows(i).Item(0).ToString()
            Cantidad = myDataSet.Tables(0).Rows(i).Item(1).ToString()


            Response.Write("<row id='" & Observaciones & "'>")
            Response.Write("<cell>" & Observaciones & "</cell>")
            Response.Write("<cell>" & Cantidad & "</cell>")


            Response.Write("</row>")
        Next
        Response.Write("</rows>")


    End Sub

    Sub bindEfectores()

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
        Dim w2 As String

        If Not (String.IsNullOrEmpty(nm_mask) Or (nm_mask Is Nothing)) Then
            Dim arr As Array
            arr = nm_mask.Split(",")

            For Each elemento As String In arr
                w2 += " '" & elemento.Trim(" ") & "', "
            Next

            If arr.Length < 5 Then
                For cont As Integer = arr.Length + 1 To 5
                    If cont < 5 Then
                        w2 += "'',"
                    Else
                        w2 += "''"
                    End If
                Next
            End If

        Else
            w2 = "'', '', '', '', ''"
        End If


        'calcular la posición inicial de las filas
        Dim start As Integer = limit * page - limit
        If start < 0 Then
            start = 0
        End If

        sqlString = "Exec efectoresExpedientes " & w2 & ""
        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Efectores")

        'contar el numero de registros de la tabla
        Dim count As Integer = 1


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

        Dim cuie As String
        Dim Servicio As String




        Dim i As Integer

        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            cuie = myDataSet.Tables(0).Rows(i).Item(0).ToString()
            Servicio = myDataSet.Tables(0).Rows(i).Item(1).ToString()


            Response.Write("<row id='" & cuie & "'>")
            Response.Write("<cell>" & cuie & "</cell>")
            Response.Write("<cell>" & Servicio & "</cell>")


            Response.Write("</row>")
        Next
        Response.Write("</rows>")


    End Sub

End Class