Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports FEBase
Imports FEForms
Imports FEPackaging

Public Class FrameEditor
    Private EditorSettings As New FrameEditorSettings
    Private LoadedMoveset As New Moveset
    Private SelectedFrame As Integer = 0
    Private Refreshing As Boolean = False

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Dim frm As New FrameEditor
        frm.Show()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
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

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog
        If sfd.ShowDialog() = DialogResult.OK Then
            Dim thrd As New Thread(Sub()
                                       Dim prog As New ProgressDialog With {
                                           .ProgressType = ProgressBarStyle.Marquee,
                                           .Status = "Saving package..."
                                       }
                                       prog.ShowDialog()
                                   End Sub)
            thrd.Start()

            Try
                Dim pkg As New Package
                pkg.SetPackageTypes(PackageType.JsonData)
                pkg.JsonData = Json.Serialize(LoadedMoveset)
                pkg.Save(sfd.FileName)
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
            ProgressDialog.CloseAll()
        End If
    End Sub

    Public Sub RefreshUI()
        If Not Refreshing Then
            Refreshing = True
            MovesetPropertyGrid.SelectedObject = LoadedMoveset
            PlayTimer.Interval = LoadedMoveset.FrameDelay
            If SelectedFrame < LoadedMoveset.Frames.Count And SelectedFrame >= 0 Then
                FramePropertyGrid.SelectedObject = LoadedMoveset.Frames(SelectedFrame)
                UpdateViewport()
                Dim indx As Integer = FrameSelector.SelectedIndex
                FrameSelector.Items.Clear()
                For i As Integer = 0 To LoadedMoveset.Frames.Count - 1
                    FrameSelector.Items.Add("Frame " & i + 1)
                Next
                If FrameSelector.Items.Count > indx Then
                    FrameSelector.SelectedIndex = indx
                End If
            Else
                FramePropertyGrid.SelectedObject = Nothing
                PlayerViewport.Image = Nothing
                FrameSelector.Items.Clear()
            End If
            Refreshing = False
        End If
    End Sub

    Public Sub UpdateViewport()
        PlayerViewport.Image = FrameOverlay(LoadedMoveset.Frames(SelectedFrame), EditorSettings.DrawFrameData)
        PlayerViewport.Invalidate()
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

    Private Sub FrameSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FrameSelector.SelectedIndexChanged
        SelectedFrame = FrameSelector.SelectedIndex
        RefreshUI()
    End Sub

    Private Sub FrameEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshUI()
    End Sub

    Private Sub PlayTimer_Tick(sender As Object, e As EventArgs) Handles PlayTimer.Tick
        If SelectedFrame + 1 < LoadedMoveset.Frames.Count Then
            SelectedFrame += 1
        Else
            SelectedFrame = 0
        End If
        UpdateViewport()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        PlayTimer.Stop()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        PlayTimer.Start()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        SelectedFrame = 0
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        SelectedFrame = LoadedMoveset.Frames.Count - 1
    End Sub

    Private Function FrameOverlay(frame As Frame, show As Boolean) As Bitmap
        If show Then
            Dim copy As Bitmap = frame.Image.Image.Clone()
            Dim g As Graphics = Graphics.FromImage(copy)
            g.FillEllipse(New TextureBrush(My.Resources.BlueChecker), New Rectangle(frame.LeftWeaponOrigin + New Vector(CDbl(EditorSettings.DataPointRadius * -1), CDbl(EditorSettings.DataPointRadius * -1)), New Point(EditorSettings.DataPointRadius * 2, EditorSettings.DataPointRadius * 2)))
            g.FillEllipse(New TextureBrush(My.Resources.BlueChecker), New Rectangle(frame.RightWeaponOrigin + New Vector(CDbl(EditorSettings.DataPointRadius * -1), CDbl(EditorSettings.DataPointRadius * -1)), New Point(EditorSettings.DataPointRadius * 2, EditorSettings.DataPointRadius * 2)))
            g.DrawLine(New Pen(New TextureBrush(My.Resources.GreenChecker), EditorSettings.DataVectorWidth), frame.LeftWeaponOrigin, frame.LeftWeaponOrigin + (frame.LeftWeaponVector * EditorSettings.DataVectorLength))
            g.DrawLine(New Pen(New TextureBrush(My.Resources.GreenChecker), EditorSettings.DataVectorWidth), frame.RightWeaponOrigin, frame.RightWeaponOrigin + (frame.RightWeaponVector * EditorSettings.DataVectorLength))
            Return copy
        Else
            Return frame.Image.Image.Clone()
        End If
    End Function

    Private Sub FrameEditor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        EditorSettings.Save()
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        Dim propDia As New PropertyDialog With {
            .SelectedObject = EditorSettings,
            .Text = "Frame Editor Settings"
        }
        propDia.ShowDialog()
    End Sub

    Private Sub FramePropertyGrid_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles FramePropertyGrid.PropertyValueChanged
        RefreshUI()
    End Sub

    Private Sub FolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FolderToolStripMenuItem.Click
        Dim fbd As New FolderBrowserDialog
        If fbd.ShowDialog() = DialogResult.OK Then
            Dim progDia As New ProgressDialog With {
                .ProgressType = ProgressBarStyle.Continuous,
                .Status = "Sorting files..."
            }
            progDia.Show()
            Application.DoEvents()
            Dim files As List(Of String)
            Try
                files = IO.Directory.GetFiles(fbd.SelectedPath).ToList()
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
                progDia.Close()
                Exit Sub
            End Try
            files.Sort()
            LoadedMoveset = New Moveset
            SelectedFrame = 0

            Dim count As Integer = 0
            Dim msgLog As String = String.Empty
            Dim frames As New List(Of Frame)
            For Each file In files
                Try
                    frames.Add(New Frame With {
                        .Image = New Bitmap(file)
                    })
                    count += 1
                Catch ex As Exception
                    msgLog += "Error importing " & IO.Path.GetFileName(file) & ": " & ex.Message
                End Try
                progDia.UpdateProgress(count / files.Count, IO.Path.GetFileName(file))
            Next

            If msgLog IsNot String.Empty Then
                MsgBox("The following errors occued while importing:" & Environment.NewLine & msgLog)
                progDia.Close()
                Exit Sub
            End If

            LoadedMoveset.Frames = frames
            RefreshUI()
            progDia.Close()
        End If
    End Sub
End Class