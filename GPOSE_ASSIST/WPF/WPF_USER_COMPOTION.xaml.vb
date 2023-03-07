Imports System.Windows

Public Class WPF_USER_COMPOTION

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
    Private SRT_CUSTOMIZE As SRT_APP_SETTINGS_TRIM_COMPOTION_USER
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

    Friend Property CUSTOMIZE As SRT_APP_SETTINGS_TRIM_COMPOTION_USER
        Get
            Return SRT_CUSTOMIZE
        End Get
        Set(ByVal value As SRT_APP_SETTINGS_TRIM_COMPOTION_USER)
            SRT_CUSTOMIZE = value
        End Set
    End Property
#End Region

#Region "初期化・終了処理"
    Private Sub SUB_CTRL_VIEW_INIT()
        Call SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE_ONLY(CMB_TRIM_COMPOTION_TYPE_01, True)
        Call SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE_ONLY(CMB_TRIM_COMPOTION_TYPE_02, True)
        Call SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE_ONLY(CMB_TRIM_COMPOTION_TYPE_03, True)
        Call SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE_ONLY(CMB_TRIM_COMPOTION_TYPE_04, True)
    End Sub

    Private Sub SUB_CTRL_VALUE_INIT()
        Me.CANCEL = True

        Call SUB_SET_VALUE_TO_CONTROL(Me.CUSTOMIZE)


    End Sub

#End Region

#Region "プルダウン関連"

    Private Sub SUB_INIT_COMBO_BOX_ITEM_COMPOTION_TYPE_ONLY(ByRef CMB_MAIN As Controls.ComboBox, Optional ByVal BLN_NONE As Boolean = False)
        With CMB_MAIN
            Call .Items.Clear()

            If BLN_NONE Then
                Call .Items.Add("無し")
            End If
            Call SUB_ADD_ITEMS_COMPOTION_TYPE_ONLY(.Items)
        End With
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
        Me.CANCEL = False
        Me.CUSTOMIZE = FUNC_GET_VALUE_FROM_CONTROL()
        Call SUB_CLOSE()
    End Sub

    Private Sub SUB_CLOSE()
        Call Me.Close()
    End Sub

#End Region

#Region "構造体←→画面コントロール"
    Private Sub SUB_SET_VALUE_TO_CONTROL(ByRef SRT_VALUE As SRT_APP_SETTINGS_TRIM_COMPOTION_USER)

        TXT_NAME.Text = SRT_VALUE.NAME
        CMB_TRIM_COMPOTION_TYPE_01.SelectedIndex = (SRT_VALUE.TYPE(1))
        CMB_TRIM_COMPOTION_TYPE_02.SelectedIndex = (SRT_VALUE.TYPE(2))
        CMB_TRIM_COMPOTION_TYPE_03.SelectedIndex = (SRT_VALUE.TYPE(3))
        CMB_TRIM_COMPOTION_TYPE_04.SelectedIndex = (SRT_VALUE.TYPE(4))
    End Sub

    Private Function FUNC_GET_VALUE_FROM_CONTROL() As SRT_APP_SETTINGS_TRIM_COMPOTION_USER
        Dim SRT_RET As SRT_APP_SETTINGS_TRIM_COMPOTION_USER

        SRT_RET = Nothing
        With SRT_RET
            .NAME = TXT_NAME.Text
            ReDim .TYPE(4)
            .TYPE(1) = (CMB_TRIM_COMPOTION_TYPE_01.SelectedIndex)
            .TYPE(2) = (CMB_TRIM_COMPOTION_TYPE_02.SelectedIndex)
            .TYPE(3) = (CMB_TRIM_COMPOTION_TYPE_03.SelectedIndex)
            .TYPE(4) = (CMB_TRIM_COMPOTION_TYPE_04.SelectedIndex)
            .BASE = .FUNC_GET_BASE
        End With

        Return SRT_RET
    End Function
#End Region

#Region "イベント-ボタンクリック"
    Private Sub BTN_OK_Click(sender As Object, e As RoutedEventArgs) Handles BTN_OK.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_OK)
    End Sub

    Private Sub BTN_CANCEL_Click(sender As Object, e As RoutedEventArgs) Handles BTN_CANCEL.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CLOSE)
    End Sub
#End Region

    Private Sub WPF_USER_COMPOTION_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CTRL_VIEW_INIT()
        Call SUB_CTRL_VALUE_INIT()
    End Sub
End Class
