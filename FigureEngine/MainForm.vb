Public Class MainForm
    Public Property Display As Bitmap
        Get
            Return ScaleBox.Image
        End Get
        Set(value As Bitmap)
            ScaleBox.Image = value
            ScaleBox.Invalidate()
        End Set
    End Property

    Private Sub ScaleBox_Click(sender As Object, e As EventArgs) Handles ScaleBox.Click

    End Sub
End Class
