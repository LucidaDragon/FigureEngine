Imports System.ComponentModel
Imports System.Drawing
Imports FEBase

<TypeConverter(GetType(PropertyConverter(Of Frame)))>
Public Class Frame
    Public Property Image As New BitmapData With {
        .Image = Nothing
    }
    Public Property RightWeaponOrigin As New Point
    Public Property RightWeaponVector As New Vector
    Public Property LeftWeaponOrigin As New Point
    Public Property LeftWeaponVector As New Vector
    Public Property HurtBoxes As New List(Of Rectangle)
    Public Property HitBoxes As New List(Of Rectangle)
    Public Property DamageScale As Double
End Class
