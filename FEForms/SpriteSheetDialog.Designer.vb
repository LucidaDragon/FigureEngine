<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpriteSheetDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SpriteSheetDialog))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericWidth = New System.Windows.Forms.NumericUpDown()
        Me.NumericHeight = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericTileCount = New System.Windows.Forms.NumericUpDown()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.NumericWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericTileCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tile Width"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Tile Height"
        '
        'NumericWidth
        '
        Me.NumericWidth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericWidth.Location = New System.Drawing.Point(76, 7)
        Me.NumericWidth.Maximum = New Decimal(New Integer() {1920, 0, 0, 0})
        Me.NumericWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericWidth.Name = "NumericWidth"
        Me.NumericWidth.Size = New System.Drawing.Size(146, 20)
        Me.NumericWidth.TabIndex = 2
        Me.NumericWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NumericHeight
        '
        Me.NumericHeight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericHeight.Location = New System.Drawing.Point(76, 33)
        Me.NumericHeight.Maximum = New Decimal(New Integer() {1920, 0, 0, 0})
        Me.NumericHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericHeight.Name = "NumericHeight"
        Me.NumericHeight.Size = New System.Drawing.Size(146, 20)
        Me.NumericHeight.TabIndex = 3
        Me.NumericHeight.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Tile Count"
        '
        'NumericTileCount
        '
        Me.NumericTileCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericTileCount.Location = New System.Drawing.Point(76, 59)
        Me.NumericTileCount.Maximum = New Decimal(New Integer() {2147483647, 0, 0, 0})
        Me.NumericTileCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericTileCount.Name = "NumericTileCount"
        Me.NumericTileCount.Size = New System.Drawing.Size(146, 20)
        Me.NumericTileCount.TabIndex = 5
        Me.NumericTileCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(135, 85)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(87, 24)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Select File"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SpriteSheetDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 121)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.NumericTileCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NumericHeight)
        Me.Controls.Add(Me.NumericWidth)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SpriteSheetDialog"
        Me.Text = "Import Sprite Sheet"
        CType(Me.NumericWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericTileCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents NumericWidth As Windows.Forms.NumericUpDown
    Friend WithEvents NumericHeight As Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents NumericTileCount As Windows.Forms.NumericUpDown
    Friend WithEvents Button1 As Windows.Forms.Button
End Class
