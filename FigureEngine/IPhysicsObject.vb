Option Strict On
Option Explicit On

Public Interface IPhysicsObject
    Sub Tick(deltaTime As Double, objects As List(Of IPhysicsObject))
    Property Bounds As BoundingBox
    Property LinearVelocity As Vector
    Property AngularVelocity As Vector
End Interface
