Option Strict On
Option Explicit On

Public Class Engine
    Public Property PhysicsObjects As New List(Of IPhysicsObject)
    Public Property RenderObjects As New List(Of IDrawable)
    Public Property Layer As Integer = 0

    <Web.Script.Serialization.ScriptIgnore>
    Public Property RenderSurface As BitmapData
        Get
            Return surface
        End Get
        Set(value As BitmapData)
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

    Public Event UpdateUI(img As BitmapData, layer As Integer)

    Public Sub AddObject(obj As Object, Optional drawOnly As Boolean = False, Optional physicsOnly As Boolean = False)
        If TypeOf obj Is IPhysicsObject And Not drawOnly Then
            PhysicsObjects.Add(CType(obj, IPhysicsObject))
        End If
        If TypeOf obj Is IDrawable And Not physicsOnly Then
            RenderObjects.Add(CType(obj, IDrawable))
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
        RaiseEvent UpdateUI(RenderSurface, Layer)
    End Sub
End Class
