Imports System.Windows.Forms

Public Class BundleBrowser
    Private Sub BundleBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub LoadBundle(bundle As BundleData, Optional root As TreeNode = Nothing)
        If root Is Nothing Then
            TreeView1.Nodes.Clear()
            root = New TreeNode("Bundle Data") With {.Tag = bundle, .ImageIndex = 0}
            TreeView1.Nodes.Add(root)
        End If

        For Each file In bundle.Files
            root.Nodes.Add(New TreeNode(file.Name) With {.Tag = file, .ImageIndex = 1})
        Next

        For Each directory In bundle.Directories
            Dim newRoot As New TreeNode(directory.Name) With {.Tag = directory, .ImageIndex = 0}
            LoadBundle(directory, newRoot)
            root.Nodes.Add(newRoot)
        Next
    End Sub

    Public Function ProcessFolder(path As String) As BundleData
        Dim bundle As New BundleData
        bundle.Name = IO.Path.GetFileName(path)

        For Each file In IO.Directory.GetFiles(path)
            bundle.Files.Add(New BundleData.BundleFile With {
                .Name = IO.Path.GetFileName(file),
                .Content = IO.File.ReadAllText(file)
            })
        Next

        For Each directory In IO.Directory.GetDirectories(path)
            bundle.Directories.Add(ProcessFolder(directory))
        Next

        Return bundle
    End Function

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim pkg As New Package
                pkg.Load(ofd.FileName)

                If pkg.Types.Contains(PackageType.BundleData) Then
                    LoadBundle(pkg.BundleData)
                Else
                    Throw New InvalidOperationException("The package does not contain bundle data.")
                End If
            Catch ex As Exception
                MsgBox("Import Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub ConvertFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertFolderToolStripMenuItem.Click
        Dim fbd As New FolderBrowserDialog

        If fbd.ShowDialog() = DialogResult.OK Then
            Try
                LoadBundle(ProcessFolder(fbd.SelectedPath))
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub
End Class