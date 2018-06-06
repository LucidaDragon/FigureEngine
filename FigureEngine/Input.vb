Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class Input
    Public Shared PressedKeys As New List(Of Keys)

    Public Shared Sub KeyDown(key As Keys)
        PressedKeys.Add(key)
    End Sub

    Public Shared Sub KeyUp(key As Keys)
        While PressedKeys.Remove(key)
        End While
    End Sub

    Public Shared Function Query(keys As Keys()) As Boolean
        For Each elem In keys
            If PressedKeys.Contains(elem) Then
                Return True
            End If
        Next
        Return False
    End Function
End Class
