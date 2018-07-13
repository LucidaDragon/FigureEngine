Imports FEAnimation
Imports FEBase
Imports FEForms
Imports FEPackaging

Public Class DebugForm
    Private Sub DebugForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '
        ' Perform debugging actions below, then run with FEDebug selected as the startup project.
        '

        Dim dia As New PropertyDialog With {
            .SelectedObject = New Package
        }
        dia.ShowDialog()

        Close()
    End Sub
End Class
