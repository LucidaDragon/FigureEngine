Imports System.ComponentModel
Imports System.Drawing
Imports FEBase
Imports FEPackaging

<TypeConverter(GetType(PropertyConverter(Of Weapon)))>
Public Class Weapon
    Implements ISerialize

    <Category("Appearance")>
    Public Property Image As New BitmapData
    <Category("Usage")>
    Public Property Origin As New Point
    <Category("Usage")>
    Public Property ForwardVector As New Vector
    <Category("Usage")>
    Public Property SupportedWeildTypes As New List(Of WieldType)
    <Category("Damage")>
    Public Property Hurtboxes As New List(Of Rectangle)
    <Category("Damage")>
    Public Property PerTickContactDamage As Double = 1
    <Category("Damage")>
    Public Property MultiplyContactByVelocity As Boolean = True

    <Browsable(False)>
    Public Property FullName As String Implements ISerialize.FullName
        Get
            Return Me.GetType().FullName
        End Get
        Set(value As String)
        End Set
    End Property

    Public Function Serialize() As String Implements ISerialize.Serialize
        Return Json.Serialize(Me)
    End Function

    Public Function Deserialize(str As String) As ISerialize Implements ISerialize.Deserialize
        Return Json.Deserialize(Of Weapon)(str)
    End Function
End Class
