Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class Animation
    Public Const WeaponAttachPointName As String = "Weap.Origin"
    Public Const WeaponAttachVector As String = "Weap.Vector"

    Public Shared Function GetWeaponPoint(graphic As MetaGraphic, frame As Integer) As Point
        If graphic.Frames(frame).GetValue(WeaponAttachPointName) IsNot Nothing Then
            Return CType(graphic.Frames(frame).GetValue(WeaponAttachPointName), Point)
        Else
            Throw New InvalidMetaGraphicException(WeaponAttachPointName & " not found.")
        End If
    End Function

    Public Shared Function GetWeaponVector(graphic As MetaGraphic, frame As Integer) As Vector
        Return graphic.Frames(frame).GetValue(WeaponAttachVector) - GetWeaponPoint(graphic, frame)
    End Function

    Public Class InvalidMetaGraphicException
        Inherits Exception

        Sub New()
            MyBase.New()
        End Sub

        Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class
End Class
