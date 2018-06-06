Public Class MetaGraphicEditor
    Public Property EditableMetaGraphic As New MetaGraphic

    Private Border As Boolean = True

    Private Sub MetaGraphicEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PropertyGrid1.SelectedObject = EditableMetaGraphic
    End Sub

    Private Sub DrawTimer_Tick(sender As Object, e As EventArgs) Handles DrawTimer.Tick
        Viewport.Image = EditableMetaGraphic.CurrentFrame.Clone()
        Using g As Graphics = Graphics.FromImage(Viewport.Image)
            If Border Then
                g.DrawRectangle(New Pen(Color.Red, 2), New Rectangle(New Point, Viewport.Image.Size))
            End If
            For Each elem In EditableMetaGraphic.CurrentData
                g.DrawImage(My.Resources.OrangeDot, New Rectangle(elem.Data, New Size(4, 4)))
            Next
        End Using
        TextBox1.Text = SerializeListOfData(EditableMetaGraphic.CurrentData)
        Try
            If Viewport.Image.Size.Width * Viewport.Image.Size.Height > 1920 ^ 2 Then
                GC.Collect()
            End If
        Catch ex As Exception
            GC.Collect()
        End Try
    End Sub

    Private Function SerializeListOfData(data As List(Of MetaGraphic.MetaData)) As String
        Dim result As String = String.Empty

        If data IsNot Nothing Then
            For Each elem In data
                If elem IsNot Nothing Then
                    result += "{Name:""" & elem.Name & """,Data:{X:" & elem.Data.X & ",Y:" & elem.Data.Y & "},"
                End If
            Next
        End If

        Return result.TrimEnd(",")
    End Function

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        EditableMetaGraphic = New MetaGraphic
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                EditableMetaGraphic = Json.Deserialize(Of MetaGraphic)(IO.File.ReadAllText(ofd.FileName))
            Catch ex As Exception
                MsgBox("Error while loading: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog With {
            .Filter = "Meta Graphic Json (*.mgj) | *.mgj"
        }

        If sfd.ShowDialog() = DialogResult.OK Then
            Try
                IO.File.WriteAllText(sfd.FileName, Json.Serialize(EditableMetaGraphic))
            Catch ex As Exception
                MsgBox("Error while saving: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Viewport_Click(sender As Object, e As EventArgs) Handles Viewport.Click
        If Viewport.SizeMode = PictureBoxSizeMode.Zoom Then
            Viewport.SizeMode = PictureBoxSizeMode.CenterImage
        ElseIf Viewport.SizeMode = PictureBoxSizeMode.CenterImage Then
            Viewport.SizeMode = PictureBoxSizeMode.StretchImage
        ElseIf Viewport.SizeMode = PictureBoxSizeMode.StretchImage Then
            Viewport.SizeMode = PictureBoxSizeMode.Zoom
        End If
        SizeModeLabel.Text = Viewport.SizeMode.ToString()
    End Sub

    Private Sub BorderDrawLabel_Click(sender As Object, e As EventArgs) Handles BorderDrawLabel.Click
        If BorderDrawLabel.Text = "Enabled" Then
            BorderDrawLabel.Text = "Disabled"
            Border = False
        ElseIf BorderDrawLabel.Text = "Disabled" Then
            BorderDrawLabel.Text = "Enabled"
            Border = True
        End If
    End Sub

    Private Sub SizeModeLabel_Click(sender As Object, e As EventArgs) Handles SizeModeLabel.Click
        Viewport_Click(Nothing, Nothing)
    End Sub
End Class