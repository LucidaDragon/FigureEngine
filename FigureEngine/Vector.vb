Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class Vector
    Implements ISerialize

    Public Property X As Double
    Public Property Y As Double

    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property Angle As Double
        Get
            Return Math.Atan2(Y, X) * 180 / Math.PI
        End Get
    End Property

    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property Magnitude As Double
        Get
            Return Math.Sqrt((X ^ 2) + (Y ^ 2))
        End Get
    End Property

    Public Property FullName As String Implements ISerialize.FullName
        Get
            Return [GetType]().FullName
        End Get
        Set(value As String)
        End Set
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
        Return New Point(CType(a.X + b.X, Integer), CType(a.Y + b.Y, Integer))
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
        Return New Size(CType(obj.X, Integer), CType(obj.Y, Integer))
    End Operator

    Public Shared Narrowing Operator CType(obj As Vector) As Point
        Return New Point(CType(obj.X, Integer), CType(obj.Y, Integer))
    End Operator

    Public Shared Narrowing Operator CType(obj As Vector) As PointF
        Return New PointF(CType(obj.X, Integer), CType(obj.Y, Integer))
    End Operator

    Public Shared Function DotProduct(a As Vector, b As Vector) As Double
        Return (a.X * b.X) + (a.Y + b.Y)
    End Function

    Public Shared Function AngleTo(a As Vector, b As Vector) As Double
        Return 180 * (Math.Acos(DotProduct(a, b) / (a.Magnitude * b.Magnitude)) / Math.PI)
    End Function

    Public Function ToJsonString() As String Implements ISerialize.ToJsonString
        Return Json.Serialize(Me)
    End Function

    Public Function FromJsonString(str As String) As ISerialize Implements ISerialize.FromJsonString
        Return Json.Deserialize(Of Vector)(str)
    End Function
End Class