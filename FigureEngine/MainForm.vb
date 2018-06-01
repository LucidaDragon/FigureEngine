Public Class MainForm
    Public Property Display As Bitmap
        Get
            Return ScaleBox.Image
        End Get
        Set(value As Bitmap)
            ScaleBox.Image = value
            ScaleBox.Invalidate()
        End Set
    End Property

    Public Property Threads As New List(Of EngineThread) 'Add generation of engine threads.

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Input.KeyDown(e.KeyCode)
    End Sub

    Private Sub MainForm_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Input.KeyUp(e.KeyCode)
    End Sub

    Private Sub MainForm_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then
            Input.KeyDown(Keys.LButton)
        ElseIf e.Button = MouseButtons.Right Then
            Input.KeyDown(Keys.RButton)
        End If
    End Sub

    Private Sub MainForm_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = MouseButtons.Left Then
            Input.KeyUp(Keys.LButton)
        ElseIf e.Button = MouseButtons.Right Then
            Input.KeyUp(Keys.RButton)
        End If
    End Sub
End Class