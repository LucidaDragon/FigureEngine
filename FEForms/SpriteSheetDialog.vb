Imports System.Drawing
Imports System.Windows.Forms

Public Class SpriteSheetDialog
    Public Property Tiles As New List(Of Bitmap)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ofd As New OpenFileDialog
        Dim img As Bitmap = Nothing
        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                img = Bitmap.FromFile(ofd.FileName)
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If

        If img IsNot Nothing Then
            Dim prog As New ProgressDialog With {
                .ProgressType = ProgressBarStyle.Continuous
            }
            prog.Show()
            Dim xPos As Integer = 0
            Dim yPos As Integer = 0
            For i As Integer = 0 To NumericTileCount.Value - 1
                Dim failCount As Integer = 0
                Dim tile As New Bitmap(CType(NumericWidth.Value, Integer), CType(NumericHeight.Value, Integer))

                For x As Integer = 0 To NumericWidth.Value - 1
                    For y As Integer = 0 To NumericHeight.Value - 1
                        If xPos + x < img.Width And yPos + y < img.Height Then
                            tile.SetPixel(x, y, img.GetPixel(xPos + x, yPos + y))
                        Else
                            failCount += 1
                        End If
                    Next
                Next

                If failCount = NumericWidth.Value * NumericHeight.Value Then
                    xPos = 0
                    yPos += NumericHeight.Value
                Else
                    xPos += NumericWidth.Value
                    Tiles.Add(tile)
                End If

                prog.UpdateProgress(i / NumericTileCount.Value, "Importing tiles... (" & i + 1 & "/" & NumericTileCount.Value & ")")
                Application.DoEvents()
            Next
            prog.Close()
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
End Class