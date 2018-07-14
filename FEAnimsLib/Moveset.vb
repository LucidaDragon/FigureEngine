Imports System.ComponentModel
Imports FEBase

<TypeConverter(GetType(PropertyConverter(Of Moveset)))>
Public Class Moveset
    Public Property Name As String
    Public Property Frames As New List(Of Frame)
End Class
