Imports System.Windows

Public Class WPF_MAIN

#Region "画面用・列挙定数"
    Private Enum ENM_WINDOW_EXEC
        VIEW_TRIM = 0
        APPL_EXIT
        APPL_SETTING
    End Enum
#End Region

#Region "画面用・変数"
    Private WPF_SHOW_WINDOW As WPF_TRIM = Nothing
    Private BLN_WINDOW_EXEC_DO As Boolean = False
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
            Case ENM_WINDOW_EXEC.APPL_EXIT
                Call SUB_APPL_EXIT()
            Case ENM_WINDOW_EXEC.APPL_SETTING

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
            Call WPF_SHOW_WINDOW.Show()
            Call SUB_SET_LOCATION_OVERLAY_WPF(WPF_SHOW_WINDOW, ENM_POSITION_WPF_LOCATION.LEFT_TOP)
        End If
    End Sub

    Private Sub SUB_APPL_SETTING()
        Call FRM_APPL_MAIN.SUB_SETTING()
    End Sub

    Private Sub SUB_APPL_EXIT()
        Call FRM_APPL_MAIN.SUB_EXIT()
    End Sub

#End Region

#Region "NEW"
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        AddHandler Me.MouseLeftButtonDown, Sub(sender, e) Me.DragMove() 'ウィンドウをマウスのドラッグで移動できるようにする
    End Sub

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

    Private Sub MNI_EXIT_Click(sender As Object, e As RoutedEventArgs) Handles MNI_EXIT.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.APPL_EXIT)
    End Sub

#End Region

End Class
