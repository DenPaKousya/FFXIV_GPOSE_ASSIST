<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FRM_MAIN
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM_MAIN))
        Me.NTI_TASK = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.CMS_MENU = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TSM_SETTING = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSS_01 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSM_OPEN_SAVE_FOLDER = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSS_02 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSM_EXIT = New System.Windows.Forms.ToolStripMenuItem()
        Me.TIM_CHECK_PROCESS = New System.Windows.Forms.Timer(Me.components)
        Me.CMS_MENU.SuspendLayout()
        Me.SuspendLayout()
        '
        'NTI_TASK
        '
        Me.NTI_TASK.ContextMenuStrip = Me.CMS_MENU
        Me.NTI_TASK.Visible = True
        '
        'CMS_MENU
        '
        Me.CMS_MENU.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.CMS_MENU.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSM_SETTING, Me.TSS_01, Me.TSM_OPEN_SAVE_FOLDER, Me.TSS_02, Me.TSM_EXIT})
        Me.CMS_MENU.Name = "CMS_MENU"
        Me.CMS_MENU.Size = New System.Drawing.Size(175, 82)
        '
        'TSM_SETTING
        '
        Me.TSM_SETTING.Name = "TSM_SETTING"
        Me.TSM_SETTING.Size = New System.Drawing.Size(174, 22)
        Me.TSM_SETTING.Text = "Setting"
        '
        'TSS_01
        '
        Me.TSS_01.Name = "TSS_01"
        Me.TSS_01.Size = New System.Drawing.Size(171, 6)
        '
        'TSM_OPEN_SAVE_FOLDER
        '
        Me.TSM_OPEN_SAVE_FOLDER.Name = "TSM_OPEN_SAVE_FOLDER"
        Me.TSM_OPEN_SAVE_FOLDER.Size = New System.Drawing.Size(174, 22)
        Me.TSM_OPEN_SAVE_FOLDER.Text = "Open save folder"
        '
        'TSS_02
        '
        Me.TSS_02.Name = "TSS_02"
        Me.TSS_02.Size = New System.Drawing.Size(171, 6)
        '
        'TSM_EXIT
        '
        Me.TSM_EXIT.Name = "TSM_EXIT"
        Me.TSM_EXIT.Size = New System.Drawing.Size(174, 22)
        Me.TSM_EXIT.Text = "Exit"
        '
        'TIM_CHECK_PROCESS
        '
        Me.TIM_CHECK_PROCESS.Interval = 1000
        '
        'FRM_MAIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(10, 10)
        Me.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FRM_MAIN"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.CMS_MENU.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents NTI_TASK As NotifyIcon
    Friend WithEvents CMS_MENU As ContextMenuStrip
    Friend WithEvents TSS_01 As ToolStripSeparator
    Friend WithEvents TSM_EXIT As ToolStripMenuItem
    Friend WithEvents TIM_CHECK_PROCESS As Timer
    Friend WithEvents TSM_SETTING As ToolStripMenuItem
    Friend WithEvents TSM_OPEN_SAVE_FOLDER As ToolStripMenuItem
    Friend WithEvents TSS_02 As ToolStripSeparator
End Class
