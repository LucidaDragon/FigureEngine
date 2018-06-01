Public Class Engine
    Public Property PhysicsObjects As New List(Of IPhysicsObject)
    Public Property RenderObjects As New List(Of IDrawable)

    <Web.Script.Serialization.ScriptIgnore>
    Public Property RenderSurface As Bitmap
        Get
            Return surface
        End Get
        Set(value As Bitmap)
            surface = value
            Graphics = Graphics.FromImage(surface)
        End Set
    End Property
    Private surface As Bitmap

    Private Graphics As Graphics

    Sub New()
    End Sub

    Sub New(width As Integer, height As Integer)
        RenderSurface = New Bitmap(width, height)
    End Sub

    Public Sub AddObject(obj As Object)
        If TypeOf obj Is IPhysicsObject Then
            PhysicsObjects.Add(obj)
        End If
        If TypeOf obj Is IDrawable Then
            RenderObjects.Add(obj)
        End If
    End Sub

    Public Sub Tick(deltaTime As Double)
        For Each obj In PhysicsObjects
            obj.Tick(deltaTime, PhysicsObjects)
        Next
    End Sub

    Public Sub Draw(deltaTime As Double)
        Graphics.Clear(Color.Transparent)
        For Each obj In RenderObjects
            obj.Draw(Graphics, Camera.Offset, deltaTime)
        Next
    End Sub
End Class
