<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFolder = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.clbScripts = New ListViewEmbeddedControls.ListViewEx()
        Me.C1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.C2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.C3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.chbSaveRpt = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Scintilla1 = New ScintillaNET.Scintilla()
        Me.Server = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(414, 45)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(26, 20)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(119, 4)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(136, 20)
        Me.txtServer.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Server"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(119, 81)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 29)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Run Scripts"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Selected Folder"
        '
        'txtFolder
        '
        Me.txtFolder.Location = New System.Drawing.Point(119, 45)
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.ReadOnly = True
        Me.txtFolder.Size = New System.Drawing.Size(268, 20)
        Me.txtFolder.TabIndex = 5
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(270, 81)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 29)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'clbScripts
        '
        Me.clbScripts.CheckBoxes = True
        Me.clbScripts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.C1, Me.C2, Me.Server, Me.C3})
        Me.clbScripts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbScripts.GridLines = True
        Me.clbScripts.HideSelection = False
        Me.clbScripts.Location = New System.Drawing.Point(0, 0)
        Me.clbScripts.MultiSelect = False
        Me.clbScripts.Name = "clbScripts"
        Me.clbScripts.Size = New System.Drawing.Size(701, 548)
        Me.clbScripts.TabIndex = 7
        Me.clbScripts.UseCompatibleStateImageBehavior = False
        Me.clbScripts.View = System.Windows.Forms.View.Details
        '
        'C1
        '
        Me.C1.Text = "Script Name"
        Me.C1.Width = 200
        '
        'C2
        '
        Me.C2.Text = ""
        '
        'C3
        '
        Me.C3.Text = "Status"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClearAll)
        Me.Panel1.Controls.Add(Me.btnSelectAll)
        Me.Panel1.Controls.Add(Me.chbSaveRpt)
        Me.Panel1.Controls.Add(Me.txtFolder)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.txtServer)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(42, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(540, 180)
        Me.Panel1.TabIndex = 9
        '
        'btnClearAll
        '
        Me.btnClearAll.Location = New System.Drawing.Point(14, 147)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(75, 23)
        Me.btnClearAll.TabIndex = 11
        Me.btnClearAll.Text = "Clear All"
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(14, 118)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectAll.TabIndex = 10
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'chbSaveRpt
        '
        Me.chbSaveRpt.AutoSize = True
        Me.chbSaveRpt.Location = New System.Drawing.Point(119, 124)
        Me.chbSaveRpt.Name = "chbSaveRpt"
        Me.chbSaveRpt.Size = New System.Drawing.Size(92, 17)
        Me.chbSaveRpt.TabIndex = 9
        Me.chbSaveRpt.Text = "Save RPT file"
        Me.chbSaveRpt.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.clbScripts)
        Me.Panel2.Location = New System.Drawing.Point(42, 230)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(703, 550)
        Me.Panel2.TabIndex = 10
        '
        'Scintilla1
        '
        Me.Scintilla1.AutoCMaxHeight = 9
        Me.Scintilla1.Location = New System.Drawing.Point(768, 230)
        Me.Scintilla1.Name = "Scintilla1"
        Me.Scintilla1.Size = New System.Drawing.Size(591, 548)
        Me.Scintilla1.TabIndex = 11
        '
        'Server
        '
        Me.Server.Text = "Server"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1382, 792)
        Me.Controls.Add(Me.Scintilla1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents txtServer As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtFolder As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents clbScripts As ListViewEmbeddedControls.ListViewEx
    Friend WithEvents C1 As ColumnHeader
    Friend WithEvents C2 As ColumnHeader
    Friend WithEvents C3 As ColumnHeader
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Scintilla1 As ScintillaNET.Scintilla
    Friend WithEvents chbSaveRpt As CheckBox
    Friend WithEvents btnClearAll As Button
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents Server As ColumnHeader
End Class
