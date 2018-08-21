Imports System.ComponentModel
Imports FEBase

<TypeConverter(GetType(PropertyConverter(Of Moveset)))>
Public Class Moveset
    Public Property Name As String
    Public Property Frames As New List(Of Frame)
    Public Property FrameDelay As Integer = 41
    Public Property WieldTypes As WieldType = WieldType.None
End Class
