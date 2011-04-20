Imports System.Data
Imports System.Data.SqlClient

Partial Public Class beneficiarios
    Inherits System.Web.UI.Page
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString1").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim total As String
        Dim potencial As String

        Dim myCommand As New SqlCommand
        myCommand.Connection = myConnection

        myConnection.Open()
        myCommand.CommandText = "Select Count(*) From Nacer_Chaco.dbo.SMIAfiliados Where Activo = 'S'"
        total = myCommand.ExecuteScalar * 15
        myCommand.CommandText = "Select SUM(Potencial) From Nacer_Chaco.dbo.Efectores"
        potencial = myCommand.ExecuteScalar * 15
        myConnection.Close()

        'calculos estimados
        TCM.Text = "$" & total * 0.6
        CT.Text = "$" & total * 0.4
        TP.Text = "$" & total

        'calculos potencial
        TCMP.Text = "$" & potencial * 0.6
        CTP.Text = "$" & potencial * 0.4
        TPP.Text = "$" & potencial
    End Sub

End Class