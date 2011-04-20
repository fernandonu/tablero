Public Partial Class users
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim usr As New Usuario(Me.txtUsuario.Text, Me.txtPassword.Text, 1, Me.txtNya.Text, Me.ddlAreas.SelectedValue)
        usr.agregarUsuario()
    End Sub
End Class