Public Class PropertyDialog
    Public Property SelectedObject As Object
        Get
            Return PropertyGrid1.SelectedObject
        End Get
        Set(value As Object)
            PropertyGrid1.SelectedObject = value
        End Set
    End Property
End Class
