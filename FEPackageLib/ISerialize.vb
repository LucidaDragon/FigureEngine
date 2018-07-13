Public Interface ISerialize
    Function Serialize() As String
    Function Deserialize(str As String) As ISerialize
    Property FullName As String
End Interface
