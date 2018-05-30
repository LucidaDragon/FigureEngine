﻿Public Class EngineRectangle
    Public Property TopLeft As Vector
    Public Property TopRight As Vector
    Public Property BottomRight As Vector
    Public Property BottomLeft As Vector

    Public ReadOnly Property Points As List(Of Point)
        Get
            Return New List(Of Point) From {TopLeft + Location, TopRight + Location, BottomRight + Location, BottomLeft + Location}
        End Get
    End Property
    Public ReadOnly Property Width As Double
        Get
            Return (TopRight - TopLeft).Magnitude
        End Get
    End Property
    Public ReadOnly Property Height As Double
        Get
            Return (BottomLeft - TopLeft).Magnitude
        End Get
    End Property
    Public ReadOnly Property Center As Vector
        Get
            Return New Vector(Width / 2, Height / 2)
        End Get
    End Property
    Public ReadOnly Property VectorRectangle As EngineRectangle
        Get
            Return New EngineRectangle(New Vector, TopLeft, TopRight, BottomRight, BottomLeft)
        End Get
    End Property

    Public Property Location As Vector

    Sub New()
    End Sub

    Sub New(location As Vector, topLeft As Vector, topRight As Vector, bottomRight As Vector, bottomLeft As Vector)
        Me.Location = location
        Me.TopLeft = topLeft
        Me.TopRight = topRight
        Me.BottomRight = bottomRight
        Me.BottomLeft = bottomLeft
    End Sub

    Sub New(rect As Rectangle)
        Location = rect.Location
        TopLeft = New Vector(rect.Left, rect.Top) - Location
        TopRight = New Vector(rect.Right, rect.Top) - Location
        BottomRight = New Vector(rect.Right, rect.Bottom) - Location
        BottomLeft = New Vector(rect.Left, rect.Bottom) - Location
    End Sub

    Public Shared Function Distance(a As EngineRectangle, b As EngineRectangle)
        Return (b.Center - a.Center).Magnitude
    End Function

    Public Shared Operator +(rect As EngineRectangle, amount As Vector) As EngineRectangle
        Return New EngineRectangle(rect.Location + amount, rect.TopLeft, rect.TopRight, rect.BottomRight, rect.BottomLeft)
    End Operator

    Public Shared Widening Operator CType(rect As Rectangle) As EngineRectangle
        Return New EngineRectangle(rect)
    End Operator

    Public Shared Widening Operator CType(eRect As EngineRectangle) As Rectangle
        Return New Rectangle(eRect.Location, eRect.BottomRight)
    End Operator
End Class
