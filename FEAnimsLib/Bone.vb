Imports System.ComponentModel
Imports System.Drawing
Imports FEBase

<TypeConverter(GetType(PropertyConverter(Of Bone)))>
Public Class Bone
    Public Property StartPoint As Point
    Public Property EndPoint As Point

End Class
