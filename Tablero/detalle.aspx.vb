Imports InfoSoftGlobal
Imports InfoSoftGlobal.FusionCharts
Imports TCFlash.DataConnection
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class detalle
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)
    'Public indcod As String
    'Public graid As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            indcod.Value = Context.Request("ind")
            graid.Value = Context.Request("gra")
            Me.lblInd.Text = Context.Request("indnom")
            updateChartProv(graid.Value, indcod.Value)

            'Si me mandan un efector
            Dim cuie As String = Context.Request("cuie")
            If Not (String.IsNullOrEmpty(cuie) Or (cuie Is Nothing) Or cuie = "") Then
                updateChart(cuie, graid.Value, indcod.Value)
            End If
        End If
    End Sub


    Private Sub updateChart(ByVal id As String, ByVal graf As String, ByVal ind As String)
        If ind = "" Then
            ind = 0
        End If
        If id = "" Then
            id = 0
        End If
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet
        Dim sqlString As String
        'Get factory details depending on FactoryID from selected Radio Button
        'Dim sqlString As String = "Select IndCod, IndNom, Valor  from V_Indicadores Where IndCod = 1 or indcod = " & indcod

        If id.StartsWith("H") Then
            sqlString = "select * from (Select top 12 PerId, cast((hisnum/hisden)*100 as decimal(10,2)) as Valor From Historico" & _
            " Where indcod = " & ind & " And cuie = '" & id & "' order by perid desc) as t order by t.perid"
        ElseIf id.StartsWith("L") Then
            sqlString = "select * from (Select top 12  PerId, cast(avg(cast((hisnum/hisden) as money))*100 as decimal(10,2)) as Valor From Historico" & _
            " Where indcod = " & ind & " And cuie in (select cuie from efector where locid=" & Replace(id, "L", "") & ")" & _
            "group by  perid order by perid desc) as t order by t.perid"
        ElseIf id.StartsWith("Z") Then
            sqlString = "select * from (Select top 12  PerId, cast(avg(cast((hisnum/hisden) as money))*100 as decimal(10,2)) as Valor From Historico" & _
            " Where indcod = " & ind & " and cuie in (select cuie from efector where locid in  " & _
            "(select locid from localidad where zonid=" & Replace(id, "Z", "") & "))" & _
            "group by  perid order by perid desc) as t order by t.perid"
        Else
            sqlString = ""
        End If


        myAdapter = New SqlDataAdapter(sqlString, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Detalle")

        'Create FusionCharts XML
        Dim strXML As New StringBuilder()
        'Create chart element
        strXML.Append("<chart caption= '" & nombre.Text & "' showborder='0' bgcolor='FFFFFF' bgalpha='100' subcaption='Comportamiento historico' xAxisName='Días' yAxisName='Porcentaje' yAxisMaxValue='100' rotateLabels='1' placeValuesInside='1' rotateValues='1' >")

        Dim i As Integer
        Dim nom As String
        Dim val As String

        'Iterate through database
        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            nom = myDataSet.Tables(0).Rows(i).Item("PerId").ToString()
            val = myDataSet.Tables(0).Rows(i).Item("Valor").ToString.Replace(",", ".")


            'Create set element
            'Also set date into d/M format
            strXML.Append("<set label='" & nom & "' Value='" & val & "' />")
        Next

        myAdapter.SelectCommand.CommandText = "Select Top 1 Cast(PerMeta1 * 100 as Int) As PerMeta1, Cast(PerMeta2 * 100 as Int) As PerMeta2, Cast(PerMeta3 * 100 as Int) As PerMeta3 From Periodo Where IndCod = " & ind & " order by perid desc"
        myAdapter.Fill(myDataSet, "Metas")

        strXML.Append("<trendLines>")
        strXML.Append("<line startValue='" & myDataSet.Tables("Metas").Rows(0).Item("PerMeta1").ToString() & "' color='CC3300' displayvalue='Meta1' valueOnRight='1'/> ")
        strXML.Append("<line startValue='" & myDataSet.Tables("Metas").Rows(0).Item("PerMeta2").ToString() & "' color='003366' displayvalue='Meta2' valueOnRight='1'/> ")
        strXML.Append("<line startValue='" & myDataSet.Tables("Metas").Rows(0).Item("PerMeta3").ToString() & "' color='009900' displayvalue='Meta3' valueOnRight='1'/> ")
        strXML.Append("</trendLines>")

        strXML.Append("<styles>")

        strXML.Append("<definition>")
        strXML.Append("<style name='CanvasAnim' type='animation' param='_xScale' start='0' duration='1' />")
        strXML.Append("</definition>")

        strXML.Append("<application>")
        strXML.Append("<apply toObject='Canvas' styles='CanvasAnim' />")
        strXML.Append("</application>")

        strXML.Append("</styles>")


        'Close chart element
        strXML.Append("</chart>")
        Dim outPut As String = ""
        If IsPostBack = True Then

            'when an ajax call is made we use RenderChartHTML method
            outPut = FusionCharts.RenderChartHTML("FusionCharts/Line.swf", "", strXML.ToString(), "chart2", "460", "265", False, False)

        Else

            'When the page is loaded for the first time, we call RenderChart() method to avoid IE's 'Click here to Acrtivate...' message
            outPut = FusionCharts.RenderChart("FusionCharts/Line.swf", "", strXML.ToString(), "chart2", "460", "265", False, False)
        End If

        'Clear panel which will contain the chart
        Panel1.Controls.Clear()


        'Add Litaral control to Panel which adds the chart from outPut string
        Panel1.Controls.Add(New LiteralControl(outPut))



  


        ' close Data Reader


    End Sub

    Private Sub updateChartProv(ByVal graf As String, ByVal ind As String)
        Dim myAdapter As SqlDataAdapter
        Dim myDataSet As DataSet
        Dim sqlStringProv As String
        'Get factory details depending on FactoryID from selected Radio Button
        'Dim sqlString As String = "Select IndCod, IndNom, Valor  from V_Indicadores Where IndCod = 1 or indcod = " & indcod
        If ind = "" Then
            ind = 14
        End If

        sqlStringProv = "select * from (Select top 12 PerId, cast(sum(hisnum)/sum(hisden)*100 as decimal(10,2)) as Valor From Historico" & _
            " Where indcod = " & ind & " group by  perid order by perid desc) as t order by t.perid  "

        'Create database connection to get data for chart 

        myAdapter = New SqlDataAdapter(sqlStringProv, myConnection)
        myDataSet = New DataSet
        myAdapter.Fill(myDataSet, "Detalle")

        'Create FusionCharts XML
        Dim strXMLProv As New StringBuilder()
        'Create chart element
        strXMLProv.Append("<chart caption='Provincia del Chaco' showborder='0' bgcolor='FFFFFF' bgalpha='100' subcaption='Comportamiento historico' xAxisName='Días' yAxisName='Porcentaje' yAxisMaxValue='100' rotateLabels='1' placeValuesInside='1' rotateValues='1' >")

        Dim i As Integer
        Dim nom As String
        Dim val As String

        'Iterate through database
        For i = 0 To myDataSet.Tables(0).Rows.Count - 1
            nom = myDataSet.Tables(0).Rows(i).Item("PerId").ToString()
            val = myDataSet.Tables(0).Rows(i).Item("Valor").ToString.Replace(",", ".")


            'Create set element
            'Also set date into d/M format
            strXMLProv.Append("<set label='" & nom & "' Value='" & val & "' />")
        Next

        myAdapter.SelectCommand.CommandText = "Select Top 1 Cast(PerMeta1 * 100 as Int) As PerMeta1, Cast(PerMeta2 * 100 as Int) As PerMeta2, Cast(PerMeta3 * 100 as Int) As PerMeta3 From Periodo Where IndCod = " & ind & " order by perid desc"
        myAdapter.Fill(myDataSet, "Metas")

        strXMLProv.Append("<trendLines>")
        strXMLProv.Append("<line startValue='" & myDataSet.Tables("Metas").Rows(0).Item("PerMeta1").ToString() & "' color='CC3300' displayvalue='Meta1' valueOnRight='1' /> ")
        strXMLProv.Append("<line startValue='" & myDataSet.Tables("Metas").Rows(0).Item("PerMeta2").ToString() & "' color='003366' displayvalue='Meta2' valueOnRight='1' /> ")
        strXMLProv.Append("<line startValue='" & myDataSet.Tables("Metas").Rows(0).Item("PerMeta3").ToString() & "' color='009900' displayvalue='Meta3' valueOnRight='1' /> ")
        strXMLProv.Append("</trendLines>")

        strXMLProv.Append("<styles>")

        strXMLProv.Append("<definition>")
        strXMLProv.Append("<style name='CanvasAnim' type='animation' param='_xScale' start='0' duration='1' />")
        strXMLProv.Append("</definition>")

        strXMLProv.Append("<application>")
        strXMLProv.Append("<apply toObject='Canvas' styles='CanvasAnim' />")
        strXMLProv.Append("</application>")

        strXMLProv.Append("</styles>")


        'Close chart element
        strXMLProv.Append("</chart>")
        Dim outPut As String = ""



        'outPut will store the HTML of the chart rendered as string 

        If IsPostBack = True Then

            'when an ajax call is made we use RenderChartHTML method
            outPut = FusionCharts.RenderChartHTML("FusionCharts/Line.swf", "", strXMLProv.ToString(), "chart1", "460", "265", False, False)


        Else

            'When the page is loaded for the first time, we call RenderChart() method to avoid IE's 'Click here to Acrtivate...' message
            outPut = FusionCharts.RenderChart("FusionCharts/Line.swf", "", strXMLProv.ToString(), "chart1", "460", "265", False, False)

        End If

        'Clear panel which will contain the chart
        Panel2.Controls.Clear()


        'Add Litaral control to Panel which adds the chart from outPut string
        Panel2.Controls.Add(New LiteralControl(outPut))


        ' close Data Reader


    End Sub


    Private Sub vergraf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles vergraf.Click
        'If Me.cuie.Text.StartsWith("H") Then
        updateChartProv(graid.Value, indcod.Value)
        If Me.cuie.Text <> "" Then
            updateChart(Me.cuie.Text, graid.Value, indcod.Value)
        End If
    End Sub

End Class