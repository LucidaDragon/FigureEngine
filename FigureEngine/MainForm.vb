Public Class MainForm
    Public Property Display As Bitmap
        Get
            Return ScaleBox.Image
        End Get
        Set(value As Bitmap)
            ScaleBox.Image = value
            ScaleBox.Invalidate()
            Graphics = Graphics.FromImage(Display)
        End Set
    End Property
    Private Graphics As Graphics

    Public Property Threads As New List(Of EngineThread)
    Public Property Layers As New List(Of Bitmap)
    Public Property Resolution As New Point(800, 600)

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

    Public Sub InitViewport(Optional width As Integer = 800, Optional height As Integer = 600)
        For Each thread In Threads
            thread.CancelAsync()
        Next
        Threads.Clear()
        Layers.Clear()
        Resolution = New Point(width, height)
        Display = New Bitmap(Resolution.X, Resolution.Y)
    End Sub

    Public Function AddLayer() As EngineThread
        Threads.Add(New EngineThread(Threads.Count, 16))
        Layers.Add(New Bitmap(Resolution.X, Resolution.Y))
        AddHandler Threads.Last.UpdateUI, AddressOf UpdateLayer
        Threads.Last.RunWorkerAsync(Resolution)
        Return Threads.Last
    End Function

    Public Sub UpdateLayer(data As Bitmap, layer As Integer)
        Layers.Item(layer) = data
    End Sub

    Public Sub CombineLayers()
        For i As Integer = 0 To Layers.Count - 1
            Graphics.DrawImage(Layers(i), 0, 0)
        Next
        ScaleBox.Invalidate()
    End Sub
End Class