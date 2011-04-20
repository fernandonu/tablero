Imports System.Data
Imports System.Data.SqlClient

Public Class Indicador
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)
    Dim myCommand As New SqlCommand


    Public Function AgregarPeriodo(ByVal indcod As String, ByVal perid As String, ByVal m1 As String, ByVal m2 As String, ByVal m3 As String) As Boolean

        myCommand.Connection = myConnection
        myCommand.CommandText = "Insert Into Periodo (IndCod, PerId, PerMeta1, PerMeta2, PerMeta3) " & _
        "Values (" & indcod & ", " & perid & ", " & m1 & ", " & m2 & ", " & m3 & ")"

        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()

        Return True

    End Function
End Class
