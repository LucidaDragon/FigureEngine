Public Class Json
    Protected Shared JsonSerializer As New Web.Script.Serialization.JavaScriptSerializer With {
        .MaxJsonLength = Integer.MaxValue
    }

    Public Shared Function Serialize(obj As Object) As String
        Return JsonSerializer.Serialize(obj)
    End Function

    Public Shared Function Deserialize(Of T)(str As String) As T
        Return JsonSerializer.Deserialize(Of T)(str)
    End Function
End Class
