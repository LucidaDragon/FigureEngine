Public Class DynamicBackground
    Implements IDrawable

    Public Property Frames As New List(Of BitmapData)
    Public Property Length As Double
    Public Property Position As Double = 0
    Public Property Location As Point

    Sub New()
    End Sub

    Sub New(frames As List(Of BitmapData), length As Double, location As Point)
        Me.Frames = frames
        Me.Length = length
        Me.Location = location
    End Sub

    Public Sub Draw(g As Graphics, offset As Vector, deltaTime As Double) Implements IDrawable.Draw
        Position += Math.Max(deltaTime / 1000, Double.Epsilon)
        While Position > Length
            Position -= Length
        End While

        g.DrawImage(Frames((Position / Length) * (Frames.Count - 1)), Location + offset)
    End Sub
End Class
