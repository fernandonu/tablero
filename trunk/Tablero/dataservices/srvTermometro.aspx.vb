Public Partial Class srvTermometro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindChart()
        End If
    End Sub

    Private Sub bindChart()

        Dim nro As String = Context.Request("nro")
        Dim m1 As String = Context.Request("ma")
        Dim m2 As String = Context.Request("mb")
        Dim m3 As String = Context.Request("mc")
        Dim Limite As String

        If (nro > 100) Then
            Limite = nro
        ElseIf (m3 >= 100) Then
            Limite = m3 + (m3 - m2)
        Else
            Limite = 100
        End If
        'Create FusionCharts XML
        Response.ContentType = "text/xml"

        'Create chart element
        Response.Write("<Chart bgColor='FFFFFF' bgAlpha='0' showBorder='0' upperLimit='" & Limite & "' lowerLimit='0' gaugeRoundRadius='5' chartBottomMargin='0' ticksBelowGauge='1' showGaugeLabels='0' valueAbovePointer='1' pointerOnTop='0' pointerRadius='9' gaugeFillMix='{light-10},{light-20},{light-60},{dark-30},{dark-40}, {dark-40}' gaugeFillRatio='' >")

        Response.Write("<colorRange>")
        Response.Write("<color minValue='0' maxValue='" & m1 & "' code='FF654F' name='BAD'  />")
        Response.Write("<color minValue='" & m1 & "' maxValue='" & m2 & "' code='FFA500' name='WEAK' /> ")
        Response.Write("<color minValue='" & m2 & "' maxValue='" & m3 & "' code='FFFF00' name='GOOD' />")
        Response.Write("<color minValue='" & m3 & "' maxValue='" & Limite & "' code='8BBA00' name='EXCELLENT' />")
        Response.Write("</colorRange>")

        Response.Write("<value>" & nro & "</value>")

        Response.Write("<styles>")
        Response.Write("<definition>")
        Response.Write("<style name='ValueFont' type='Font' bgColor='333333' size='10' color='FFFFFF'/>")
        Response.Write("</definition>")
        Response.Write("<application>")
        Response.Write("<apply toObject='VALUE' styles='valueFont'/>")
        Response.Write("</application>")
        Response.Write("</styles>")


        'Close chart element
        Response.Write("</Chart>")


    End Sub
End Class