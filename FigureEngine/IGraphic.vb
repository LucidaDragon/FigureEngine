Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Interface IGraphic
    Inherits IDrawable

    ReadOnly Property Type As GraphicType
End Interface
