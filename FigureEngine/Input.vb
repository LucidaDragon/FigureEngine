Public Class Input
    Public Shared PressedKeys As New List(Of Keys)

    Public Shared Sub KeyDown(key As Keys)
        PressedKeys.Add(key)
    End Sub

    Public Shared Sub KeyUp(key As Keys)
        While PressedKeys.Remove(key)
        End While
    End Sub
End Class
