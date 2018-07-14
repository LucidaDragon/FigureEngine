Imports System.ComponentModel
Imports System.Drawing
Imports FEBase
Imports FEPackaging

<TypeConverter(GetType(PropertyConverter(Of BitmapData)))>
Public Class BitmapData
    Implements ISerialize

    Public Property Data As String
        Get
            If Image IsNot Nothing Then
                Dim memStream As New IO.MemoryStream
                Try
                    Image.Save(memStream, Image.RawFormat)
                Catch ex As Exception
                    Image.Save(memStream, Imaging.ImageFormat.Bmp)
                End Try
                Return Convert.ToBase64String(memStream.GetBuffer())
            Else
                Return "Nothing"
            End If
        End Get
        Set(value As String)
            Dim memStream As New IO.MemoryStream
            Dim arr As Byte() = Convert.FromBase64String(value)
            memStream.Write(arr, 0, arr.Length)
            Image = New Bitmap(memStream)
        End Set
    End Property
    <Web.Script.Serialization.ScriptIgnore>
    Public Property Image As Bitmap

    Public Property FullName As String Implements ISerialize.FullName
        Get
            Return Me.GetType().FullName
        End Get
        Set(value As String)
        End Set
    End Property

    Sub New()
        Image = New Bitmap(1, 1)
    End Sub

    Public Function Serialize() As String Implements ISerialize.Serialize
        Return Json.Serialize(Me)
    End Function

    Public Function Deserialize(str As String) As ISerialize Implements ISerialize.Deserialize
        Return Json.Deserialize(Of BitmapData)(str)
    End Function

    Public Shared Widening Operator CType(img As Bitmap) As BitmapData
        Return New BitmapData With {
            .Image = img
        }
    End Operator

    Public Shared Widening Operator CType(data As BitmapData) As Bitmap
        Return data.Image
    End Operator
End Class
