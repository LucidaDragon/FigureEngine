Imports System.ComponentModel
Imports FEPackaging

Public Class FrameEditorSettings
    Implements ISerialize

    Public Property DrawFrameData As Boolean = True
    Public Property DataPointRadius As Integer = My.Settings.EditorPointRadius
    Public Property DataVectorLength As Integer = My.Settings.EditorVectorLength
    Public Property DataVectorWidth As Integer = My.Settings.EditorVectorWidth

    Public Sub Save()
        My.Settings.EditorPointRadius = DataPointRadius
        My.Settings.EditorVectorLength = DataVectorLength
        My.Settings.EditorVectorWidth = DataVectorWidth
        My.Settings.Save()
    End Sub

    <Browsable(False)>
    Public Property FullName As String Implements ISerialize.FullName
        Get
            Return Me.GetType().FullName
        End Get
        Set(value As String)
        End Set
    End Property

    Public Function Serialize() As String Implements ISerialize.Serialize
        Return Json.Serialize(Me)
    End Function

    Public Function Deserialize(str As String) As ISerialize Implements ISerialize.Deserialize
        Return Json.Deserialize(Of FrameEditorSettings)(str)
    End Function
End Class
