Imports System.Windows
Imports System.ComponentModel

Public Class WPF_COPY_WINDOW

#Region "プロパティ用変数"
    Private blnPROPETY_CHECK_CLOSED As Boolean = False

    Private ENM_PROPETY_ROTATE_CURRENT As RotateFlipType
    Private BLN_PROPERTY_WINDOW_MAX As Boolean
#End Region

#Region "プロパティ"
    Public Property CHECK_CLOSED As Boolean
        Get
            Return blnPROPETY_CHECK_CLOSED
        End Get
        Set(ByVal value As Boolean)
            blnPROPETY_CHECK_CLOSED = value
        End Set
    End Property

    Public Property ROTATE_CURRENT As RotateFlipType
        Get
            Return ENM_PROPETY_ROTATE_CURRENT
        End Get
        Set(ByVal value As RotateFlipType)
            ENM_PROPETY_ROTATE_CURRENT = value
        End Set
    End Property

    Private Property WINDOW_MAX As Boolean
        Get
            Return BLN_PROPERTY_WINDOW_MAX
        End Get
        Set(ByVal value As Boolean)
            BLN_PROPERTY_WINDOW_MAX = value
        End Set
    End Property

#End Region

#Region "画面用列挙定数"

#End Region

#Region "画面用変数"
    Friend SIZ_WINDOW As RECT_WH
    Friend BMP_VIEW_LOGO As Bitmap
    Friend BMP_BASE_LOGO As Bitmap
    Friend INT_HANDLE_THUMBNAIL As IntPtr

    Private STR_PATH_IMAGE_FILE As String

    Private WPF_IMAGE_SUB_WINDOW As WPF_IMAGE
    Private BLN_SHOW_IMAGE_SUB_WINDOW As Boolean = False
#End Region

#Region "初期化・終了処理"
    Public Sub SUB_INIT_THUBNAIL()

        STR_PATH_IMAGE_FILE = ""

        Dim INT_HANDLE_ME As IntPtr
        INT_HANDLE_ME = FUNC_GET_HANDLE_THUBNAIL()

        INT_HANDLE_THUMBNAIL = 0
        Dim INT_RET As Integer
        INT_RET = DwmRegisterThumbnail(INT_HANDLE_ME, PRC_APP_TARGET.MainWindowHandle, INT_HANDLE_THUMBNAIL)

        If INT_RET = 0 Then
            Call SUB_REFRESH_THUBNAIL()
        End If
    End Sub

    Private Sub SUB_INIT_IMAGE()
        Dim BLN_CHECK_FILE As Boolean
        If STR_PATH_IMAGE_FILE = "" Then
            BLN_CHECK_FILE = False
        Else
            If FUNC_FILE_CHECK(STR_PATH_IMAGE_FILE) Then
                BLN_CHECK_FILE = True
            Else
                BLN_CHECK_FILE = False
            End If
        End If

        Dim URI_IMAGE As Uri
        If Not BLN_CHECK_FILE Then
            URI_IMAGE = New Uri("/RES/IMG/BLANK_16_9.png", UriKind.Relative)
            PCB_APPEND.Source = New System.Windows.Media.Imaging.BitmapImage(URI_IMAGE)
            Exit Sub
        End If

        If WPF_IMAGE_SUB_WINDOW Is Nothing Then
            WPF_IMAGE_SUB_WINDOW = New WPF_IMAGE
        End If

        WPF_IMAGE_SUB_WINDOW.PATH_IMAGE = STR_PATH_IMAGE_FILE
        Call WPF_IMAGE_SUB_WINDOW.SUB_INIT_IMAGE()

        'If WPF_IMAGE_SUB_WINDOW.Visibility = Visibility.Visible Then
        '    Call WPF_IMAGE_SUB_WINDOW.Close()
        'End If

        WPF_IMAGE_SUB_WINDOW.PATH_IMAGE = STR_PATH_IMAGE_FILE
        Call WPF_IMAGE_SUB_WINDOW.Show()
        BLN_SHOW_IMAGE_SUB_WINDOW = True

        'If BLN_CHECK_FILE Then
        '    URI_IMAGE = New Uri(STR_PATH_IMAGE_FILE, UriKind.Relative)
        'Else
        '    URI_IMAGE = New Uri("/RES/IMG/BLANK_16_9.png", UriKind.Relative)
        'End If
        'PCB_APPEND.Source = New System.Windows.Media.Imaging.BitmapImage(URI_IMAGE)

    End Sub
#End Region

#Region "内部処理"
    Private Sub SUB_REFRESH_THUBNAIL()

        If INT_HANDLE_THUMBNAIL = IntPtr.Zero Then
            Exit Sub
        End If

        Dim PSZ_SET As PSIZE

        Call DwmQueryThumbnailSourceSize(INT_HANDLE_THUMBNAIL, PSZ_SET)


        Dim INT_SLIDE_VALUE As Integer
        INT_SLIDE_VALUE = SLI_APPEND_IMAGE_OPACITY.Value
        Dim INT_OPACITY As Integer
        'INT_OPACITY = 255 * (INT_SLIDE_VALUE / SLI_APPEND_IMAGE_OPACITY.Maximum)
        INT_OPACITY = 255

        Dim BLN_CHECK_BORDERLESS As Boolean
        BLN_CHECK_BORDERLESS = FUNC_CHECK_WINDOW_BORDERLESS(PRC_APP_TARGET)

        Dim INT_LEFT As Integer
        Dim INT_TOP As Integer
        Dim INT_RIGHT As Integer
        Dim INT_BOTTOM As Integer

        If BLN_CHECK_BORDERLESS Then 'ボーダレスの場合
            INT_LEFT = 0
            INT_TOP = 0
            INT_RIGHT = Me.Width
            INT_BOTTOM = Me.Height
        Else
            Dim RCT_CLIENT As RECT_WH
            RCT_CLIENT = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)

            Dim RCT_WIONDOW As RECT_WH
            RCT_WIONDOW = FUNC_GET_WINDOW_RECT_WH(PRC_APP_TARGET)

            Dim DEC_RATE_W As Decimal
            DEC_RATE_W = CDec(Me.Width / RCT_CLIENT.width)

            Dim DEC_RATE_H As Decimal
            DEC_RATE_H = CDec(Me.Height / RCT_CLIENT.height)

            Dim INT_BORDER As Integer
            INT_BORDER = CInt(Math.Floor((RCT_WIONDOW.width - RCT_CLIENT.width) / 2)) 'ウィンドウ枠のピクセルを取得

            Dim INT_BAR As Integer
            INT_BAR = (RCT_WIONDOW.height - RCT_CLIENT.height) - (INT_BORDER * 2) 'バーの高さを取得

            Dim INT_BORDER_W As Integer
            INT_BORDER_W = CInt(Math.Floor(INT_BORDER * DEC_RATE_W))

            Dim INT_BORDER_H As Integer
            INT_BORDER_H = CInt(Math.Floor(INT_BORDER * DEC_RATE_H))

            Dim INT_BAR_H As Integer
            INT_BAR_H = CInt(Math.Floor(INT_BAR * DEC_RATE_H))

            INT_LEFT = -1 * (INT_BORDER_W)
            INT_TOP = -1 * (INT_BORDER_H + INT_BAR_H)
            INT_RIGHT = (+1 * (INT_BORDER_W)) + Me.Width
            INT_BOTTOM = (+1 * (INT_BORDER_H)) + Me.Height
        End If

        Dim PRP_SET As DWM_THUMBNAIL_PROPERTIES
        With PRP_SET
            .fVisible = True
            .dwFlags = DWM_TNP_VISIBLE Or DWM_TNP_RECTDESTINATION Or DWM_TNP_OPACITY
            .opacity = CByte(INT_OPACITY)
            .rcDestination.left = INT_LEFT
            .rcDestination.top = INT_TOP
            .rcDestination.right = INT_RIGHT
            .rcDestination.bottom = INT_BOTTOM
        End With

        Call DwmUpdateThumbnailProperties(INT_HANDLE_THUMBNAIL, PRP_SET)
    End Sub

    Private Sub SUB_LOAD_LOGO(ByRef BMP_LOGO As Bitmap, ByVal ENM_ROTATE As RotateFlipType)
        Dim BMP_VIEW As Bitmap

        BMP_VIEW = BMP_LOGO.Clone
        BMP_VIEW.RotateFlip(ENM_ROTATE)

        'Me.Width = BMP_VIEW.Width
        'Me.Height = BMP_VIEW.Height

        PCB_APPEND.Source = FUNC_GET_IMAGESOURCE(BMP_VIEW)

        BMP_BASE_LOGO = BMP_LOGO.Clone
        BMP_VIEW_LOGO = BMP_VIEW.Clone
    End Sub


    Private BLN_SET_SIZE As Boolean = False
    Private Sub SUB_SET_RATE()
        Dim intWIDTH As Integer
        Dim intHEIGHT As Integer
        Dim intWIDTH_01 As Integer
        Dim intHEIGHT_01 As Integer
        Dim intWIDTH_02 As Integer
        Dim intHEIGHT_02 As Integer
        Dim intAREA_01 As Integer
        Dim intAREA_02 As Integer
        Dim intWIDTH_SET As Integer
        Dim intHEIGHT_SET As Integer

        intWIDTH = Me.ActualWidth
        intHEIGHT = Me.ActualHeight

        Dim RCT_WINDOW_WH As MOD_PROCESS_WINDOW.RECT_WH
        RCT_WINDOW_WH = FUNC_GET_WINDOW_RECT_WH(PRC_APP_TARGET)
        Dim INT_WINDOW_WIDTH As Integer
        INT_WINDOW_WIDTH = RCT_WINDOW_WH.width
        Dim INT_WINDOW_HEIGHT As Integer
        INT_WINDOW_HEIGHT = RCT_WINDOW_WH.height

        Dim BLN_LOCK_ASPPECT As Boolean
        BLN_LOCK_ASPPECT = MNI_RATE_LOCK.IsChecked
        BLN_LOCK_ASPPECT = False

        If Not BLN_LOCK_ASPPECT Then
            intWIDTH_01 = intWIDTH
            intHEIGHT_01 = intHEIGHT

            intHEIGHT_02 = intHEIGHT
            intWIDTH_02 = intWIDTH
        Else
            Dim DEC_RATE_HEIGHT As Decimal
            DEC_RATE_HEIGHT = INT_WINDOW_HEIGHT / INT_WINDOW_WIDTH
            intWIDTH_01 = intWIDTH
            intHEIGHT_01 = (intWIDTH_01 * DEC_RATE_HEIGHT)

            Dim DEC_RATE_WIDTH As Decimal
            DEC_RATE_WIDTH = INT_WINDOW_WIDTH / INT_WINDOW_HEIGHT

            intHEIGHT_02 = intHEIGHT
            intWIDTH_02 = intHEIGHT_02 * DEC_RATE_WIDTH
        End If

        intAREA_01 = intWIDTH_01 * intHEIGHT_01
        intAREA_02 = intWIDTH_02 * intHEIGHT_02

        Dim RCT_CLIENT_WH As MOD_PROCESS_WINDOW.RECT_WH
        RCT_CLIENT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)
        Dim intWIDTH_CLIENT As Integer
        Dim intHEIGHT_CLIENT As Integer
        intWIDTH_CLIENT = RCT_CLIENT_WH.width
        intHEIGHT_CLIENT = RCT_CLIENT_WH.height

        If intAREA_01 <= intAREA_02 Then '基本的には大きい方を採用する
            If intWIDTH_CLIENT >= intWIDTH_02 And intHEIGHT_CLIENT >= intHEIGHT_02 Then 'ただし画面サイズを超えていない場合だけ
                intWIDTH_SET = intWIDTH_02
                intHEIGHT_SET = intHEIGHT_02
            Else
                intWIDTH_SET = intWIDTH_01
                intHEIGHT_SET = intHEIGHT_01
            End If
        Else
            If intWIDTH_CLIENT >= intWIDTH_01 And intHEIGHT_CLIENT >= intHEIGHT_01 Then 'ただし画面サイズを超えていない場合だけ
                intWIDTH_SET = intWIDTH_01
                intHEIGHT_SET = intHEIGHT_01
            Else
                intWIDTH_SET = intWIDTH_02
                intHEIGHT_SET = intHEIGHT_02
            End If
        End If

        BLN_SET_SIZE = True
        Me.Width = intWIDTH_SET
        Me.Height = intHEIGHT_SET
        BLN_SET_SIZE = False
    End Sub
#End Region

#Region "NEW"

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        'ウィンドウをマウスのドラッグで移動できるようにする
        AddHandler Me.MouseLeftButtonDown, Sub(sender, e) Me.DragMove()

        Call SUB_INIT_IMAGE()
    End Sub

#End Region

#Region "イベント-コンテキストメニュークリック"
    Private Sub MNI_LOAD_IMAGE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_LOAD_IMAGE.Click
        Dim ofdDIALOG As OpenFileDialog

        ofdDIALOG = New OpenFileDialog()
        ofdDIALOG.Title = "Open image"
        ofdDIALOG.Filter = "image file|*.png;*.jpg;*.tif"
        ofdDIALOG.Multiselect = False
        'ofdDIALOG.InitialDirectory = srtCAPT_SETTINGS.PATH_SAVE

        Dim rstDIALOG As DialogResult
        rstDIALOG = ofdDIALOG.ShowDialog()

        If rstDIALOG = Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim strFILE_PATH As String
        strFILE_PATH = ofdDIALOG.FileName
        STR_PATH_IMAGE_FILE = strFILE_PATH
        Call SUB_INIT_IMAGE()

    End Sub

    Private Sub MNI_DISABLE_IMAGE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_DISABLE_IMAGE.Click
        SLI_APPEND_IMAGE_OPACITY.Value = SLI_APPEND_IMAGE_OPACITY.Maximum
        STR_PATH_IMAGE_FILE = ""
        Call SUB_INIT_IMAGE()
        Call SUB_REFRESH_THUBNAIL()
    End Sub

    Private Sub MNI_CLOSE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_CLOSE.Click
        Call Me.Close()
    End Sub

    Private Sub MNI_RATE_LOCK_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_LOCK.Click
        If MNI_RATE_LOCK.IsChecked Then
            Call SUB_SET_RATE()
        End If
    End Sub
#End Region

    Private Sub SLI_APPEND_IMAGE_OPACITY_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles SLI_APPEND_IMAGE_OPACITY.ValueChanged
        Call SUB_REFRESH_THUBNAIL()
    End Sub

#Region "マウスイベント"

    'Private Sub WPF_COPY_WINDOW_MouseLeave(sender As Object, e As MouseEventArgs) Handles Me.MouseLeave
    '    'Me.PCB_COMPOSITION.Visibility = System.Windows.Visibility.Hidden
    'End Sub

    'Private Sub WPF_COPY_WINDOW_MouseEnter(sender As Object, e As MouseEventArgs) Handles Me.MouseEnter
    '    'Me.PCB_COMPOSITION.Visibility = System.Windows.Visibility.Visible
    'End Sub

#End Region

    Private Sub WPF_COPY_WINDOW_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If BLN_SHOW_IMAGE_SUB_WINDOW Then
            Call WPF_IMAGE_SUB_WINDOW.Close()
            WPF_IMAGE_SUB_WINDOW = Nothing
        End If
        Me.CHECK_CLOSED = True
    End Sub

    Private Sub WPF_COPY_WINDOW_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        Call SUB_SET_RATE()
        Call SUB_REFRESH_THUBNAIL()
    End Sub

    Private Sub WPF_COPY_WINDOW_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged

    End Sub

End Class
