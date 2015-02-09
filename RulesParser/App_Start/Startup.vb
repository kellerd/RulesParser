Imports Microsoft.Owin
Imports Owin
Imports System.Web
Imports Microsoft.AspNet.SignalR
Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        app.MapSignalR()
    End Sub
End Class
Namespace SignalRChat
    Public Class ChatHub
        Inherits Hub
        Public Sub Send(bulkRules As String, pattern As String)
            ' Call the addNewMessageToPage method to update clients.
            Clients.Client(MyBase.Context.ConnectionId).addNewMessageToPage(bulkRules, pattern)
        End Sub
    End Class
End Namespace