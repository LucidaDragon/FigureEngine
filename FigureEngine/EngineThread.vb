Option Strict On

Public Class EngineThread
    Inherits ComponentModel.BackgroundWorker

    Public Property Speed As Integer
    Public Property Layer As Integer
    Public Property Ready As Boolean = False

    Sub New(layer As Integer, speed As Integer)
        MyBase.New()
        WorkerReportsProgress = True
        WorkerSupportsCancellation = True
        Me.Layer = layer
        Me.Speed = speed
    End Sub

    Public Event UpdateUI(data As Bitmap, layer As Integer)

    Private Sub Worker_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles MyBase.DoWork
        Dim eng As New Engine(CType(e.Argument, Point).X, CType(e.Argument, Point).Y)
        Dim time As DateTime = DateTime.Now
        Dim delta As Double = 0

        While (Not Ready) And (Not CancellationPending)
            Threading.Thread.Sleep(Speed)
        End While

        While Not CancellationPending
            delta = (DateTime.Now - time).TotalMilliseconds
            time = DateTime.Now

            eng.Tick(delta)
            eng.Draw(delta)

            ReportProgress(0, CType(eng.RenderSurface, Bitmap))
            Threading.Thread.Sleep(Speed)
        End While
    End Sub

    Private Sub Worker_ReportProgress(sender As Object, e As ComponentModel.ProgressChangedEventArgs) Handles MyBase.ProgressChanged
        RaiseEvent UpdateUI(CType(e.UserState, Bitmap), Layer)
    End Sub
End Class
