﻿Option Strict On
Option Explicit On
#Disable Warning IDE0037

Public Class MainForm
    Public Property Display As BitmapData
        Get
            Return CType(ScaleBox.Image, BitmapData)
        End Get
        Set(value As BitmapData)
            ScaleBox.Image = value
            ScaleBox.Invalidate()
            Graphics = Graphics.FromImage(Display)
        End Set
    End Property
    Private Graphics As Graphics

    Public Property Threads As New List(Of EngineThread)
    Public Property Layers As New List(Of BitmapData)
    Public Property Engines As New List(Of Engine)
    Public Property Resolution As New Point(800, 600)

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Input.KeyDown(e.KeyCode)
    End Sub

    Private Sub MainForm_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Input.KeyUp(e.KeyCode)
    End Sub

    Private Sub MainForm_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then
            Input.KeyDown(Keys.LButton)
        ElseIf e.Button = MouseButtons.Right Then
            Input.KeyDown(Keys.RButton)
        End If
    End Sub

    Private Sub MainForm_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = MouseButtons.Left Then
            Input.KeyUp(Keys.LButton)
        ElseIf e.Button = MouseButtons.Right Then
            Input.KeyUp(Keys.RButton)
        End If
    End Sub

    Public Sub InitViewport(Optional width As Integer = 800, Optional height As Integer = 600)
        For Each thread In Threads
            thread.CancelAsync()
        Next
        Threads.Clear()
        Layers.Clear()
        Resolution = New Point(width, height)
        Display = New Bitmap(Resolution.X, Resolution.Y)
    End Sub

    Public Function AddLayer(Optional multithread As Boolean = True) As EngineThread
        If multithread Then
            Threads.Add(New EngineThread(Layers.Count, 16))
            Layers.Add(New Bitmap(Resolution.X, Resolution.Y))
            AddHandler Threads.Last.UpdateUI, AddressOf UpdateLayer
            Threads.Last.RunWorkerAsync(Resolution)
            Return Threads.Last
        Else
            Layers.Add(New Bitmap(Resolution.X, Resolution.Y))
            Return Nothing
        End If
    End Function

    Public Sub UpdateLayer(data As Bitmap, layer As Integer)
        Layers.Item(layer) = data
    End Sub

    Public Sub CombineLayers()
        Graphics.Clear(Color.Black)
        For i As Integer = 0 To Layers.Count - 1
            Graphics.DrawImage(Layers(i), 0, 0)
        Next
        Graphics.Flush()
        ScaleBox.Invalidate()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitViewport()
        '
        'MetaGraphicEditor.ShowDialog()
        '
        'PropertyDialog.EditObject = Me
        'PropertyDialog.ShowDialog()
        '
        Dim eng As New Engine(Resolution.X, Resolution.Y)
        Layers.Add(Nothing)
        eng.AddObject(New StaticGraphic(My.Resources.DebugImage, New Point(0, 0)))
        AddHandler eng.UpdateUI, AddressOf UpdateLayer
        Engines.Add(eng)
    End Sub

    Private Sub GameTimer_Tick(sender As Object, e As EventArgs) Handles GameTimer.Tick
        For Each engine In Engines
            engine.Draw(GameTimer.Interval)
        Next

        CombineLayers()

        If Input.Query({Keys.W, Keys.Up}) Then
            Camera.MoveCamera(0, 1)
        End If
        If Input.Query({Keys.A, Keys.Left}) Then
            Camera.MoveCamera(-1, 0)
        End If
        If Input.Query({Keys.S, Keys.Down}) Then
            Camera.MoveCamera(0, -1)
        End If
        If Input.Query({Keys.D, Keys.Right}) Then
            Camera.MoveCamera(1, 0)
        End If
    End Sub
End Class