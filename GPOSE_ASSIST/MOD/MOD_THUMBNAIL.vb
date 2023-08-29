Module MOD_THUMBNAIL

#Region "モジュール用変数"
    Friend FRM_PARENT As FRM_MAIN
    Private WPF_MY_WINDOW As WPF_COPY_WINDOW
#End Region

#Region "PUBLIC"
    Public Sub SUB_CALL_THUBNAIL(ByRef FRM_CALL As FRM_MAIN)
        If FUNC_CHECK_WINDOW() Then
            Call SUB_CLOSE_THUBNAIL()
        Else
            Call SUB_SHOW_THUBNAIL(FRM_CALL)
        End If
    End Sub

    Public Sub SUB_RECALL_THUBNAIL(ByRef FRM_CALL As FRM_MAIN)

        If Not FUNC_CHECK_WINDOW() Then
            Exit Sub
        End If

        Call SUB_CLOSE_THUBNAIL()
        Call SUB_SHOW_THUBNAIL(FRM_CALL)
    End Sub

    Public Sub SUB_SHOW_THUBNAIL(ByRef FRM_CALL As FRM_MAIN)
        If PRC_APP_TARGET Is Nothing Then
            Exit Sub
        End If

        If FUNC_CHECK_WINDOW() Then
            Exit Sub
        End If

        WPF_MY_WINDOW = New WPF_COPY_WINDOW

        'Call SUB_SET_LOCATION_OVERLAY_WPF(WPF_MY_WINDOW, ENM_POSITION_WPF_LOCATION.LEFT_TOP)
        Call WPF_MY_WINDOW.Show()
        Call WPF_MY_WINDOW.SUB_INIT_THUBNAIL()

        'WPF_MY_WINDOW.Topmost = True

        'Call SUB_FOREGROUND_WINDOW(PRC_APP_TARGET)
    End Sub

    Public Sub SUB_CLOSE_THUBNAIL()

        If Not FUNC_CHECK_WINDOW() Then
            Exit Sub
        End If

        Try
            Call WPF_MY_WINDOW.Close()
            WPF_MY_WINDOW = Nothing
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Private BLN_SET_ACTIVATE As Boolean = False
    Public Sub SUB_SET_TOPMOST_THUBNAIL(ByVal BLN_TOPMOST As Boolean, ByVal BLN_ACTIVATE As Boolean)
        If WPF_MY_WINDOW Is Nothing Then
            Exit Sub
        End If

        WPF_MY_WINDOW.Topmost = BLN_TOPMOST
        If Not BLN_TOPMOST Then
            BLN_SET_ACTIVATE = False
        End If
        If BLN_TOPMOST And BLN_ACTIVATE And Not BLN_SET_ACTIVATE Then
            Call WPF_MY_WINDOW.Activate()
            BLN_SET_ACTIVATE = True
        End If
    End Sub

    Public Function FUNC_GET_HANDLE_THUBNAIL() As IntPtr
        If WPF_MY_WINDOW Is Nothing Then
            Return IntPtr.Zero
        End If

        Dim HLP_WINDOW As System.Windows.Interop.WindowInteropHelper
        Dim INT_RET As IntPtr
        HLP_WINDOW = New System.Windows.Interop.WindowInteropHelper(WPF_MY_WINDOW)
        INT_RET = HLP_WINDOW.Handle
        Return INT_RET
    End Function
#End Region

#Region "PRIVATE"
    Private Function FUNC_CHECK_WINDOW()
        If WPF_MY_WINDOW Is Nothing Then
            Return False
        End If

        If WPF_MY_WINDOW.CHECK_CLOSED Then
            Return False
        End If

        Return True
    End Function
#End Region

End Module
