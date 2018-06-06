Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class StaticGraphic
    Implements IDrawable
    Implements ISerialize
    Implements IGraphic

    Public Property Image As BitmapData
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
            Return GraphicType.StaticGraphic
        End Get
    End Property

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

    Public Function ToJsonString() As String Implements ISerialize.ToJsonString
        Return Json.Serialize(Me)
    End Function

    Public Function FromJsonString(str As String) As ISerialize Implements ISerialize.FromJsonString
        Return Json.Deserialize(Of StaticGraphic)(str)
    End Function
End Class
