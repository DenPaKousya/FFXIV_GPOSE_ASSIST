Imports System.Windows
Imports System.Windows.Input

Class WPF_MOBILE_SETTING

#Region "画面用-列挙定数"
    Private Enum ENM_WINDOW_EXEC
        DO_OK = 0
        DO_CLOSE
    End Enum
#End Region

#Region "画面用・変数"
    Private BLN_WINDOW_EXEC_DO As Boolean = False
#End Region

#Region "プロパティ用変数"
    Private BLN_PROPERTY_CANCEL As Boolean = True
#End Region

#Region "プロパティ"

    Public Property CANCEL As Boolean
        Get
            Return BLN_PROPERTY_CANCEL
        End Get
        Set(ByVal value As Boolean)
            BLN_PROPERTY_CANCEL = value
        End Set
    End Property

#End Region

#Region "主処理呼出元"

    Private Sub SUB_EXEC_DO(ByVal ENM_WINDOW_EXEC As ENM_WINDOW_EXEC)

        If BLN_WINDOW_EXEC_DO Then
            Exit Sub
        End If
        BLN_WINDOW_EXEC_DO = True
        Call Me.DoEvents()

        Select Case ENM_WINDOW_EXEC
            Case ENM_WINDOW_EXEC.DO_OK
                Call SUB_OK()
            Case ENM_WINDOW_EXEC.DO_CLOSE
                Call SUB_CLOSE()
        End Select

        Call Me.DoEvents()
        BLN_WINDOW_EXEC_DO = False
    End Sub

#Region "DoEvents"

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

#End Region

#Region "主処理"
    Private Sub SUB_OK()
        'SRT_APP_SETTINGS_VALUE = FUNC_GET_SETTING_FROM_CONTROL()
        Me.CANCEL = False
        Call SUB_CLOSE()
    End Sub

    Private Sub SUB_CLOSE()
        Call Me.Close()
    End Sub
#End Region

#Region "初期化・終了処理"
    Private Sub SUB_CTRL_VIEW_INIT()
        Call SUB_INIT_COMMBO_BOX_KIND(CMB_SEND_COMMAND_00_KIND)
        Call SUB_INIT_COMMBO_BOX_KEY_MASK(CMB_SEND_COMMAND_00_KEY_01)
        Call SUB_INIT_COMMBO_BOX_KEY(CMB_SEND_COMMAND_00_KEY_02)
    End Sub

    Private Sub SUB_CTRL_VALUE_INIT()
        Me.CANCEL = True
        Call SUB_SET_COMBO_KIND_CODE_FIRST(CMB_SEND_COMMAND_00_KIND)
        Call SUB_SET_COMBO_KIND_CODE_FIRST(CMB_SEND_COMMAND_00_KEY_01)
        Call SUB_SET_COMBO_KIND_CODE_FIRST(CMB_SEND_COMMAND_00_KEY_02)
    End Sub
#End Region

#Region "イベント-セレクションチェンジ"
    Private Sub CMB_SEND_COMMAND_00_KIND_SelectionChanged(sender As Object, e As Controls.SelectionChangedEventArgs) Handles CMB_SEND_COMMAND_00_KIND.SelectionChanged
        Call SUB_CHANGE_KIND(sender)
    End Sub

    Private Sub SUB_CHANGE_KIND(ByRef CMB_KIND As System.Windows.Controls.ComboBox)
        Dim INT_KIND As Integer
        INT_KIND = CMB_KIND.SelectedIndex

        Dim BLN_ENABLED_CHAT As Boolean
        Dim BLN_ENABLED_KEY As Boolean
        Dim BLN_ENABLED_MOUSE As Boolean
        Select Case INT_KIND
            Case 0
                BLN_ENABLED_CHAT = True
                BLN_ENABLED_KEY = False
                BLN_ENABLED_MOUSE = False
            Case 1
                BLN_ENABLED_CHAT = False
                BLN_ENABLED_KEY = True
                BLN_ENABLED_MOUSE = False
            Case 2
                BLN_ENABLED_CHAT = False
                BLN_ENABLED_KEY = False
                BLN_ENABLED_MOUSE = True
            Case Else
                BLN_ENABLED_CHAT = False
                BLN_ENABLED_KEY = False
                BLN_ENABLED_MOUSE = False
        End Select

        'Chat
        TXT_SEND_COMMAND_00_CHAT.IsEnabled = BLN_ENABLED_CHAT
        'Key
        CMB_SEND_COMMAND_00_KEY_01.IsEnabled = BLN_ENABLED_KEY
        CMB_SEND_COMMAND_00_KEY_02.IsEnabled = BLN_ENABLED_KEY
        'Mouse
        TXT_SEND_COMMAND_00_MOUSE_X.IsEnabled = BLN_ENABLED_MOUSE
        TXT_SEND_COMMAND_00_MOUSE_Y.IsEnabled = BLN_ENABLED_MOUSE
    End Sub
#End Region

#Region "イベント-ボタンクリック"
    Private Sub BTN_OK_Click(sender As Object, e As RoutedEventArgs) Handles BTN_OK.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_OK)
    End Sub

    Private Sub BTN_CANCEL_Click(sender As Object, e As RoutedEventArgs) Handles BTN_CANCEL.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CLOSE)
    End Sub
#End Region

    Private Sub WPF_MOBILE_SETTING_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CTRL_VIEW_INIT()
        Call SUB_CTRL_VALUE_INIT()
    End Sub

End Class
