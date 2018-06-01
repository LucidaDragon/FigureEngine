Option Strict On
Option Explicit On

Public Class StaticBackground
    Implements IDrawable

    Public Property Image As BitmapData
    Public Property Location As Point

    Sub New()
    End Sub

    Sub New(image As Bitmap, location As Point)
        Me.Image = image
        Me.Location = location
    End Sub

    Public Sub Draw(g As Graphics, offset As Vector, deltaTime As Double) Implements IDrawable.Draw
        g.DrawImage(Image, Location + offset)
    End Sub

    Sub New(image As BitmapData, location As Point)
        Me.Image = image
        Me.Location = location
    End Sub
End Class
