Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class DynamicGraphic
    Implements IDrawable
    Implements ISerialize
    Implements IGraphic

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

    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property Type As GraphicType Implements IGraphic.Type
        Get
            Return GraphicType.DynamicGraphic
        End Get
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

        If Frames.Count > 0 Then
            If Frames(CType((Position / Length) * (Frames.Count - 1), Integer)) IsNot Nothing Then
                g.DrawImage(Frames(CType((Position / Length) * (Frames.Count - 1), Integer)), Location + offset)
            End If
        End If
    End Sub

    Public Function ToJsonString() As String Implements ISerialize.ToJsonString
        Return Json.Serialize(Me)
    End Function

    Public Function FromJsonString(str As String) As ISerialize Implements ISerialize.FromJsonString
        Return Json.Deserialize(Of DynamicGraphic)(str)
    End Function
End Class
