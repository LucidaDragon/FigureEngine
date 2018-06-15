Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class MetaGraphic
    Implements IDrawable
    Implements ISerialize
    Implements IGraphic

    Public Property Frames As New List(Of MetaFrame)
    Public Property Length As Double = 1
    Public Property Position As Double
        Get
            Return pos
        End Get
        Set(value As Double)
            pos = Math.Max(0, Math.Min(Length, value))
        End Set
    End Property
    Private pos As Double = 0
    Public Property Location As Point

    Public Property FullName As String Implements ISerialize.FullName
        Get
            Return [GetType]().FullName
        End Get
        Set(value As String)
        End Set
    End Property

    <ComponentModel.Browsable(False)>
    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property CurrentData As List(Of MetaData)
        Get
            If Frames.Count > 0 Then
                If Frames(CType((Position / Length) * (Frames.Count - 1), Integer)).Data IsNot Nothing Then
                    Return Frames(CType((Position / Length) * (Frames.Count - 1), Integer)).Data
                End If
            End If
            Return New List(Of MetaData)
        End Get
    End Property

    <ComponentModel.Browsable(False)>
    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property CurrentFrame As Bitmap
        Get
            If Frames.Count > 0 Then
                If Frames(CType((Position / Length) * (Frames.Count - 1), Integer)).Image IsNot Nothing Then
                    Return Frames(CType((Position / Length) * (Frames.Count - 1), Integer)).Image
                End If
            End If
            Return New Bitmap(1, 1)
        End Get
    End Property

    <Web.Script.Serialization.ScriptIgnore>
    Public ReadOnly Property Type As GraphicType Implements IGraphic.Type
        Get
            Return GraphicType.MetaGraphic
        End Get
    End Property

    Sub New()
    End Sub

    Sub New(frames As List(Of MetaFrame), length As Double, location As Point)
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
            If Frames(CType((Position / Length) * (Frames.Count - 1), Integer)).Image IsNot Nothing Then
                g.DrawImage(Frames(CType((Position / Length) * (Frames.Count - 1), Integer)).Image, Location + offset)
            End If
        End If
    End Sub

    Public Function ToJsonString() As String Implements ISerialize.ToJsonString
        Return Json.Serialize(Me)
    End Function

    Public Function FromJsonString(str As String) As ISerialize Implements ISerialize.FromJsonString
        Return Json.Deserialize(Of MetaGraphic)(str)
    End Function

    Public Class MetaFrame
        <Web.Script.Serialization.ScriptIgnore>
        Public Property Image As Bitmap
            Get
                Return ImageData
            End Get
            Set(value As Bitmap)
                ImageData = value
            End Set
        End Property
        Public Property ImageData As New BitmapData
        Public Property Data As New List(Of MetaData)

        Sub New()
        End Sub

        Sub New(image As BitmapData, data As List(Of MetaData))
            Me.Image = image
            Me.Data = data
        End Sub

        Public Function GetValue(name As String) As Vector
            For Each dat In Data
                If dat.Name = name Then
                    Return dat.Data
                End If
            Next
            Return Nothing
        End Function
    End Class

    Public Class MetaData
        Public Property Name As String
        Public Property Data As New Point

        Sub New()
        End Sub

        Sub New(name As String, data As Point)
            Me.Name = name
            Me.Data = data
        End Sub
    End Class
End Class
