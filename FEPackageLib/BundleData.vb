Imports System.ComponentModel
Imports FEBase

<TypeConverter(GetType(PropertyConverter(Of BundleData)))>
Public Class BundleData
    Public Property Name As String
    Public Property Directories As New List(Of BundleData)
    Public Property Files As New List(Of BundleFile)

    Public Class BundleFile
        Public Property Name As String
        Public Property Content As String
    End Class
End Class
