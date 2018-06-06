Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class SerializeBase
    Implements ISerialize

    Public Property FullName As String Implements ISerialize.FullName

    Public Function ToJsonString() As String Implements ISerialize.ToJsonString
        Return Json.Serialize(Me)
    End Function

    Public Function FromJsonString(str As String) As ISerialize Implements ISerialize.FromJsonString
        Return Json.Deserialize(Of SerializeBase)(str)
    End Function
End Class
