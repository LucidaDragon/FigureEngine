Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class FileSystem
    Public Shared Sub GenerateResource(obj As ISerialize, path As String)
        IO.File.WriteAllText(path, obj.ToJsonString())
    End Sub

    Public Shared Function ReadResource(path As String) As ISerialize
        Dim t As Type = Reflection.Assembly.GetExecutingAssembly().GetType(Json.Deserialize(Of SerializeBase)(IO.File.ReadAllText(path)).FullName)
        Return TryCast(t.GetConstructor(Type.EmptyTypes).Invoke(Nothing), ISerialize).FromJsonString(IO.File.ReadAllText(path))
    End Function
End Class
