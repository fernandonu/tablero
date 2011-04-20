Partial Public Class monitor
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim sqlstring As String
        'Dim myCommand As New SqlCommand
        'myCommand.Connection = myConnection

        'Dim myAdapter As SqlDataAdapter
        'Dim myDataSet As DataSet

        'sqlstring = "exec recibidos"
        'myAdapter = New SqlDataAdapter(sqlString, myConnection)
        'myDataSet = New DataSet
        'myAdapter.Fill(myDataSet, "Ipos")


        ''Niños
        'TP.Text = myDataSet.Tables("Ipos").Rows(0).Item(1)
        'TE.Text = myDataSet.Tables("Ipos").Rows(0).Item(1) * 0.3
        'TR.Text = myDataSet.Tables("Ipos").Rows(0).Item(2) * 0.3

        ''Embarazadas
        'EP.Text = myDataSet.Tables("Ipos").Rows(1).Item(1)
        'EE.Text = myDataSet.Tables("Ipos").Rows(1).Item(1) * 0.3
        'ER.Text = myDataSet.Tables("Ipos").Rows(1).Item(2) * 0.3


    End Sub

End Class