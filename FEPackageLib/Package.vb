Imports System.ComponentModel
Imports FEBase

<TypeConverter(GetType(PropertyConverter(Of Package)))>
Public Class Package
    Implements ISerialize

    Public Property Name As String = String.Empty
    Public Property Meta As New Dictionary(Of String, String)
    Public Property Types As New List(Of PackageType)

    Public Property TextData As String
    Public Property JsonData As String
    Public Property BundleData As New BundleData

    Public Property FullName As String Implements ISerialize.FullName
        Get
            Return Me.GetType().FullName
        End Get
        Set(value As String)
        End Set
    End Property

    Public Sub SetPackageTypes(ParamArray types() As PackageType)
        Me.Types.Clear()
        Me.Types.AddRange(types)
    End Sub

    Public Sub Save(filename As String)
        IO.File.WriteAllText(filename, Serialize())
    End Sub

    Public Sub Load(filename As String)
        Dim pkg As Package = Json.Deserialize(Of Package)(IO.File.ReadAllText(filename))

        For Each prop In Me.GetType().GetProperties()
            prop.SetValue(Me, prop.GetValue(pkg))
        Next
    End Sub

    Public Function Serialize() As String Implements ISerialize.Serialize
        Return Json.Serialize(Me)
    End Function

    Public Function Deserialize(str As String) As ISerialize Implements ISerialize.Deserialize
        Return Json.Deserialize(Of Package)(str)
    End Function
End Class
