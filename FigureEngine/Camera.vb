Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class Camera
    Public Shared Property Offset As New Vector
    Public Shared Property Speed As Integer = 10

    Public Shared Sub MoveCamera(x As Integer, y As Integer)
        Offset.X -= x * Speed
        Offset.Y += y * Speed
    End Sub
End Class
