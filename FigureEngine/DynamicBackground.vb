Option Strict On
Option Explicit On

Public Class DynamicBackground
    Implements IDrawable
    Implements ISerialize

    Public Property Frames As New List(Of BitmapData)
    Public Property Length As Double
    Public Property Position As Double = 0
    Public Property Location As Point

    Public Property FullName As String Implements ISerialize.FullName
        Get
            Return [GetType]().FullName
        End Get
        Set(value As String)
        End Set
    End Property

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

        g.DrawImage(Frames(CType((Position / Length) * (Frames.Count - 1), Integer)), Location + offset)
    End Sub

    Public Function ToJsonString() As String Implements ISerialize.ToJsonString
        Return Json.Serialize(Me)
    End Function

    Public Function FromJsonString(str As String) As ISerialize Implements ISerialize.FromJsonString
        Return Json.Deserialize(Of DynamicBackground)(str)
    End Function
End Class
