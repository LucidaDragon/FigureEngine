Imports System.Windows.Forms

Public Class ProgressDialog
    Protected Shared Dialogs As New List(Of ProgressDialog)

    Public Property ProgressType As ProgressBarStyle
        Get
            Return ProgressBar.Style
        End Get
        Set(value As ProgressBarStyle)
            ProgressBar.Style = value
        End Set
    End Property

    Public Property Status As String
        Get
            Return StatusLabel.Text
        End Get
        Set(value As String)
            StatusLabel.Text = value
        End Set
    End Property

    Public Property Progress As Double
        Get
            Return ProgressBar.Value
        End Get
        Set(value As Double)
            ProgressBar.Value = value
        End Set
    End Property

    Sub New()
        InitializeComponent()

        Dialogs.Add(Me)
    End Sub

    Public Shared Sub CloseAll()
        For Each dia In Dialogs
            dia.ThreadClose()
        Next
        Dialogs.Clear()
        GC.Collect()
    End Sub

    Public Sub ThreadClose()
        If InvokeRequired Then
            Invoke(Sub()
                       Close()
                   End Sub)
        Else
            Close()
        End If
    End Sub

    Public Sub UpdateProgress(progress As Double, Optional status As String = "")
        Me.Progress = progress * 100
        If Not status = "" Then
            Me.Status = status
        End If
    End Sub
End Class