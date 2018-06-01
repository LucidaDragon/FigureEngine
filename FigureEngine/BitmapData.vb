Option Strict On
Option Explicit On

Public Class BitmapData
    Public Property Data As String
        Get
            Dim memStream As New IO.MemoryStream
            Try
                Image.Save(memStream, Image.RawFormat)
            Catch ex As Exception
                Image.Save(memStream, Imaging.ImageFormat.Bmp)
            End Try
            Return Convert.ToBase64String(memStream.GetBuffer())
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

    Public Shared Widening Operator CType(img As Bitmap) As BitmapData
        Return New BitmapData With {
            .Image = img
        }
    End Operator

    Public Shared Widening Operator CType(data As BitmapData) As Bitmap
        Return data.Image
    End Operator
End Class
