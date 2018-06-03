Option Strict On
Option Explicit On

Public Class Json
    Private Shared json As New Web.Script.Serialization.JavaScriptSerializer With {
        .MaxJsonLength = Integer.MaxValue,
        .RecursionLimit = 500
    }

    Public Shared Function Serialize(obj As ISerialize) As String
        Return json.Serialize(obj)
    End Function

    Public Shared Function Deserialize(Of T)(input As String) As T
        Return json.Deserialize(Of T)(input)
    End Function
End Class
