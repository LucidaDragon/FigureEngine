Imports FEAnimation
Imports FEBase
Imports FEForms
Imports FEPackaging

Public Class DebugForm
    Private Sub DebugForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '
        ' Perform debugging actions below, then run with FEDebug selected as the startup project.
        '

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New FrameEditor
        frm.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm As New WeaponEditor
        frm.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New DemoViewDialog
        frm.Show()
    End Sub
End Class
