Imports System.Windows
Imports System.Windows.Interop

Public Class WPF_MAIN

#Region "画面用・列挙定数"
    Private Enum ENM_WINDOW_EXEC
        VIEW_TRIM = 0
        VIEW_COPY
        APPL_EXIT
        APPL_SETTING
        APPL_ROTATE_WINDOW
        APPL_OPEN_SAVE_FOLDER
    End Enum
#End Region

#Region "画面用・変数"
    Private WPF_SHOW_WINDOW As WPF_TRIM = Nothing
    Private BLN_WINDOW_EXEC_DO As Boolean = False
#End Region

#Region "初期化・終了処理"
    Private Sub SUB_CTRL_NEW_INIT()
    End Sub

    Private Sub SUB_CTRL_VIEW_INIT()
        Dim WIH_HELPER As System.Windows.Interop.WindowInteropHelper
        WIH_HELPER = New System.Windows.Interop.WindowInteropHelper(Me)

        Dim INT_KEY As Integer
        INT_KEY = CInt(FUNC_GET_KEY_HOTKEY("G"))
        Dim INT_MOD As Integer
        INT_MOD = FUNC_GET_MOD_HOTKEY("Alt")
        Dim INT_ID As Integer
        INT_ID = 0
        Dim LNG_PARAM As Long
        LNG_PARAM = 0L
        If Not FUNC_REGISTER_HOT_KEY(WIH_HELPER.Handle, INT_ID, LNG_PARAM, INT_MOD, INT_KEY) Then
            Call Debug.WriteLine("しっぱい")
        End If

        Dim HSC_SOURCE As System.Windows.Interop.HwndSource
        HSC_SOURCE = HwndSource.FromHwnd(WIH_HELPER.Handle)
        Call HSC_SOURCE.AddHook(New HwndSourceHook(AddressOf WndProc))
        'AddHandler ComponentDispatcher.ThreadPreprocessMessage, AddressOf ComponentDispatcher_ThreadPreprocessMessage
    End Sub
#End Region

#Region "主処理呼出元"

    Private Sub SUB_EXEC_DO(ByVal ENM_WINDOW_EXEC As ENM_WINDOW_EXEC)

        If BLN_WINDOW_EXEC_DO Then
            Exit Sub
        End If
        BLN_WINDOW_EXEC_DO = True
        Call Me.DoEvents()

        Select Case ENM_WINDOW_EXEC
            Case ENM_WINDOW_EXEC.VIEW_TRIM
                Call SUB_VIEW_TRIM()
            Case ENM_WINDOW_EXEC.VIEW_COPY
                Call SUB_APPL_VIEW_COPY_WINDOW()
            Case ENM_WINDOW_EXEC.APPL_EXIT
                Call SUB_APPL_EXIT()
            Case ENM_WINDOW_EXEC.APPL_SETTING
                Call SUB_APPL_SETTING()
            Case ENM_WINDOW_EXEC.APPL_ROTATE_WINDOW
                Call SUB_APPL_ROTATE_WINDOW()
            Case ENM_WINDOW_EXEC.APPL_OPEN_SAVE_FOLDER
                Call SUB_APPL_OPEN_SAVE_FOLDER()
        End Select

        Call Me.DoEvents()
        BLN_WINDOW_EXEC_DO = False
    End Sub

    Private Sub DoEvents()
        Dim frame As System.Windows.Threading.DispatcherFrame = New System.Windows.Threading.DispatcherFrame()
        Dim callback As Object

        callback = New System.Windows.Threading.DispatcherOperationCallback(AddressOf ExitFrames)
        System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, callback, frame)
        System.Windows.Threading.Dispatcher.PushFrame(frame)
    End Sub

    Private Function ExitFrames(sender As Object) As Object
        sender.Continue = False
        Return Nothing
    End Function
#End Region

#Region "主処理"
    Private Sub SUB_VIEW_TRIM()

        If WPF_SHOW_WINDOW Is Nothing Then
            WPF_SHOW_WINDOW = New WPF_TRIM
            Call WPF_SHOW_WINDOW.SUB_SET_LOCATION_CONTROL(Me.Height)
        End If

        Dim BLN_VISIBLE As Boolean
        Select Case WPF_SHOW_WINDOW.Visibility
            Case Visibility.Hidden, Visibility.Collapsed
                BLN_VISIBLE = False
            Case Visibility.Visible
                BLN_VISIBLE = True
            Case Else
                BLN_VISIBLE = False
        End Select

        If BLN_VISIBLE Then
            WPF_SHOW_WINDOW.Visibility = Visibility.Hidden
            Call WPF_SHOW_WINDOW.Hide()
        Else
            WPF_SHOW_WINDOW.SET_SIZE = True
            Call WPF_SHOW_WINDOW.Show()
            WPF_SHOW_WINDOW.SET_SIZE = False
            Call WPF_SHOW_WINDOW.SUB_SET_SIZE_AND_LOCATION_DEFAULT()

            Dim BLN_LOCATION_DEFAULT As Boolean
            BLN_LOCATION_DEFAULT = (WPF_SHOW_WINDOW.Left = 0 And WPF_SHOW_WINDOW.Top = 0)
            If BLN_LOCATION_DEFAULT Then
                Call SUB_SET_LOCATION_OVERLAY_WPF(WPF_SHOW_WINDOW, ENM_POSITION_WPF_LOCATION.LEFT_TOP)
            End If
        End If
    End Sub

    Private Sub SUB_APPL_SETTING()
        Call FRM_APPL_MAIN.SUB_SETTING()
    End Sub

    Private Sub SUB_APPL_ROTATE_WINDOW()
        Call FRM_APPL_MAIN.SUB_ROTATE()
    End Sub

    Private Sub SUB_APPL_OPEN_SAVE_FOLDER()
        Call FRM_APPL_MAIN.SUB_OPEN_SAVE_FOLDER()
    End Sub

    Private Sub SUB_APPL_VIEW_COPY_WINDOW()
        Call FRM_APPL_MAIN.SUB_VIEW_COPY_WINDOW()
    End Sub

    Private Sub SUB_APPL_EXIT()
        Call FRM_APPL_MAIN.SUB_EXIT()
    End Sub

#End Region

#Region "外部呼出"
    Public Sub SUB_WINDOW_REFRESH()

        If Not WPF_SHOW_WINDOW Is Nothing Then
            Call WPF_SHOW_WINDOW.Hide()
            Call WPF_SHOW_WINDOW.Close()
            WPF_SHOW_WINDOW = Nothing
        End If


    End Sub
#End Region

#Region "NEW"
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        AddHandler Me.MouseLeftButtonDown, Sub(sender, e) Me.DragMove() 'ウィンドウをマウスのドラッグで移動できるようにする
        Call SUB_CTRL_NEW_INIT()
    End Sub
#End Region

#Region "イベント-ウィンドウメッセージ"
    Private STE_WND_PROC As Stopwatch
    Private Function WndProc(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr, ByRef handled As Boolean) As IntPtr
        Const CST_WM_HOTKEY As Integer = &H312

        Dim BLN_EXIT As Boolean
        Select Case msg
            Case CST_WM_HOTKEY
                BLN_EXIT = False
            Case Else '対象のメッセージ種別以外は
                BLN_EXIT = True 'EXIT
        End Select

        If BLN_EXIT Then
            Return IntPtr.Zero
        End If

        Dim INT_ELAPSED As Integer
        Const CST_WAIT_MSEC As Integer = 100 '連打を受取らない時間

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
            Call SUB_HOTKEY(lParam) '処理を行う
            Call STE_WND_PROC.Restart() '0から始動
        Else
            Call STE_WND_PROC.Start() '処理時間を除いて再始動
        End If

        Return IntPtr.Zero
    End Function

    Private Sub SUB_HOTKEY(ByVal LNG_PARAM As Long)
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.VIEW_TRIM)
    End Sub


    'Private Sub ComponentDispatcher_ThreadPreprocessMessage(ByRef msg As MSG, ByRef handled As Boolean)
    '    Const CST_WM_HOTKEY As Integer = &H312

    '    Dim BLN_EXIT As Boolean
    '    Select Case msg.message
    '        Case CST_WM_HOTKEY
    '            BLN_EXIT = False
    '        Case Else '対象のメッセージ種別以外は
    '            BLN_EXIT = True 'EXIT
    '    End Select

    '    If BLN_EXIT Then
    '        Exit Sub
    '    End If
    'End Sub
#End Region

#Region "イベント-ボタンクリック"
    Private Sub PCB_BUTTON_MAIN_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_MAIN.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.VIEW_TRIM)
    End Sub

#End Region

#Region "イベント-コンテキストメニュークリック"

    Private Sub MNI_SETTING_Click(sender As Object, e As RoutedEventArgs) Handles MNI_SETTING.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.APPL_SETTING)
    End Sub

    Private Sub MNI_ROTATE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_ROTATE.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.APPL_ROTATE_WINDOW)
    End Sub


    Private Sub MNI_OPEN_SAVE_FOLDER_Click(sender As Object, e As RoutedEventArgs) Handles MNI_OPEN_SAVE_FOLDER.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.APPL_OPEN_SAVE_FOLDER)
    End Sub

    Private Sub MNI_EXIT_Click(sender As Object, e As RoutedEventArgs) Handles MNI_EXIT.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.APPL_EXIT)
    End Sub

    Private Sub MNI_COPY_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COPY.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.VIEW_COPY)
    End Sub
#End Region

    Private Sub WPF_MAIN_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CTRL_VIEW_INIT()
    End Sub

End Class
