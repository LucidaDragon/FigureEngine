Public Class Vector
    Public Property X As Double
    Public Property Y As Double

    Public ReadOnly Property Magnitude As Double
        Get
            Return Math.Sqrt((X ^ 2) + (Y ^ 2))
        End Get
    End Property

    Sub New()
    End Sub

    Sub New(x As Double, y As Double)
        Me.X = x
        Me.Y = y
    End Sub

    Sub New(obj As Point)
        X = obj.X
        Y = obj.Y
    End Sub

    Sub New(obj As PointF)
        X = obj.X
        Y = obj.Y
    End Sub

    Sub New(length As Integer, angle As Double)
        X = Math.Sin((angle / 180) * Math.PI) * length
        Y = Math.Cos((angle / 180) * Math.PI) * length
    End Sub

    Sub New(start As Point, finish As Point)
        X = finish.X - start.X
        Y = finish.Y - start.Y
    End Sub

    Sub New(start As PointF, finish As PointF)
        X = finish.X - start.X
        Y = finish.Y - start.Y
    End Sub

    Public Shared Operator +(a As Vector, b As Vector) As Vector
        Return New Vector(a.X + b.X, a.Y + b.Y)
    End Operator

    Public Shared Operator +(a As Point, b As Vector) As Point
        Return New Point(a.X + b.X, a.Y + b.Y)
    End Operator

    Public Shared Operator -(obj As Vector) As Vector
        Return New Vector(-obj.X, -obj.Y)
    End Operator

    Public Shared Operator -(a As Vector, b As Vector) As Vector
        Return New Vector(a.X - b.X, a.Y - b.Y)
    End Operator

    Public Shared Operator *(a As Double, b As Vector) As Vector
        Return New Vector(a * b.X, a * b.Y)
    End Operator

    Public Shared Operator *(a As Vector, b As Double) As Vector
        Return New Vector(b * a.X, b * a.Y)
    End Operator

    Public Shared Widening Operator CType(obj As Point) As Vector
        Return New Vector(obj.X, obj.Y)
    End Operator

    Public Shared Widening Operator CType(obj As PointF) As Vector
        Return New Vector(obj.X, obj.Y)
    End Operator

    Public Shared Narrowing Operator CType(obj As Vector) As Size
        Return New Size(obj.X, obj.Y)
    End Operator

    Public Shared Narrowing Operator CType(obj As Vector) As Point
        Return New Point(obj.X, obj.Y)
    End Operator

    Public Shared Narrowing Operator CType(obj As Vector) As PointF
        Return New PointF(obj.X, obj.Y)
    End Operator

    Public Shared Function DotProduct(a As Vector, b As Vector) As Double
        Return (a.X * b.X) + (a.Y + b.Y)
    End Function

    Public Shared Function Angle(a As Vector, b As Vector) As Double
        Return 180 * (Math.Acos(DotProduct(a, b) / (a.Magnitude * b.Magnitude)) / Math.PI)
    End Function
End Class
