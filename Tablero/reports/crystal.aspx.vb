Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Public Class crystal
    Inherits System.Web.UI.Page

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If Not IsPostBack Then
    '        bindReport()
    '        CrystalReportViewer1.ReportSource = Session("Report")
    '        CrystalReportViewer1.DataBind()
    '        CrystalReportViewer1.Visible = True
    '    Else
    '        CrystalReportViewer1.ReportSource = Session("Report")
    '        'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.ServerName = "NACER-CHACO\UGSP"
    '        'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.DatabaseName = "Nacer_chaco"
    '        'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.UserID = "lea"
    '        'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.Password = "guga"
    '        'CrystalReportViewer1.ReuseParameterValuesOnRefresh = True
    '    End If
    'End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            bindReport()
            CrystalReportViewer1.ReportSource = Session("Report")
            CrystalReportViewer1.DataBind()
            CrystalReportViewer1.Visible = True
        Else
            CrystalReportViewer1.ReportSource = Session("Report")
            'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.ServerName = "NACER-CHACO\UGSP"
            'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.DatabaseName = "Nacer_chaco"
            'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.UserID = "lea"
            'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.Password = "guga"
            'CrystalReportViewer1.ReuseParameterValuesOnRefresh = True
        End If
    End Sub

    Private Sub bindReport()



        Dim x As New ReportDocument
        Select Case Context.Request("rep")
            Case "0"
                x.Load(Server.MapPath("Ranking Completo.rpt"))

            Case "14"
                x.Load(Server.MapPath("Ranking Completo Zonas.rpt"))

            Case "1" 'Embarazadas (Parametros desde y hasta)
                Dim desde As Integer = Context.Request("desde")
                Dim hasta As Integer = Context.Request("hasta")
                x.Load(Server.MapPath("Embarazadas.rpt"))
                x.SetParameterValue("edadmin", desde)
                x.SetParameterValue("edadmax", hasta)

            Case "2" 'Cargas (Sin parametros)
                x.Load(Server.MapPath("detalle de carga ipos.rpt"))

            Case "3" 'Facturacion (Parametros año, cuie y nombre del efector)
                Dim ano As Integer = Context.Request("ano")
                Dim cuie As String = Context.Request("cuie")
                Dim efec As String = Context.Request("efec")

                x.Load(Server.MapPath("Facturacion.rpt"))

                x.SetParameterValue("param_ano", ano)
                x.SetParameterValue("param_cuie", cuie)
                x.SetParameterValue("Efector", efec)

            Case "4"
                x.Load(Server.MapPath("eVOLUCION DE BENEFICIARIOS por cap.rpt"))

            Case "5"
                x.Load(Server.MapPath("porcentaje de controles por efectorl.rpt"))

            Case "11" 'Indicadores (Parametros cuie)
                Dim cuie As String = Context.Request("cuie")

                x.Load(Server.MapPath("Indicadores por Efector.rpt"))

                x.SetParameterValue("cuie", cuie)

            Case "12" 'Practicas Habilitadas (Parametros cuie)
                Dim cuie As String = Context.Request("cuie")
                cuie = "'" & cuie & "'"

                x.Load(Server.MapPath("Prestaciones X Efector.rpt"))

                x.SetParameterValue("cuie", cuie)

            Case "13" 'Padron activos (Parametros mes)
                Dim mes As String = Context.Request("mes")
                mes = "'" & mes & "'"

                x.Load(Server.MapPath("Padron Activos con B recategorizado.rpt"))

                x.SetParameterValue("mes_año", mes)
        End Select


        x.SetDatabaseLogon("lea", "guga", "NACER-CHACO\UGSP", "Nacer_chaco")

        'setConnInfo(x.Database.Tables)

        Session("Report") = x

        'CrystalReportViewer1.ReportSource = Session("Report")


    End Sub

    Private Sub setConnInfo(ByVal tables As Tables)
        Dim connInfo As New ConnectionInfo
        connInfo.ServerName = "NACER-CHACO\UGSP"
        connInfo.DatabaseName = "Nacer_chaco"
        connInfo.UserID = "lea"
        connInfo.Password = "guga"

        For Each table As CrystalDecisions.CrystalReports.Engine.Table In tables
            Dim tablelogoninfo As TableLogOnInfo = table.LogOnInfo
            tablelogoninfo.ConnectionInfo = connInfo
            table.ApplyLogOnInfo(tablelogoninfo)
        Next
    End Sub

End Class