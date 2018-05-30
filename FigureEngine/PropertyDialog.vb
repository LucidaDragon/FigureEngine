Public Class PropertyDialog
    Public Property EditObject As Object

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub PropertyDialog_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        PropertyGrid1.SelectedObject = EditObject
    End Sub
End Class
