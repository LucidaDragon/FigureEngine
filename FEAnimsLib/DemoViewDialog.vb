Imports System.Threading
Imports System.Windows.Forms
Imports FEForms
Imports FEPackaging

Public Class DemoViewDialog
    Private LoadedMoveset As New Moveset
    Private LoadedWeapon As New Weapon

    Private Sub OpenMoveset()
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog() = DialogResult.OK Then
            Dim thrd As New Thread(Sub()
                                       Dim prog As New ProgressDialog With {
                                           .ProgressType = ProgressBarStyle.Marquee,
                                           .Status = "Loading package..."
                                       }
                                       prog.ShowDialog()
                                   End Sub)
            thrd.Start()

            Try
                Dim pkg As New Package
                pkg.Load(ofd.FileName)
                If pkg.Types.Contains(PackageType.JsonData) Then
                    LoadedMoveset = Json.Deserialize(Of Moveset)(pkg.JsonData)
                    RefreshUI()
                Else
                    MsgBox("The package does not contain moveset data.")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
            ProgressDialog.CloseAll()
        End If
    End Sub

    Private Sub OpenWeapon()
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim thrd As New Thread(Sub()
                                           Dim prog As New ProgressDialog With {
                                           .ProgressType = ProgressBarStyle.Marquee,
                                           .Status = "Loading package..."
                                       }
                                           prog.ShowDialog()
                                       End Sub)
                thrd.Start()

                Try
                    Dim pkg As New Package
                    pkg.Load(ofd.FileName)
                    If pkg.Types.Contains(PackageType.JsonData) Then
                        LoadedWeapon = Json.Deserialize(Of Weapon)(pkg.JsonData)
                    Else
                        MsgBox("The package does not contain moveset data.")
                    End If
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message)
                End Try
                ProgressDialog.CloseAll()
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub RefreshUI()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        OpenMoveset()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        OpenWeapon()
    End Sub
End Class