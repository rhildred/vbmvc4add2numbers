Public Class HomeController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Home

    Function Index() As ActionResult
        Dim add = New Add()
        add.num1 = 0
        add.num2 = 0
        Try
            add.num1 = Integer.Parse(Request("number1"))
            add.num2 = Integer.Parse(Request("number2"))
        Catch ex As Exception
            ' ignore error
        End Try
        Return View(add)
    End Function

End Class