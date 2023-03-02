Imports System.Windows

Public Class WPF_SETTING

#Region "画面用-列挙定数"
    Private Enum ENM_WINDOW_EXEC
        DO_OK = 0
        DO_CLOSE
        DO_VIEW_DIALOG_SD
        DO_INIT_TRIM
        DO_VIEW_DIALOG_USER_COMP
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
            Case ENM_WINDOW_EXEC.DO_VIEW_DIALOG_SD
                Call SUB_VIEW_DIALOG_SD()
            Case ENM_WINDOW_EXEC.DO_INIT_TRIM
                Call SUB_INIT_TRIM()
            Case ENM_WINDOW_EXEC.DO_VIEW_DIALOG_USER_COMP
                Call SUB_VIEW_DIALOG_USER_COMP()
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
        Me.CANCEL = False
        Call SUB_CLOSE()
    End Sub

    Private Sub SUB_CLOSE()
        Call Me.Close()
    End Sub

    Private Sub SUB_VIEW_DIALOG_SD()
        Dim FBD_FLODER As FolderBrowserDialog
        FBD_FLODER = New FolderBrowserDialog
        With FBD_FLODER
            .Description = "Select Save Folder"
            .SelectedPath = TXT_SAVE_DIRECTORY.Text
        End With

        Dim DLR_RETURN As DialogResult
        DLR_RETURN = FBD_FLODER.ShowDialog()

        If Not (DLR_RETURN = Forms.DialogResult.OK) Then
            Exit Sub
        End If

        With FBD_FLODER
            TXT_SAVE_DIRECTORY.Text = .SelectedPath
        End With
    End Sub

    Private Sub SUB_INIT_TRIM()
        TXT_TRIM_LOACTION_LEFT.Text = CStr(0)
        TXT_TRIM_LOACTION_TOP.Text = CStr(0)

        TXT_TRIM_SIZE_WIDTH.Text = CStr(600)
        TXT_TRIM_SIZE_HEIGHT.Text = CStr(400)

        CMB_TRIM_ASPECT_RATIO_TYPE.SelectedIndex = 3

        CMB_TRIM_COMPOTION_TYPE.SelectedIndex = 2
    End Sub

    Private Sub SUB_VIEW_DIALOG_USER_COMP()
        Dim WPF_SHOW As WPF_USER_COMPOTION

        WPF_SHOW = New WPF_USER_COMPOTION
        Dim INT_INDEX As Integer
        INT_INDEX = (CMB_TRIM_COMPOTION_TYPE_USER.SelectedIndex + 1)
        WPF_SHOW.CUSTOMIZE = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(INT_INDEX)
        Call WPF_SHOW.ShowDialog()

        If Not WPF_SHOW.CANCEL Then
            'If Not WPF_WINDOW_MAIN Is Nothing Then
            '    Call WPF_WINDOW_MAIN.SUB_WINDOW_REFRESH()
            'End If
        End If

        Call WPF_SHOW.Close()
        WPF_SHOW = Nothing
    End Sub
#End Region

#Region "初期化・終了処理"
    Private Sub SUB_CTRL_VIEW_INIT()
        Call SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE(CMB_TRIM_COMPOTION_TYPE)
        Call SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE_USER(CMB_TRIM_COMPOTION_TYPE_USER)
    End Sub

    Private Sub SUB_CTRL_VALUE_INIT()
        Me.CANCEL = True
        Call SUB_SET_SETTING_TO_CONTROL(SRT_APP_SETTINGS_VALUE)
        Call SUB_SET_COMBO_KIND_CODE_FIRST(CMB_TRIM_COMPOTION_TYPE_USER)
    End Sub
#End Region

#Region "構図補助プルダウン初期化"
    Private Sub SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE(ByRef CMB_MAIN As Controls.ComboBox)
        With CMB_MAIN
            Call .Items.Clear()

            Call SUB_ADD_ITEMS_COMPOTION_TYPE_ONLY(.Items)

            Dim STR_NAME() As String
            STR_NAME = FUNC_GET_STR_ARRAY_FROM_COMPOTION_TYPE_USER(SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER)
            Call SUB_ADD_ITEMS_COMPOTION_TYPE_USER(.Items, STR_NAME)

        End With
    End Sub

    Private Sub SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE_USER(ByRef CMB_MAIN As Controls.ComboBox)
        With CMB_MAIN
            Call .Items.Clear()

            Dim STR_NAME() As String
            STR_NAME = FUNC_GET_STR_ARRAY_FROM_COMPOTION_TYPE_USER(SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER)
            Call SUB_ADD_ITEMS_COMPOTION_TYPE_USER(.Items, STR_NAME)
        End With
    End Sub

    Private Function FUNC_GET_STR_ARRAY_FROM_COMPOTION_TYPE_USER(ByRef SRT_CONF() As SRT_APP_SETTINGS_TRIM_COMPOTION_USER) As String()
        Dim STR_RET() As String
        ReDim STR_RET(0)

        Dim INT_INDEX As Integer
        INT_INDEX = (SRT_CONF.Length - 1)
        For i = 1 To INT_INDEX
            ReDim Preserve STR_RET(i)
            STR_RET(i) = SRT_CONF(i).NAME
        Next
        Return STR_RET
    End Function
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
            TXT_SAVE_FILE_INDEX.Text = .FILE.INDEX
            CHK_SAVE_FILE_COPYRIGHT.IsChecked = FUNC_CAST_INT_TO_BOOL(.FILE.COPYRIGHT)
        End With
        With SRT_SET.GUIDE
            CMB_GUIDE_LOCATION_ALIGNMENT.Text = .LOCATION.ALIGNMENT
        End With
        With SRT_SET.TRIM
            TXT_TRIM_LOACTION_LEFT.Text = .LOCATION.LEFT
            TXT_TRIM_LOACTION_TOP.Text = .LOCATION.TOP
            TXT_TRIM_SIZE_WIDTH.Text = .SIZE.WIDTH
            TXT_TRIM_SIZE_HEIGHT.Text = .SIZE.HEIGHT
            CMB_TRIM_ASPECT_RATIO_TYPE.SelectedIndex = .ASPECT_RATIO.TYPE
            CMB_TRIM_COMPOTION_TYPE.SelectedIndex = .COMPOTION.TYPE
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
            If .FILE.QUALITY > 100 Then
                .FILE.QUALITY = 100
            End If
            If .FILE.QUALITY <= 0 Then
                .FILE.QUALITY = 10
            End If
            .FILE.INDEX = FUNC_VALUE_CONVERT_NUMERIC_INT(TXT_SAVE_FILE_INDEX.Text, 1)
            If .FILE.INDEX <= 0 Then
                .FILE.QUALITY = 1
            End If
            .FILE.COPYRIGHT = FUNC_CAST_BOOL_TO_INT(CHK_SAVE_FILE_COPYRIGHT.IsChecked)
        End With
        With SRT_RET.GUIDE
            .LOCATION.ALIGNMENT = CMB_GUIDE_LOCATION_ALIGNMENT.Text
        End With
        With SRT_RET.TRIM
            .LOCATION.LEFT = FUNC_VALUE_CONVERT_NUMERIC_INT(TXT_TRIM_LOACTION_LEFT.Text, 0)
            .LOCATION.TOP = FUNC_VALUE_CONVERT_NUMERIC_INT(TXT_TRIM_LOACTION_TOP.Text, 0)
            .SIZE.WIDTH = FUNC_VALUE_CONVERT_NUMERIC_INT(TXT_TRIM_SIZE_WIDTH.Text, 400)
            .SIZE.HEIGHT = FUNC_VALUE_CONVERT_NUMERIC_INT(TXT_TRIM_SIZE_HEIGHT.Text, 300)
            .ASPECT_RATIO.TYPE = CMB_TRIM_ASPECT_RATIO_TYPE.SelectedIndex
            .COMPOTION.TYPE = CMB_TRIM_COMPOTION_TYPE.SelectedIndex

            ReDim .COMPOTION.USER(CST_APP_CONFIG_TRIM_COMPOTION_USER_ITEM_COUNT)
            For i = 1 To (.COMPOTION.USER.Length - 1) 'USER SET
                ReDim .COMPOTION.USER(i).TYPE(4)
                .COMPOTION.USER(i).NAME = "あいうえお" & i
                For j = 1 To (.COMPOTION.USER(i).TYPE.Length - 1)
                    .COMPOTION.USER(i).TYPE(j) = j
                Next
                .COMPOTION.USER(i).BASE = .COMPOTION.USER(i).FUNC_GET_BASE
            Next
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

#Region "イベント-ボタンクリック"

    Private Sub BTN_OK_Click(sender As Object, e As RoutedEventArgs) Handles BTN_OK.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_OK)
    End Sub

    Private Sub BTN_CANCEL_Click(sender As Object, e As RoutedEventArgs) Handles BTN_CANCEL.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CLOSE)
    End Sub

    Private Sub BTN_SAVE_DIRECTORY_DIALOG_Click(sender As Object, e As RoutedEventArgs) Handles BTN_SAVE_DIRECTORY_DIALOG.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_VIEW_DIALOG_SD)
    End Sub

    Private Sub BTN_INIT_TRIM_Click(sender As Object, e As RoutedEventArgs) Handles BTN_INIT_TRIM.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_INIT_TRIM)
    End Sub

    Private Sub BTN_CUSTOM_TRIM_COMPOTION_TYPE_USER_Click(sender As Object, e As RoutedEventArgs) Handles BTN_CUSTOM_TRIM_COMPOTION_TYPE_USER.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_VIEW_DIALOG_USER_COMP)
    End Sub
#End Region

    Private Sub WPF_SETTING_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CTRL_VIEW_INIT()
        Call SUB_CTRL_VALUE_INIT()
    End Sub

End Class
