Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Interface IPhysicsObject
    Sub Tick(deltaTime As Double, objects As List(Of IPhysicsObject))
    Property Bounds As List(Of BoundingBox)
    Property LinearVelocity As Vector
    Property AngularVelocity As Double
End Interface
