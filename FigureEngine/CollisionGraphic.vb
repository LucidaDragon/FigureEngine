Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class CollisionGraphic
    Implements IDrawable
    Implements ICollide

    Public Property Graphic As IGraphic
    Public Property Bounds As BoundingBox

    Private ReadOnly Property Collision As BoundingBox() Implements ICollide.Collision
        Get
            Return {Bounds}
        End Get
    End Property

    Public Sub Draw(g As Graphics, offset As Vector, deltaTime As Double) Implements IDrawable.Draw
        Graphic.Draw(g, offset, deltaTime)
    End Sub
End Class
