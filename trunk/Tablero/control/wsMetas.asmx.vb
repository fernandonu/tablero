Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la siguiente línea.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class wsMetas
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function AgregarPeriodo() As Boolean

        Dim m1 As String = Context.Request.Form("m1")
        Dim m2 As String = Context.Request.Form("m2")
        Dim m3 As String = Context.Request.Form("m3")
        Dim indcod As String = Context.Request.Form("indcod")
        Dim perid As String = Context.Request.Form("perid")


        Dim ind As New Indicador

        ind.AgregarPeriodo(indcod, perid, m1, m2, m3)

        Context.Response.ContentType = "text/plain"
        Context.Response.Write("Se han actualizado las metas para el período.")

        Return True
    End Function

End Class