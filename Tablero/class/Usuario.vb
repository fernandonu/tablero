Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Usuario
    Private usrName As String
    Private usrPwd As String
    Private usrNiv As Integer
    Private usrNya As String
    Private AreId As String

    Dim myCommand As New SqlCommand
    Dim myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnectionString").ConnectionString)

    Public Sub New(ByVal name As String, ByVal pwd As String, ByVal niv As Integer, ByVal nya As String, ByVal area As String)
        usrName = name
        usrPwd = generarClaveSHA1(pwd)
        usrNiv = niv
        usrNya = nya
        AreId = area
    End Sub

    Public Function agregarUsuario() As Boolean
        Dim x As Integer
        myCommand.Connection = myConnection
        myCommand.CommandText = "Insert Into Usuario (UsrId, UsrPwd, UsrLev, UsrNom, AreId)" & _
        " Values ('" & usrName & "', '" & usrPwd & "', '" & usrNiv & "', '" & usrNya & "', '" & AreId & "')"
        myConnection.Open()
        x = myCommand.ExecuteNonQuery()
        myConnection.Close()
        If x > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function modificarPassword(ByVal password As String, ByVal oldpassword As String) As Boolean
        myCommand.Connection = myConnection
        Dim x As String

        myCommand.CommandText = "Select UsrPwd From Usuario Where UsrId ='" & usrName & "'"
        myConnection.Open()
        x = myCommand.ExecuteScalar()
        myConnection.Close()

        If x = oldpassword Then
            myCommand.CommandText = "Update Usuario Set UsrPwd = '" & _
            generarClaveSHA1(password) & "', UsrNom='" & usrNya & "' Where UsrId = '" & usrName & "'"

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub eliminarUsuario()
        myCommand.Connection = myConnection
        myCommand.CommandText = "Delete From Usuario" & _
        " Where UsrId = '" & usrName & "'"
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Sub

    Public Function generarClaveSHA1(ByVal nombre As String) As String
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


End Class
