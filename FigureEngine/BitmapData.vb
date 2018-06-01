Public Class BitmapData
    Public Property Data As String
        Get
            Dim memStream As New IO.MemoryStream
            Image.Save(memStream, Imaging.ImageFormat.Png)
            Dim buf(memStream.Length) As Byte
            memStream.Read(buf, 0, memStream.Length)
            Return Text.Encoding.UTF8.GetString(buf)
        End Get
        Set(value As String)
            Dim memStream As New IO.MemoryStream
            Dim buf() As Byte
            buf = Text.Encoding.UTF8.GetBytes(value)
            memStream.Write(buf, 0, buf.Count)
            Image = Drawing.Image.FromStream(memStream)
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
