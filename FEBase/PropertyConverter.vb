Imports System.ComponentModel
Imports System.Globalization

Public Class PropertyConverter(Of T)
    Inherits ExpandableObjectConverter

    Private Shared Json As New Web.Script.Serialization.JavaScriptSerializer With {
        .MaxJsonLength = Integer.MaxValue
    }

    Public Overloads Overrides Function CanConvertTo(context As ITypeDescriptorContext, destinationType As Type) As Boolean
        If destinationType Is GetType(T) Then
            Return True
        End If
        Return MyBase.CanConvertTo(context, destinationType)
    End Function

    Public Overloads Overrides Function ConvertTo(context As ITypeDescriptorContext, culture As CultureInfo, value As Object, destinationType As Type) As Object
        If (destinationType Is GetType(String) AndAlso TypeOf value Is T) Then
            Dim obj As T = value
            Return Json.Serialize(obj)
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overloads Overrides Function CanConvertFrom(context As ITypeDescriptorContext, sourceType As Type) As Boolean
        If sourceType Is GetType(String) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(context As ITypeDescriptorContext, culture As CultureInfo, value As Object) As Object
        If TypeOf value Is String Then
            Try
                Return Json.Deserialize(Of T)(value)
            Catch ex As Exception
                MsgBox("Invalid json data. The object could not be converted.", MsgBoxStyle.Exclamation)
            End Try
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function
End Class
