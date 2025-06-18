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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM_MAIN))
        NTI_TASK = New NotifyIcon(components)
        CMS_MENU = New ContextMenuStrip(components)
        TSM_SETTING = New ToolStripMenuItem()
        TSS_01 = New ToolStripSeparator()
        TSM_OPEN_SAVE_FOLDER = New ToolStripMenuItem()
        TSS_02 = New ToolStripSeparator()
        TSM_MOBILE = New ToolStripMenuItem()
        TSS_03 = New ToolStripSeparator()
        TSM_EXIT = New ToolStripMenuItem()
        TIM_CHECK_PROCESS = New Timer(components)
        CMS_MENU.SuspendLayout()
        SuspendLayout()
        ' 
        ' NTI_TASK
        ' 
        NTI_TASK.ContextMenuStrip = CMS_MENU
        NTI_TASK.Visible = True
        ' 
        ' CMS_MENU
        ' 
        CMS_MENU.Font = New Font("メイリオ", 9.0F)
        CMS_MENU.Items.AddRange(New ToolStripItem() {TSM_SETTING, TSS_01, TSM_OPEN_SAVE_FOLDER, TSS_02, TSM_MOBILE, TSS_03, TSM_EXIT})
        CMS_MENU.Name = "CMS_MENU"
        CMS_MENU.Size = New Size(175, 110)
        ' 
        ' TSM_SETTING
        ' 
        TSM_SETTING.Name = "TSM_SETTING"
        TSM_SETTING.Size = New Size(174, 22)
        TSM_SETTING.Text = "Setting"
        ' 
        ' TSS_01
        ' 
        TSS_01.Name = "TSS_01"
        TSS_01.Size = New Size(171, 6)
        ' 
        ' TSM_OPEN_SAVE_FOLDER
        ' 
        TSM_OPEN_SAVE_FOLDER.Name = "TSM_OPEN_SAVE_FOLDER"
        TSM_OPEN_SAVE_FOLDER.Size = New Size(174, 22)
        TSM_OPEN_SAVE_FOLDER.Text = "Open save folder"
        ' 
        ' TSS_02
        ' 
        TSS_02.Name = "TSS_02"
        TSS_02.Size = New Size(171, 6)
        ' 
        ' TSM_MOBILE
        ' 
        TSM_MOBILE.Name = "TSM_MOBILE"
        TSM_MOBILE.Size = New Size(174, 22)
        TSM_MOBILE.Text = "Mobile"
        ' 
        ' TSS_03
        ' 
        TSS_03.Name = "TSS_03"
        TSS_03.Size = New Size(171, 6)
        ' 
        ' TSM_EXIT
        ' 
        TSM_EXIT.Name = "TSM_EXIT"
        TSM_EXIT.Size = New Size(174, 22)
        TSM_EXIT.Text = "Exit"
        ' 
        ' TIM_CHECK_PROCESS
        ' 
        TIM_CHECK_PROCESS.Interval = 1000
        ' 
        ' FRM_MAIN
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(120, 10)
        Font = New Font("メイリオ", 9F)
        FormBorderStyle = FormBorderStyle.None
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FRM_MAIN"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        WindowState = FormWindowState.Minimized
        CMS_MENU.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents NTI_TASK As NotifyIcon
    Friend WithEvents CMS_MENU As ContextMenuStrip
    Friend WithEvents TSS_01 As ToolStripSeparator
    Friend WithEvents TSM_EXIT As ToolStripMenuItem
    Friend WithEvents TIM_CHECK_PROCESS As Timer
    Friend WithEvents TSM_SETTING As ToolStripMenuItem
    Friend WithEvents TSM_OPEN_SAVE_FOLDER As ToolStripMenuItem
    Friend WithEvents TSS_02 As ToolStripSeparator
    Friend WithEvents TSM_MOBILE As ToolStripMenuItem
    Friend WithEvents TSS_03 As ToolStripSeparator
End Class
