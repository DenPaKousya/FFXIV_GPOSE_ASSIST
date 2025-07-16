Imports System.ComponentModel

Public Class FRM_MAIN

#Region "画面用・変数"
    Private BLN_WINDOW_ACTIVATED As Boolean = False

    Private BLN_DONE_SUB_CHECK_PROCESS As Boolean = False
    Private WPF_WINDOW_MAIN As WPF_MAIN = Nothing
#End Region

#Region "初期化・終了"
    Private Sub SUB_CTRL_NEW_INIT()
        FRM_APPL_MAIN = Me

        Dim STR_ERROR As String
        STR_ERROR = ""
        If Not FUNC_APPL_INIT(STR_ERROR, Me) Then
            Call MessageBox.Show(Me, STR_ERROR, "", MessageBoxDefaultButton.Button1, MessageBoxIcon.Error)
            Call Application.Exit()
        End If

        Me.NTI_TASK.Icon = Me.Icon
    End Sub

    Private Sub SUB_CTRL_VIEW_INIT()
        BLN_WINDOW_ACTIVATED = False
    End Sub

    Private Sub SUB_CTRL_VALUE_INIT()
        TIM_CHECK_PROCESS.Enabled = True
    End Sub

    Private Sub SUB_CTRL_ACTIVATE_INIT()

        If BLN_WINDOW_ACTIVATED Then
            Exit Sub
        End If
        BLN_WINDOW_ACTIVATED = True

    End Sub

    Private Sub SUB_CTRL_NEW_FIN()
        Call SUB_APPL_FIN()
    End Sub

    Private Sub SUB_CTRL_VIEW_FIN()

    End Sub

    Private Sub SUB_CTRL_VALUE_FIN()

    End Sub

#End Region

#Region "処理実行"
    Public Sub SUB_MOBILE()
        Dim WPF_SHOW As WPF_MOBILE_SETTING

        WPF_SHOW = New WPF_MOBILE_SETTING
        Call WPF_SHOW.ShowDialog()

        If Not WPF_SHOW.CANCEL Then
            If Not WPF_WINDOW_MAIN Is Nothing Then
                Call WPF_WINDOW_MAIN.SUB_WINDOW_REFRESH()
            End If
        End If

        Call WPF_SHOW.Close()
        WPF_SHOW = Nothing
    End Sub

    Public Sub SUB_SETTING()
        Dim WPF_SHOW As WPF_SETTING

        WPF_SHOW = New WPF_SETTING
        Call WPF_SHOW.ShowDialog()

        If Not WPF_SHOW.CANCEL Then
            If Not WPF_WINDOW_MAIN Is Nothing Then
                Call WPF_WINDOW_MAIN.SUB_WINDOW_REFRESH()
            End If
        End If

        Call WPF_SHOW.Close()
        WPF_SHOW = Nothing
    End Sub

    Private WPF_SHOW_ROTATE As WPF_ROTATE
    Public Sub SUB_ROTATE()

        If WPF_SHOW_ROTATE Is Nothing Then
            WPF_SHOW_ROTATE = New WPF_ROTATE
        End If

        Dim BLN_VISIBLE As Boolean
        Select Case WPF_SHOW_ROTATE.Visibility
            Case System.Windows.Visibility.Hidden, System.Windows.Visibility.Collapsed
                BLN_VISIBLE = False
            Case System.Windows.Visibility.Visible
                BLN_VISIBLE = True
            Case Else
                BLN_VISIBLE = False
        End Select

        Call WPF_SHOW_ROTATE.SUB_IMAGE_RELOAD()
        If BLN_VISIBLE Then
            WPF_SHOW_ROTATE.Visibility = System.Windows.Visibility.Hidden
            Call WPF_SHOW_ROTATE.Hide()
        Else
            Call WPF_SHOW_ROTATE.Show()
        End If
    End Sub

    Public Sub SUB_OPEN_SAVE_FOLDER()
        Dim STR_DIR As String
        STR_DIR = SRT_APP_SETTINGS_VALUE.SAVE.DIRECTORY

        If Not FUNC_DIR_CHECK(STR_DIR) Then
            Exit Sub
        End If

        Try
            Call System.Diagnostics.Process.Start("EXPLORER.EXE", STR_DIR)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Sub SUB_VIEW_COPY_WINDOW()
        Call SUB_CALL_THUBNAIL(Me)
    End Sub

    Public Sub SUB_EXIT()
        Call Me.Close()
    End Sub
#End Region

#Region "その他"
    Private Sub SUB_CHECK_PROCESS()
        If BLN_DONE_SUB_CHECK_PROCESS Then
            Exit Sub
        End If
        BLN_DONE_SUB_CHECK_PROCESS = True
        Call System.Windows.Forms.Application.DoEvents()

        Call SUB_GET_PROCESS()
        Call SUB_REFRESH_PROCESS()

        Call Application.DoEvents()
        BLN_DONE_SUB_CHECK_PROCESS = False
    End Sub

    Private Sub SUB_GET_PROCESS()

        Dim PRC_TEMP As Process
        PRC_TEMP = FUNC_GET_PROCESS(SRT_APP_SETTINGS_VALUE.PROCESS_NAME)
        If PRC_APP_TARGET Is Nothing Then
            PRC_APP_TARGET = PRC_TEMP
        Else
            If PRC_TEMP Is Nothing Then
                PRC_APP_TARGET = PRC_TEMP
            Else
                If Not (PRC_APP_TARGET.MainWindowHandle = PRC_TEMP.MainWindowHandle) Then
                    PRC_APP_TARGET = PRC_TEMP
                End If
            End If
        End If
    End Sub

    Private Sub SUB_REFRESH_PROCESS()

        Dim BLN_PROCESS As Boolean
        BLN_PROCESS = Not (PRC_APP_TARGET Is Nothing)

        Dim STR_TEXT_NTI As String
        STR_TEXT_NTI = ""
        If BLN_PROCESS Then
            STR_TEXT_NTI &= "process hit." & System.Environment.NewLine
            STR_TEXT_NTI &= "id:" & FUNC_GET_ID_APP_TARGET()
        Else
            STR_TEXT_NTI &= "process search..."
        End If
        NTI_TASK.Text = STR_TEXT_NTI

        If BLN_PROCESS Then
            If WPF_WINDOW_MAIN Is Nothing Then
                WPF_WINDOW_MAIN = New WPF_MAIN
            End If
            Select Case WPF_WINDOW_MAIN.Visibility
                Case System.Windows.Visibility.Collapsed, System.Windows.Visibility.Hidden
                    Call WPF_WINDOW_MAIN.Show()
                Case Else
            End Select
            Dim ENM_ALIGNMENT As ENM_POSITION_WPF_LOCATION
            ENM_ALIGNMENT = FUNC_GET_LOCATION_ALIGNMENT_FROM_STRING(SRT_APP_SETTINGS_VALUE.GUIDE.LOCATION.ALIGNMENT)
            Call SUB_SET_LOCATION_OVERLAY_WPF(WPF_WINDOW_MAIN, ENM_ALIGNMENT)
        Else
            If Not WPF_WINDOW_MAIN Is Nothing Then
                Call WPF_WINDOW_MAIN.Hide()
            End If
        End If
    End Sub

    Private Sub SUB_HIDE()
        If Not Me.Visible Then
            Exit Sub
        End If

        Call Me.Hide()
        Me.Visible = False
        Call Application.DoEvents()
    End Sub
#End Region

#Region "通信用"
    Public Sub SUB_RECEIVE_MESSAGE(ByVal STR_MESSAGE As String)
        Call SUB_PROCESS_MESSAGE(STR_MESSAGE)
    End Sub

    Private Sub SUB_PROCESS_MESSAGE(ByVal STR_MESSAGE As String)
        Dim STR_SEP As String
        STR_SEP = " "

        Dim STR_MESSAGE_ROW() As String
        STR_MESSAGE_ROW = STR_MESSAGE.Split(STR_SEP)

        Dim STR_CONTENT As String
        Dim STR_DETAIL As String

        Try
            STR_CONTENT = STR_MESSAGE_ROW(0)
            STR_DETAIL = STR_MESSAGE_ROW(1)
        Catch ex As Exception
            Exit Sub
        End Try

        Select Case STR_CONTENT
            Case "EVENT"
                Call SUB_PROCESS_MESSAGE_EVENT(STR_DETAIL)
            Case Else

        End Select

    End Sub

    Private Sub SUB_PROCESS_MESSAGE_EVENT(ByVal STR_EVENT_DETAIL As String)
        Call Debug.WriteLine(STR_EVENT_DETAIL)

        Select Case STR_EVENT_DETAIL
            Case "SEND_KEY_W"
                Call SUB_SEND_KEYS_W(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_A"
                Call SUB_SEND_KEYS_A(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_S"
                Call SUB_SEND_KEYS_S(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_D"
                Call SUB_SEND_KEYS_D(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_PAGE_UP"
                Call SUB_SEND_KEYS_PGUP(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.PAGEUD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_PAGE_DOWN"
                Call SUB_SEND_KEYS_PGDN(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.PAGEUD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_ALLOW_UP"
                Call SUB_SEND_KEYS_UP(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_ALLOW_LEFT"
                Call SUB_SEND_KEYS_LEFT(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_ALLOW_RIGHT"
                Call SUB_SEND_KEYS_RIGHT(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "SEND_KEY_ALLOW_DOWN"
                Call SUB_SEND_KEYS_DOWN(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
            Case "CAPTURE_IMAGE"
                Call SUB_CAPTURE()
            Case Else

        End Select

    End Sub

    Private Sub SUB_CAPTURE()

        Dim BMP_PROCESS_CLIENT As System.Drawing.Bitmap
        BMP_PROCESS_CLIENT = Nothing

        Dim GRP_PROCESS_CLIENT As System.Drawing.Graphics
        GRP_PROCESS_CLIENT = Nothing

        Call SUB_PRINT_CLIENT_WINDOW(PRC_APP_TARGET, GRP_PROCESS_CLIENT, BMP_PROCESS_CLIENT)

        Dim BMP_MAKE As System.Drawing.Bitmap
        BMP_MAKE = BMP_PROCESS_CLIENT

        Call SUB_FIXED_PHRASE_INIT(Now, SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX)
        Dim SRT_SAVE As SRT_SAVE_IMAGE_PARAM
        With SRT_SAVE
            .SAVE_DIR = FUNC_GET_NAME_SAVE_DIR(SRT_APP_SETTINGS_VALUE.SAVE.DIRECTORY, SRT_APP_SETTINGS_VALUE.SAVE.FILE.DIRECTORY)
            .SAVE_NAME_FILE = FUNC_GET_NAME_SAVE_FILE(SRT_APP_SETTINGS_VALUE.SAVE.FILE.NAME)
            .TYPE = FUNC_GET_TYPE_IMAGE01_FROM_STRING(SRT_APP_SETTINGS_VALUE.SAVE.FILE.TYPE)
            .QUALITY = SRT_APP_SETTINGS_VALUE.SAVE.FILE.QUALITY
            .ADD_CR = FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.SAVE.FILE.COPYRIGHT)
            .EXIF = FUNC_GET_EXIF_DEFAULT(SRT_APP_SETTINGS_VALUE.PROCESS_NAME, Now)
            .SAVE_NAME_FILE_REAL = FUNC_GET_NAME_FILE_REAL(.SAVE_DIR, .SAVE_NAME_FILE)
        End With

        If Not FUNC_SAVE_IMAGE(BMP_MAKE, SRT_SAVE) Then
            Exit Sub
        End If

        Dim SRT_SAVE_02 As SRT_SAVE_IMAGE_PARAM
        With SRT_SAVE_02
            .SAVE_DIR = SRT_SAVE.SAVE_DIR
            .SAVE_NAME_FILE = SRT_SAVE.SAVE_NAME_FILE
            .TYPE = FUNC_GET_TYPE_IMAGE02_FROM_STRING(SRT_APP_SETTINGS_VALUE.SAVE.FILE.TYPE)
            .QUALITY = SRT_SAVE.QUALITY
            .ADD_CR = True
            .EXIF = SRT_SAVE.EXIF
            .SAVE_NAME_FILE_REAL = SRT_SAVE.SAVE_NAME_FILE_REAL
        End With
        If Not FUNC_SAVE_IMAGE(BMP_MAKE, SRT_SAVE_02) Then
            Exit Sub
        End If

        SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX += 1
        Call My.Computer.Audio.Play(My.Resources.SHUTTER_02_SHORT, Microsoft.VisualBasic.AudioPlayMode.Background)
    End Sub
#End Region

#Region "NEW"
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Call SUB_CTRL_NEW_INIT()
    End Sub
#End Region

#Region "イベント-ウィンドウメッセージ"
    Private STE_WND_PROC As Stopwatch
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Const CST_WM_HOTKEY As Integer = &H312

        Dim BLN_EXIT As Boolean
        Select Case m.Msg
            Case CST_WM_HOTKEY
                BLN_EXIT = False
            Case Else '対象のメッセージ種別以外は
                BLN_EXIT = True 'EXIT
        End Select

        If BLN_EXIT Then
            Call MyBase.WndProc(m) '上位へ
            Exit Sub '何もしない
        End If

        Dim INT_ELAPSED As Integer
        Const CST_WAIT_MSEC As Integer = 300 '連打を受取らない時間

        If STE_WND_PROC Is Nothing Then '初期呼出時
            STE_WND_PROC = New System.Diagnostics.Stopwatch
            Call STE_WND_PROC.Start()
            INT_ELAPSED = CST_WAIT_MSEC + 1
        Else
            Call STE_WND_PROC.Stop()
            INT_ELAPSED = STE_WND_PROC.ElapsedMilliseconds
        End If

        Dim BLN_DO As Boolean
        BLN_DO = (INT_ELAPSED > CST_WAIT_MSEC)

        If BLN_DO Then
            '処理を行う
            Call STE_WND_PROC.Restart() '0から始動
        Else
            Call STE_WND_PROC.Start() '処理時間を除いて再始動
        End If

        Call MyBase.WndProc(m) '上位処理も行う
    End Sub

#End Region

#Region "イベント-アイテムクリック"

    Private Sub TSM_SETTING_Click(sender As Object, e As EventArgs) Handles TSM_SETTING.Click
        Call SUB_SETTING()
    End Sub

    Private Sub TSM_OPEN_SAVE_FOLDER_Click(sender As Object, e As EventArgs) Handles TSM_OPEN_SAVE_FOLDER.Click
        Call SUB_OPEN_SAVE_FOLDER()
    End Sub

    Private Sub TSM_MOBILE_Click(sender As Object, e As EventArgs) Handles TSM_MOBILE.Click
        Call SUB_MOBILE()
    End Sub

    Private Sub TSM_EXIT_Click(sender As Object, e As EventArgs) Handles TSM_EXIT.Click
        Call SUB_EXIT()
    End Sub
#End Region

#Region "イベント-タイマー"
    Private Sub TIM_CHECK_PROCESS_Tick(sender As Object, e As EventArgs) Handles TIM_CHECK_PROCESS.Tick
        Call SUB_CHECK_PROCESS()
    End Sub
#End Region

    Private Sub FRM_MAIN_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call SUB_CTRL_VIEW_INIT()
        Call SUB_CTRL_VALUE_INIT()
    End Sub
    Private Sub FRM_MAIN_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Call SUB_CTRL_ACTIVATE_INIT()
        Call SUB_HIDE()
    End Sub

    Private Sub FRM_MAIN_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Me.Enabled = False
        Call Application.DoEvents()
    End Sub

    Private Sub FRM_MAIN_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Call SUB_CTRL_VALUE_FIN()
        Call SUB_CTRL_VIEW_FIN()
    End Sub

    Private Sub FRM_MAIN_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Call SUB_CTRL_NEW_FIN()
    End Sub

End Class
