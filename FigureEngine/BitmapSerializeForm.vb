Option Strict On
Option Explicit On

Public Class BitmapSerializeForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ofd As New OpenFileDialog With {
            .FileName = ""
        }
        If ofd.ShowDialog() = DialogResult.OK Then
            TextBox1.Text = ofd.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sfd As New SaveFileDialog With {
            .FileName = ""
        }
        If sfd.ShowDialog() = DialogResult.OK Then
            TextBox2.Text = sfd.FileName
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IO.File.Exists(TextBox1.Text) Then
            Try
                Dim json As New Web.Script.Serialization.JavaScriptSerializer
                Dim data As BitmapData

                If ComboBox1.SelectedIndex = 0 Then
                    data = CType(Image.FromFile(TextBox1.Text), BitmapData)
                ElseIf ComboBox1.SelectedIndex = 1 Then
                    data = json.Deserialize(Of BitmapData)(IO.File.ReadAllText(TextBox1.Text))
                Else
                    MsgBox("Unknown import type.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                If ComboBox2.SelectedIndex = 0 Then
                    IO.File.WriteAllText(TextBox2.Text, json.Serialize(data))
                ElseIf ComboBox2.SelectedIndex = 1 Then
                    data.Image.Save(TextBox2.Text, Imaging.ImageFormat.Png)
                ElseIf ComboBox2.SelectedIndex = 2 Then
                    data.Image.Save(TextBox2.Text, Imaging.ImageFormat.Bmp)
                ElseIf ComboBox2.SelectedIndex = 3 Then
                    data.Image.Save(TextBox2.Text, Imaging.ImageFormat.Jpeg)
                Else
                    MsgBox("Unknown export type.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Catch ex As Exception
                MsgBox("An error occured while processing: " & ex.Message & Environment.NewLine & Environment.NewLine & ex.ToString(), MsgBoxStyle.Exclamation)
                Exit Sub
            End Try
        Else
            MsgBox(TextBox1.Text & " does not exist.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
    End Sub

    Private Sub UpdateProcessButton() Handles ComboBox1.SelectedIndexChanged, ComboBox2.SelectedIndexChanged, ComboBox1.SelectedValueChanged, ComboBox2.SelectedValueChanged, TextBox1.TextChanged, TextBox2.TextChanged
        If ComboBox1.SelectedIndex >= 0 And ComboBox1.SelectedIndex < ComboBox2.Items.Count And ComboBox2.SelectedIndex >= 0 And ComboBox2.SelectedIndex < ComboBox2.Items.Count And TextBox1.Text.Length > 0 And TextBox2.Text.Length > 0 Then
            Button3.Enabled = True
        Else
            Button3.Enabled = False
        End If
    End Sub
End Class