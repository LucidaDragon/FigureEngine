Imports System.ComponentModel
Imports FEPackaging

Public Class FrameEditorSettings
    Implements ISerialize

    <Category("Settings")>
    Public Property DrawData As Boolean = True
    <Category("Settings")>
    Public Property DataPointRadius As Integer = My.Settings.EditorPointRadius
    <Category("Settings")>
    Public Property DataVectorLength As Integer = My.Settings.EditorVectorLength
    <Category("Settings")>
    Public Property DataLineWidth As Integer = My.Settings.EditorVectorWidth
    <Category("Settings")>
    Public Property BackgroundType As EditorBackgroundType
        Get
            Return bkgrnd
        End Get
        Set(value As EditorBackgroundType)
            bkgrnd = value
        End Set
    End Property
    Private bkgrnd As EditorBackgroundType

    Public Sub Save()
        My.Settings.EditorPointRadius = DataPointRadius
        My.Settings.EditorVectorLength = DataVectorLength
        My.Settings.EditorVectorWidth = DataLineWidth
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

    Public Enum EditorBackgroundType
        Normal = 0
        Red = 1
        Green = 2
        Blue = 3
    End Enum
End Class
