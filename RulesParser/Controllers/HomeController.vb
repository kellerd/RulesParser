Imports System.Threading.Tasks
Imports System.Web.Mvc.Async
Public Class HomeController
    Inherits AsyncController

    Function Index() As ActionResult
        Return View()
    End Function

End Class
