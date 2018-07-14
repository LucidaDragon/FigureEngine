Imports System.Windows.Forms
Imports FEForms
Imports FEPackaging

Public Class FrameEditor
    Private LoadedMoveset As New Moveset
    Private SelectedFrame As Integer = 0

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Dim frm As New FrameEditor
        frm.ShowDialog()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim pkg As New Package
                pkg.Load(ofd.FileName)
                If pkg.Types.Contains(PackageType.JsonData) Then
                    LoadedMoveset = Json.Deserialize(Of Moveset)(pkg.JsonData)
                    For i As Integer = 0 To LoadedMoveset.Frames.Count - 1
                        FrameSelector.Items.Add("Frame " & i + 1)
                    Next
                    FrameSelector.SelectedIndex = -1
                Else
                    MsgBox("The package does not contain moveset data.")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog
        If sfd.ShowDialog() = DialogResult.OK Then
            Try
                Dim pkg As New Package
                pkg.SetPackageTypes(PackageType.JsonData)
                pkg.JsonData = Json.Serialize(LoadedMoveset)
                pkg.Save(sfd.FileName)
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Public Sub RefreshUI()
        If SelectedFrame < LoadedMoveset.Frames.Count And SelectedFrame >= 0 Then
            FramePropertyGrid.SelectedObject = LoadedMoveset.Frames(SelectedFrame)
            PlayerViewport.Image = LoadedMoveset.Frames(SelectedFrame).Image
        Else
            FramePropertyGrid.SelectedObject = Nothing
            PlayerViewport.Image = Nothing
        End If
    End Sub

    Private Sub SpriteSheetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpriteSheetToolStripMenuItem.Click
        Dim ssd As New SpriteSheetDialog
        If ssd.ShowDialog() = DialogResult.OK Then
            LoadedMoveset.Frames.Clear()
            For Each tile In ssd.Tiles
                LoadedMoveset.Frames.Add(New Frame With {
                    .Image = tile
                })
            Next
            RefreshUI()
        End If
    End Sub
End Class