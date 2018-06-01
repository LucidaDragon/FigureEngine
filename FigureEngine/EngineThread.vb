Public Class EngineThread
    Inherits ComponentModel.BackgroundWorker

    Public Property Speed As Integer
    Public Property Layer As Integer

    Sub New(layer As Integer, speed As Integer)
        MyBase.New()
        WorkerReportsProgress = True
        WorkerSupportsCancellation = True
        Me.Layer = layer
        Me.Speed = speed
    End Sub

    Public Event UpdateUI(data As Bitmap, layer As Integer)

    Private Sub Worker_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles MyBase.DoWork
        Dim eng As New Engine
        Dim time As DateTime = DateTime.Now
        Dim delta As Double = 0

        While Not CancellationPending
            delta = (DateTime.Now - time).TotalMilliseconds
            time = DateTime.Now

            eng.Tick(delta)
            eng.Draw(delta)

            ReportProgress(0, eng.RenderSurface)
            Threading.Thread.Sleep(Speed)
        End While
    End Sub

    Private Sub Worker_ReportProgress(sender As Object, e As ComponentModel.ProgressChangedEventArgs) Handles MyBase.ProgressChanged
        RaiseEvent UpdateUI(e.UserState, Layer)
    End Sub
End Class
