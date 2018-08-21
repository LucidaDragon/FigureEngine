Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports FEBase
Imports FEForms
Imports FEPackaging

Public Class WeaponEditor
    Private LoadedWeapon As New Weapon
    Private EditorSettings As New FrameEditorSettings

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Dim frm As New WeaponEditor
        frm.Show()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
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
                        RefreshUI()
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

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog With {
            .DefaultExt = "json",
            .Filter = "Javascript Object Notation File (*.json)|*.json"
        }
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
                pkg.JsonData = Json.Serialize(LoadedWeapon)
                pkg.Save(sfd.FileName)
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
            ProgressDialog.CloseAll()
        End If
    End Sub

    Private Sub RefreshUI()
        WeaponPropertyGrid.SelectedObject = LoadedWeapon
        ViewportBox.Image = WeaponOverlay(LoadedWeapon, EditorSettings.DrawData)
        If EditorSettings.BackgroundType = FrameEditorSettings.EditorBackgroundType.Red Then
            SplitContainer1.Panel1.BackgroundImage = My.Resources.OrangeChecker
        ElseIf EditorSettings.BackgroundType = FrameEditorSettings.EditorBackgroundType.Green Then
            SplitContainer1.Panel1.BackgroundImage = My.Resources.GreenChecker
        ElseIf EditorSettings.BackgroundType = FrameEditorSettings.EditorBackgroundType.Blue Then
            SplitContainer1.Panel1.BackgroundImage = My.Resources.BlueChecker
        Else
            SplitContainer1.Panel1.BackgroundImage = My.Resources.TransparencyCheckers
        End If
    End Sub

    Private Function WeaponOverlay(weap As Weapon, show As Boolean) As Bitmap
        Dim copy As Bitmap = weap.Image.Image.Clone()
        Using g As Graphics = Graphics.FromImage(copy)
            g.FillEllipse(New TextureBrush(My.Resources.BlueChecker), New Rectangle(weap.Origin + New Vector(CDbl(EditorSettings.DataPointRadius * -1), CDbl(EditorSettings.DataPointRadius * -1)), New Point(EditorSettings.DataPointRadius * 2, EditorSettings.DataPointRadius * 2)))
            If weap.Hurtboxes.Count > 0 Then
                g.DrawRectangles(New Pen(New TextureBrush(My.Resources.GreenChecker), EditorSettings.DataLineWidth), weap.Hurtboxes.ToArray())
            End If
            g.DrawLine(New Pen(Color.Purple, EditorSettings.DataLineWidth), weap.Origin, weap.Origin + (weap.ForwardVector * EditorSettings.DataVectorLength))
            g.Flush()
        End Using
        Return copy
    End Function

    Private Sub WeaponEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WeaponPropertyGrid.SelectedObject = LoadedWeapon
    End Sub

    Private Sub WeaponPropertyGrid_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles WeaponPropertyGrid.PropertyValueChanged
        RefreshUI()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim propDia As New PropertyDialog With {
            .SelectedObject = EditorSettings,
            .Text = "Weapon Editor Settings"
        }
        propDia.ShowDialog()
    End Sub
End Class