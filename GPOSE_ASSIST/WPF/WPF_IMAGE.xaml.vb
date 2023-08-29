Imports System.Windows
Imports System.Windows.Controls

Public Class WPF_IMAGE

#Region "プロパティ用変数"
    Private STR_PROPETY_PATH_IMAGE As String
#End Region

#Region "プロパティ"
    Public Property PATH_IMAGE As String
        Get
            Return STR_PROPETY_PATH_IMAGE
        End Get
        Set(ByVal value As String)
            STR_PROPETY_PATH_IMAGE = value
        End Set
    End Property
#End Region

#Region "NEW"

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        'ウィンドウをマウスのドラッグで移動できるようにする
        AddHandler Me.MouseLeftButtonDown, Sub(sender, e) Me.DragMove()
    End Sub

#End Region

#Region "内部処理"
    Public Sub SUB_INIT_IMAGE()
        Dim BLN_CHECK_FILE As Boolean
        If Me.PATH_IMAGE = "" Then
            BLN_CHECK_FILE = False
        Else
            If FUNC_FILE_CHECK(Me.PATH_IMAGE) Then
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

        Dim BMP_APPEND As Bitmap
        BMP_APPEND = System.Drawing.Image.FromFile(Me.PATH_IMAGE)
        PCB_APPEND.Source = FUNC_GET_IMAGESOURCE(BMP_APPEND)

        Call SUB_CHANGE_IMAGE_OPACITY()
        'URI_IMAGE = New Uri(Me.PATH_IMAGE, UriKind.Relative)
        'PCB_APPEND.Source = New System.Windows.Media.Imaging.BitmapImage(URI_IMAGE)
    End Sub

    Private Sub SUB_LOAD_LOGO(ByRef BMP_LOGO As Bitmap, ByVal ENM_ROTATE As RotateFlipType)
        Dim BMP_VIEW As Bitmap

        BMP_VIEW = BMP_LOGO.Clone
        BMP_VIEW.RotateFlip(ENM_ROTATE)

        'Me.Width = BMP_VIEW.Width
        'Me.Height = BMP_VIEW.Height

        PCB_APPEND.Source = FUNC_GET_IMAGESOURCE(BMP_VIEW)

    End Sub

    Private Sub SUB_CHANGE_IMAGE_OPACITY()
        Dim INT_VALUE As Integer
        INT_VALUE = SLI_APPEND_IMAGE_OPACITY.Value

        Dim DEC_OPACITY As Decimal
        DEC_OPACITY = CDec(INT_VALUE / 100)

        PCB_APPEND.Opacity = CDbl(DEC_OPACITY)
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
        Me.PATH_IMAGE = strFILE_PATH
        Call SUB_INIT_IMAGE()

        SLI_APPEND_IMAGE_OPACITY.Value = 50
    End Sub

    Private Sub MNI_CLOSE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_CLOSE.Click
        Call Me.Close()
    End Sub

#End Region

#Region "イベント-バリューチェンジ"
    Private Sub SLI_APPEND_IMAGE_OPACITY_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles SLI_APPEND_IMAGE_OPACITY.ValueChanged
        Call SUB_CHANGE_IMAGE_OPACITY()
    End Sub

#End Region

End Class
