Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Drawing
Imports System.Text


Partial Public Class login
    Inherits System.Web.UI.Page
    Dim myCommand As New SqlCommand
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtUsuario.Focus()
    End Sub

    Private Sub btnLogin_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnLogin.Command
        Dim aceptado As Boolean = False
        Dim claveSHA As String = Me.generarClaveSHA1(Me.txtPassword.Text)
        aceptado = verificarusuario(Me.txtUsuario.Text, claveSHA)

        If aceptado Then
            'ActualizarIndicadores("11")
            'ActualizarIndicadores("12")
            'ActualizarIndicadores("13")
            'ActualizarIndicadores("14")
            'ActualizarIndicadores("15")
            'ActualizarIndicadores("17")
            'ActualizarIndicadores("46")
            FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, False)
        Else
            lblerror.Text = "El nombre de usuario o la contraseña son incorrectos."
        End If

    End Sub
    Private Function ActualizarIndicadores(ByVal Ind As String) As Boolean

        myCommand.CommandText = "actualizarindicadores" & Ind
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.CommandTimeout = 1500
        myCommand.Connection = myConnection

        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Function

    Private Function verificarusuario(ByVal usr As String, ByVal pwd As String) As Boolean
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim sqlString As String

        sqlString = "Select * From Usuario Where UsrId = '" & usr & "' and UsrPwd = '" & pwd & "'"
        da = New SqlDataAdapter(sqlString, myConnection)
        myConnection.Open()
        da.Fill(ds, "Usuario")
        myConnection.Close()


        If ds.Tables("Usuario").Rows.Count = 0 Then

            Session.Add("deptNombre", "No ha iniciado una sesión válida")
            Return False

        Else

            Session.Add("usr", usr)

            Session.Add("usrlvl", ds.Tables("Usuario").Rows(0).Item("UsrLev"))

            Session.Add("nombreusuario", ds.Tables("Usuario").Rows(0).Item("UsrNom"))

            Session.Add("area", ds.Tables("Usuario").Rows(0).Item("AreId"))

            Return True

        End If

    End Function

    Private Function generarClaveSHA1(ByVal nombre As String) As String
        ' Crear una clave SHA1 como la generada por 
        ' FormsAuthentication.HashPasswordForStoringInConfigFile
        ' Adaptada del ejemplo de la ayuda en la descripción de SHA1 (Clase)
        Dim enc As New UTF8Encoding
        Dim data() As Byte = enc.GetBytes(nombre)
        Dim result() As Byte

        Dim sha As New SHA1CryptoServiceProvider
        ' This is one implementation of the abstract class SHA1.
        result = sha.ComputeHash(data)
        '
        ' Convertir los valores en hexadecimal
        ' cuando tiene una cifra hay que rellenarlo con cero
        ' para que siempre ocupen dos dígitos.
        Dim sb As New StringBuilder
        For i As Integer = 0 To result.Length - 1
            If result(i) < 16 Then
                sb.Append("0")
            End If
            sb.Append(result(i).ToString("x"))
        Next
        '
        Return sb.ToString.ToUpper
    End Function


    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click

    End Sub
End Class