Imports System.Windows
Imports System.Windows.Input

Public Class WPF_ROTATE

#Region "画面用・変数"
    Private BLN_WINDOW_EXEC_DO As Boolean = False

    Private TIM_TOPMOST As System.Windows.Threading.DispatcherTimer
    Private TIM_REFRESH_IMAGE As System.Windows.Threading.DispatcherTimer
#End Region

#Region "NEW"
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        AddHandler Me.MouseLeftButtonDown, Sub(sender, e) Me.DragMove() 'ウィンドウをマウスのドラッグで移動できるようにする

        Call SUB_START_TIMER_TOPMOST()
        Call SUB_START_TIMER_REFRESH_IMAGE()
    End Sub
#End Region

#Region "外部呼出"
    Dim BMP_PROCESS_CLIENT As System.Drawing.Bitmap
    Dim GRP_PROCESS_CLIENT As System.Drawing.Graphics
    Public Sub SUB_IMAGE_RELOAD()
        BMP_PROCESS_CLIENT = Nothing
        GRP_PROCESS_CLIENT = Nothing

        Dim SRT_RECT_CLIENT_WH As RECT_WH
        SRT_RECT_CLIENT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)

        Dim PFT_BITMAP As System.Drawing.Imaging.PixelFormat
        PFT_BITMAP = System.Drawing.Imaging.PixelFormat.Format24bppRgb '24bit指定

        BMP_PROCESS_CLIENT = New System.Drawing.Bitmap(SRT_RECT_CLIENT_WH.width, SRT_RECT_CLIENT_WH.height, PFT_BITMAP)
        GRP_PROCESS_CLIENT = System.Drawing.Graphics.FromImage(BMP_PROCESS_CLIENT)
    End Sub
#End Region

#Region "その他"

    Private Sub SUB_PUT_GUIDE()
        Dim STR_PUT As String

        STR_PUT = ""
        STR_PUT &= "X:" & PCB_ROTATE.Margin.Left & System.Environment.NewLine
        STR_PUT &= "Y:" & PCB_ROTATE.Margin.Top & System.Environment.NewLine
        STR_PUT &= "W:" & PCB_ROTATE.Width & System.Environment.NewLine
        STR_PUT &= "H:" & PCB_ROTATE.Height & System.Environment.NewLine

        STR_PUT &= "WW:" & Me.Width & System.Environment.NewLine
        STR_PUT &= "HH:" & Me.Height & System.Environment.NewLine

        LBL_GUIDE.Content = STR_PUT
    End Sub

    Private Sub SUB_REFRESH_SIZE_LOCATION()
        Dim DBL_WH_RATE As Double
        DBL_WH_RATE = (BMP_PROCESS_CLIENT.Width / BMP_PROCESS_CLIENT.Height)

        Dim DBL_HW_RATE As Double
        DBL_HW_RATE = (BMP_PROCESS_CLIENT.Height / BMP_PROCESS_CLIENT.Width)

        Dim DBL_W_RATE As Double
        DBL_W_RATE = Me.Height / BMP_PROCESS_CLIENT.Width

        Dim DBL_H_RATE As Double
        DBL_H_RATE = Me.Width / BMP_PROCESS_CLIENT.Height


        Dim INT_W_SET As Integer
        Dim INT_H_SET As Integer

        If DBL_W_RATE < DBL_H_RATE Then
            INT_W_SET = Me.Height
            INT_H_SET = Math.Floor(INT_W_SET * DBL_HW_RATE)
        Else
            INT_H_SET = Me.Width
            INT_W_SET = Math.Floor(INT_H_SET * DBL_WH_RATE)
        End If

        PCB_ROTATE.Width = INT_W_SET
        PCB_ROTATE.Height = INT_H_SET

        Dim INT_Y As Integer
        INT_Y = CInt(Math.Floor((PCB_ROTATE.Width - PCB_ROTATE.Height) / 2))

        Dim INT_X As Integer
        INT_X = CInt(Math.Floor((PCB_ROTATE.Height - PCB_ROTATE.Width) / 2))

        Dim THK_CURRENT As Thickness
        THK_CURRENT = New Thickness(INT_X, INT_Y, 0, 0)
        PCB_ROTATE.Margin = THK_CURRENT
    End Sub
#End Region

#Region "イベント-コンテキストメニュークリック"

    Private Sub MNI_CLOSE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_CLOSE.Click
        Call Me.Hide()
    End Sub
#End Region

#Region "イベント-タイマー"

#Region "TOPMOST"
    Private Sub SUB_START_TIMER_TOPMOST()
        TIM_TOPMOST = New System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Send)
        AddHandler TIM_TOPMOST.Tick, AddressOf TIM_TOPMOST_TICK
        TIM_TOPMOST.Interval = TimeSpan.FromMilliseconds(1000)
        TIM_TOPMOST.IsEnabled = True
        Call TIM_TOPMOST.Start()
    End Sub

    Private Sub SUB_STOP_TIMER_TOPMOST()
        If Not (TIM_TOPMOST Is Nothing) Then
            Call TIM_TOPMOST.Stop()
            TIM_TOPMOST.IsEnabled = False
            RemoveHandler TIM_TOPMOST.Tick, AddressOf TIM_TOPMOST_TICK
            TIM_TOPMOST = Nothing
        End If
    End Sub

#End Region

#Region "REFRESH_IMAGE"
    Private Sub SUB_START_TIMER_REFRESH_IMAGE()
        TIM_REFRESH_IMAGE = New System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Send)
        AddHandler TIM_REFRESH_IMAGE.Tick, AddressOf TIM_REFRESH_IMAGE_TICK
        TIM_REFRESH_IMAGE.Interval = TimeSpan.FromMilliseconds(100)
        TIM_REFRESH_IMAGE.IsEnabled = True
        Call TIM_REFRESH_IMAGE.Start()
    End Sub

#End Region

    Private Sub TIM_TOPMOST_TICK(sender As Object, e As EventArgs)

        If Not Me.Visibility = System.Windows.Visibility.Visible Then
            Exit Sub
        End If

        Dim BLN_FORE_MAIN As Boolean
        BLN_FORE_MAIN = FUNC_CHECK_FOREGROUND_APPL(PRC_APP_TARGET)

        If BLN_FORE_MAIN Then
            If Not Me.Topmost Then
                Me.Topmost = True
            End If
        Else
            If Me.Topmost Then
                Me.Topmost = False
            End If
        End If
    End Sub

    Private Sub TIM_REFRESH_IMAGE_TICK(sender As Object, e As EventArgs)

        If Not Me.Visibility = System.Windows.Visibility.Visible Then
            Exit Sub
        End If

        Call SUB_REFRESH_IMAGE()
    End Sub

    Private BLN_SET_ANGLE_DO As Boolean = False
    Private Sub SUB_SET_ANGLE(ByVal INT_ANGLE As Integer)

        If BLN_SET_ANGLE_DO Then
            Exit Sub
        End If

        BLN_SET_ANGLE_DO = True

        'RTF_ROTATE.Angle = INT_ANGLE
        BLN_SET_ANGLE_DO = False
    End Sub

    Private BLN_REFRESH_IMAGE_DO As Boolean = False
    Private Sub SUB_REFRESH_IMAGE()

        If BLN_REFRESH_IMAGE_DO Then
            Exit Sub
        End If

        If BMP_PROCESS_CLIENT Is Nothing Then
            Exit Sub
        End If

        BLN_REFRESH_IMAGE_DO = True

        Dim BMS_VIEW As System.Windows.Media.Imaging.BitmapSource
        Dim STW_TIME As System.Diagnostics.Stopwatch
        STW_TIME = New System.Diagnostics.Stopwatch
        Call STW_TIME.Start()
        'Call SUB_BIT_BLT(GRP_PROCESS_CLIENT, PRC_APP_TARGET, BMP_PROCESS_CLIENT.Width, BMP_PROCESS_CLIENT.Height)
        'BMS_VIEW = FUNC_GET_IMAGESOURCE(BMP_PROCESS_CLIENT)
        BMS_VIEW = FUNC_GET_BS_FROM_BIT_BLT(GRP_PROCESS_CLIENT, PRC_APP_TARGET, BMP_PROCESS_CLIENT.Width, BMP_PROCESS_CLIENT.Height)
        Call STW_TIME.Stop()
        Debug.WriteLine("経過：" & STW_TIME.ElapsedMilliseconds & "ms")

        If Not (BMS_VIEW Is Nothing) Then
            PCB_ROTATE.Source = BMS_VIEW
        End If

        BLN_REFRESH_IMAGE_DO = False
    End Sub
#End Region

    Private Sub WPF_ROTATE_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub

    Private Sub WPF_ROTATE_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub

    Private Sub WPF_ROTATE_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        Call SUB_REFRESH_SIZE_LOCATION()
    End Sub

    Private Sub WPF_ROTATE_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Dim INT_LEFT As Integer
        Dim INT_TOP As Integer
        Select Case e.Key
            Case Key.Up
                INT_LEFT = PCB_ROTATE.Margin.Left
                INT_TOP = PCB_ROTATE.Margin.Top - 1
            Case Key.Right
                INT_LEFT = PCB_ROTATE.Margin.Left + 1
                INT_TOP = PCB_ROTATE.Margin.Top
            Case Key.Down
                INT_LEFT = PCB_ROTATE.Margin.Left
                INT_TOP = PCB_ROTATE.Margin.Top + 1
            Case Key.Left
                INT_LEFT = PCB_ROTATE.Margin.Left - 1
                INT_TOP = PCB_ROTATE.Margin.Top
            Case Else
                INT_LEFT = PCB_ROTATE.Margin.Left
                INT_TOP = PCB_ROTATE.Margin.Top
        End Select

        Dim THK_CURRENT As Thickness
        THK_CURRENT = New Thickness(INT_LEFT, INT_TOP, 0, 0)
        PCB_ROTATE.Margin = THK_CURRENT
    End Sub

End Class
