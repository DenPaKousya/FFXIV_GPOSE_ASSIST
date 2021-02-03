Imports System.Windows

Public Class WPF_SETTING

#Region "画面用-列挙定数"
    Private Enum ENM_WINDOW_EXEC
        DO_OK = 0
        DO_CLOSE
    End Enum
#End Region

#Region "画面用・変数"
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
        SRT_APP_SETTINGS_VALUE = FUNC_GET_SETTING_FROM_CONTROL()
        Call SUB_CLOSE()
    End Sub

    Private Sub SUB_CLOSE()
        Call Me.Close()
    End Sub

#End Region

#Region "初期化・終了処理"
    Private Sub SUB_CTRL_VIEW_INIT()

    End Sub

    Private Sub SUB_CTRL_VALUE_INIT()
        Call SUB_SET_SETTING_TO_CONTROL(SRT_APP_SETTINGS_VALUE)
    End Sub
#End Region

#Region "設定←→画面コントロール"
    Private Sub SUB_SET_SETTING_TO_CONTROL(ByRef SRT_SET As SRT_APP_SETTINGS)
        With SRT_SET
            CMB_NAME_PROCESS.Text = .PROCESS_NAME
        End With
        With SRT_SET.SAVE
            TXT_SAVE_DIRECTORY.Text = .DIRECTORY
            TXT_SAVE_FILE_DIRECTORY.Text = .FILE.DIRECTORY
            TXT_SAVE_FILE_NAME.Text = .FILE.NAME
            CMB_SAVE_FILE_TYPE.Text = .FILE.TYPE
            CMB_SAVE_FILE_QUALITY.Text = .FILE.QUALITY
            CHK_SAVE_FILE_COPYRIGHT.IsChecked = FUNC_CAST_INT_TO_BOOL(.FILE.COPYRIGHT)
        End With
        With SRT_SET.GUIDE
            CMB_GUIDE_LOCATION_ALIGNMENT.Text = .LOCATION.ALIGNMENT
        End With
        With SRT_SET.TRIM
            TXT_TRIM_LOACTION_LEFT.Text = .LOCATION.LEFT
            TXT_TRIM_LOACTION_TOP.Text = .LOCATION.TOP
            CMB_NAME_PROCESS.SelectedIndex = .COMPOTION.TYPE
        End With
    End Sub

    Private Function FUNC_GET_SETTING_FROM_CONTROL()
        Dim SRT_RET As SRT_APP_SETTINGS
        With SRT_RET
            .PROCESS_NAME = CMB_NAME_PROCESS.Text
        End With
        With SRT_RET.SAVE
            .DIRECTORY = TXT_SAVE_DIRECTORY.Text
            .FILE.DIRECTORY = TXT_SAVE_FILE_DIRECTORY.Text
            .FILE.NAME = TXT_SAVE_FILE_NAME.Text
            .FILE.TYPE = CMB_SAVE_FILE_TYPE.Text
            .FILE.QUALITY = FUNC_VALUE_CONVERT_NUMERIC_INT(CMB_SAVE_FILE_QUALITY.Text, 100)
            .FILE.COPYRIGHT = FUNC_CAST_BOOL_TO_INT(CHK_SAVE_FILE_COPYRIGHT.IsChecked)
        End With
        With SRT_RET.GUIDE
            .LOCATION.ALIGNMENT = CMB_GUIDE_LOCATION_ALIGNMENT.Text
        End With
        With SRT_RET.TRIM
            .LOCATION.LEFT = FUNC_VALUE_CONVERT_NUMERIC_INT(TXT_TRIM_LOACTION_LEFT.Text, 0)
            .LOCATION.TOP = FUNC_VALUE_CONVERT_NUMERIC_INT(TXT_TRIM_LOACTION_TOP.Text, 0)
            .COMPOTION.TYPE = CMB_NAME_PROCESS.SelectedIndex
            .SIZE.WIDTH = 0
            .SIZE.HEIGHT = 0
        End With

        Return SRT_RET
    End Function
#End Region

#Region "イベント-コンボチェンジ"
    Private Sub CMB_NAME_PROCESS_SelectionChanged(sender As Object, e As Controls.SelectionChangedEventArgs) Handles CMB_NAME_PROCESS.SelectionChanged
        Dim CIM_ITEM As Controls.ComboBoxItem
        CIM_ITEM = e.AddedItems(0)

        Dim STR_TEMP As String
        STR_TEMP = CIM_ITEM.Content
        LBL_NAME_APPLICATION.Content = FUNC_GET_APPLICATION_NAME(STR_TEMP)
    End Sub
#End Region

    Private Sub WPF_SETTING_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CTRL_VIEW_INIT()
        Call SUB_CTRL_VALUE_INIT()
    End Sub


End Class
