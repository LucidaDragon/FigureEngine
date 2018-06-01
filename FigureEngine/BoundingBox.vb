Option Strict On
Option Explicit On

Public Class BoundingBox
    Public Property SerializeBounds As EngineRectangle
        Get
            Return Bounds
        End Get
        Set(value As EngineRectangle)
            Bounds = value
            cache = Nothing
        End Set
    End Property
    Private Bounds As EngineRectangle

    Public Property Axis As Point
        Get
            Return bAxis
        End Get
        Set(value As Point)
            bAxis = value
            cache = Nothing
        End Set
    End Property
    Private bAxis As Point

    Public Property Angle As Double
        Get
            Return bAngle
        End Get
        Set(value As Double)
            bAngle = value
            cache = Nothing
        End Set
    End Property
    Private bAngle As Double

    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property RotatedRectangle As Point()
        Get
            If cache Is Nothing Then
                cache = {Rotate(Bounds.Points(0), Axis, Angle), Rotate(Bounds.Points(1), Axis, Angle), Rotate(Bounds.Points(2), Axis, Angle), Rotate(Bounds.Points(3), Axis, Angle)}
            End If
            Return cache
        End Get
    End Property
    Private cache As Point()

    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property BoundingSides As List(Of Point())
        Get
            Return New List(Of Point()) From {{RotatedRectangle(0), RotatedRectangle(1)}.ToArray(), {RotatedRectangle(1), RotatedRectangle(2)}.ToArray(), {RotatedRectangle(2), RotatedRectangle(3)}.ToArray(), {RotatedRectangle(3), RotatedRectangle(0)}.ToArray()}
        End Get
    End Property

    Public Sub DrawBoundBox(g As Graphics, offset As Vector, pen As Pen)
        For Each elem In BoundingSides
            g.DrawLine(pen, elem(0) + offset, elem(1) + offset)
        Next
    End Sub

    Public Function IsIntersecting(box As BoundingBox) As Boolean
        For Each lineA In BoundingSides
            For Each lineB In box.BoundingSides
                If LineLib.IsIntersecting(lineA(0), lineB(0), lineA(1), lineB(1)) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Private Function Rotate(point As Point, center As Point, angle As Double) As Point
        Return New Point(CInt(((point.X - center.X) * Math.Cos(Math.PI * (angle / 180))) - ((point.Y - center.Y) * Math.Sin(Math.PI * (angle / 180)))) + center.X, CInt(((point.Y - center.Y) * Math.Cos(Math.PI * (angle / 180))) + ((point.X - center.X) * Math.Sin(Math.PI * (angle / 180)))) + center.Y)
    End Function
End Class
