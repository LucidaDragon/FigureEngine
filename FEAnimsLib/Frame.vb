Imports System.ComponentModel
Imports System.Drawing
Imports FEBase

<TypeConverter(GetType(PropertyConverter(Of Frame)))>
Public Class Frame
    Public Property Image As Bitmap
    Public Property WeaponOrigin As Point
    Public Property WeaponVector As Vector
    Public Property Bones As List(Of Bone)
End Class
