Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Interface ISerialize
    Function ToJsonString() As String
    Function FromJsonString(str As String) As ISerialize
    Property FullName As String
End Interface
