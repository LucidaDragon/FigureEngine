Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Interface IDrawable
    Sub Draw(g As Graphics, offset As Vector, deltaTime As Double)
End Interface
