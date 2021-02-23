Imports System.Windows

Public Class WPF_SAVE_CONFIRM

#Region "外部用・構造体"
    Public Structure SRT_RETURN_SAVE_CONFIRM
        Public CANCEL As Boolean
        Public ADD_CR As Boolean
        Public ROTATE As System.Drawing.RotateFlipType
    End Structure
#End Region

#Region "画面用・列挙定数"
    Private Enum ENM_WINDOW_EXEC
        DO_OK = 0
        DO_CANCEL
        DO_ROTATE_R
    End Enum
#End Region

#Region "画面用・変数"
    Private BLN_WINDOW_EXEC_DO As Boolean = False
    Private ENM_WINDOW_ROTATE_CURRENT As System.Drawing.RotateFlipType
#End Region

#Region "プロパティ用変数"
    Private SRT_PROPERTY_RETURN As SRT_RETURN_SAVE_CONFIRM
    Private BMP_PROPERTY_VIEW As System.Drawing.Bitmap
    Private BLN_PROPERTY_ADD_COPYRIGHT_DEFAULT As Boolean
#End Region

#Region "プロパティ"

    Public Property RETURN_CONFIRM As SRT_RETURN_SAVE_CONFIRM
        Get
            Return SRT_PROPERTY_RETURN
        End Get
        Set(ByVal value As SRT_RETURN_SAVE_CONFIRM)
            SRT_PROPERTY_RETURN = value
        End Set
    End Property

    Public Property VIEW_IMAGE As System.Drawing.Bitmap
        Get
            Return BMP_PROPERTY_VIEW
        End Get
        Set(ByVal value As System.Drawing.Bitmap)
            BMP_PROPERTY_VIEW = value
        End Set
    End Property

    Public Property ADD_COPYRIGHT_DEFAULT As Boolean
        Get
            Return BLN_PROPERTY_ADD_COPYRIGHT_DEFAULT
        End Get
        Set(ByVal value As Boolean)
            BLN_PROPERTY_ADD_COPYRIGHT_DEFAULT = value
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
            Case ENM_WINDOW_EXEC.DO_CANCEL
                Call SUB_CANCEL()
            Case ENM_WINDOW_EXEC.DO_ROTATE_R
                Call SUB_ROTATE_R()
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

        Dim SRT_RET As SRT_RETURN_SAVE_CONFIRM
        With SRT_RET
            .CANCEL = False
            .ADD_CR = CHK_COPYRIGHT.IsChecked
            .ROTATE = ENM_WINDOW_ROTATE_CURRENT
        End With

        Me.RETURN_CONFIRM = SRT_RET
        Call Me.Close()
    End Sub

    Private Sub SUB_CANCEL()

        Call Me.Close()
    End Sub

    Private Sub SUB_ROTATE_R()

        Dim ENM_SET As RotateFlipType
        Select Case ENM_WINDOW_ROTATE_CURRENT
            Case RotateFlipType.RotateNoneFlipNone
                ENM_SET = RotateFlipType.Rotate270FlipXY
            Case RotateFlipType.Rotate270FlipXY
                ENM_SET = RotateFlipType.Rotate180FlipNone
            Case RotateFlipType.Rotate180FlipNone
                ENM_SET = RotateFlipType.Rotate90FlipXY
            Case RotateFlipType.Rotate90FlipXY
                ENM_SET = RotateFlipType.RotateNoneFlipNone
            Case else
                ENM_SET = RotateFlipType.RotateNoneFlipNone
        End Select

        Dim BMP_VIEW As Bitmap
        BMP_VIEW = BMP_PROPERTY_VIEW.Clone()

        Call BMP_VIEW.RotateFlip(ENM_SET)
        Call SUB_SHOW_IMAGE(BMP_VIEW)
        ENM_WINDOW_ROTATE_CURRENT = ENM_SET
    End Sub
#End Region

#Region "初期化・終了処理"
    Private Sub SUB_CTRL_VIEW_INIT()

    End Sub

    Private Sub SUB_CTRL_VALUE_INIT()
        Me.RETURN_CONFIRM = FUNC_GET_INIT_VALUE_RETURN_SAVE_CONFIRM()

        Call SUB_SHOW_IMAGE(BMP_PROPERTY_VIEW.Clone)
        ENM_WINDOW_ROTATE_CURRENT = RotateFlipType.RotateNoneFlipNone
        CHK_COPYRIGHT.IsChecked = Me.ADD_COPYRIGHT_DEFAULT
    End Sub
#End Region

#Region "その他処理"
    Private Function FUNC_GET_INIT_VALUE_RETURN_SAVE_CONFIRM() As SRT_RETURN_SAVE_CONFIRM
        Dim SRT_RET As SRT_RETURN_SAVE_CONFIRM
        With SRT_RET
            .ADD_CR = False
            .CANCEL = True
            .ROTATE = RotateFlipType.RotateNoneFlipNone
        End With

        Return SRT_RET
    End Function

    Private Sub SUB_SHOW_IMAGE(ByRef BMP_SHOW As Bitmap)
        Dim BMS_VIEW As System.Windows.Media.Imaging.BitmapSource
        BMS_VIEW = FUNC_GET_IMAGESOURCE(BMP_SHOW)
        Dim BMW_VIEW As System.Windows.Media.Imaging.WriteableBitmap
        BMW_VIEW = New System.Windows.Media.Imaging.WriteableBitmap(BMS_VIEW)
        PCB_IMAGE.Source = BMW_VIEW
    End Sub
#End Region

#Region "イベント-ボタンクリック"
    Private Sub BTN_OK_Click(sender As Object, e As RoutedEventArgs) Handles BTN_OK.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_OK)
    End Sub

    Private Sub BTN_CANCEL_Click(sender As Object, e As RoutedEventArgs) Handles BTN_CANCEL.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CANCEL)
    End Sub

    Private Sub BTN_ROTATE_R_Click(sender As Object, e As RoutedEventArgs) Handles BTN_ROTATE_R.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_ROTATE_R)
    End Sub
#End Region

    Private Sub WPF_SAVE_CONFIRM_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CTRL_VIEW_INIT()
        Call SUB_CTRL_VALUE_INIT()
    End Sub

End Class
