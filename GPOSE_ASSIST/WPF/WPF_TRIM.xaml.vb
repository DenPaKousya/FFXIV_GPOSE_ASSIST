Imports System.ComponentModel
Imports System.Printing
Imports System.Windows
Imports System.Windows.Input

Public Class WPF_TRIM

#Region "画面用・列挙定数"
    Private Enum ENM_WINDOW_EXEC
        DO_CAPTURE = 0
        DO_CAPTURE_JPEG
        DO_KEY_W
        DO_KEY_A
        DO_KEY_S
        DO_KEY_D
        DO_KEY_ALLOW_UP
        DO_KEY_ALLOW_LEFT
        DO_KEY_ALLOW_DOWN
        DO_KEY_ALLOW_RIGHT
        DO_KEY_PAGE_UP
        DO_KEY_PAGE_DOWN
        DO_FIT_WINDOW
        DO_FIT_WINDOW_VERTICAL
    End Enum

    Private Enum ENM_DIVISION_PATTERN
        NONE = 0

        TWO_DIV '1
        THREE_DIV '2
        QUAD_DIV '3
        THREE_DIV_PHI '4
        CROSS_DIV '5
        DIAGONAL_DIV_LL '6
        DIAGONAL_DIV_UL '7

        V_SHAPE_DOWN '8
        V_SHAPE_UP '9
        V_SHAPE_RIGHT '10
        V_SHAPE_LEFT '11

        HARMONIOUS_TRIANGLE_HLL '12
        HARMONIOUS_TRIANGLE_HUL '13
        HARMONIOUS_TRIANGLE_VLL '14
        HARMONIOUS_TRIANGLE_VUL '15

        GOLDEN_TRIANGLE_HLL '16
        GOLDEN_TRIANGLE_HUL '17
        GOLDEN_TRIANGLE_VLL '18
        GOLDEN_TRIANGLE_VUL '19

        GOLDEN_RECTANGLE_HLL '20
        GOLDEN_RECTANGLE_HUR '21
        GOLDEN_RECTANGLE_HUL '22
        GOLDEN_RECTANGLE_HLR '23

        GOLDEN_RECTANGLE_VLL '24
        GOLDEN_RECTANGLE_VUR '25
        GOLDEN_RECTANGLE_VUL '26
        GOLDEN_RECTANGLE_VLR '27

        USER_01
        USER_02
        USER_03
        USER_04
        USER_05
        USER_06
        USER_07
        USER_08
        USER_09
    End Enum

    Private Enum ENM_WINDOW_RATE
        RATE_FREE = 0
        RATE_1_1
        RATE_2_3
        RATE_3_2
        RATE_3_4
        RATE_4_3
        RATE_5_7
        RATE_7_5
        RATE_5_8
        RATE_8_5
        RATE_5_12
        RATE_12_5
        RATE_9_16
        RATE_16_9
    End Enum

    Private Enum ENM_WINDOW_SIZE
        SIZE_0 = 0
        SIZE_1
        SIZE_2
        SIZE_3
        SIZE_4
        SIZE_5
        SIZE_6
    End Enum
#End Region

#Region "画面用・構造体"
    Private Structure SRT_KEY_MASK
        Public SHIFT As Boolean
        Public CTRL As Boolean
        Public ALT As Boolean
    End Structure
#End Region

#Region "画面用・変数"
    Private BLN_WINDOW_EXEC_DO As Boolean = False
    Private SRT_MASK As SRT_KEY_MASK
    Private TIM_TOPMOST As System.Windows.Threading.DispatcherTimer
#End Region

#Region "プロパティ用変数"
    Private ENM_PROPERTY_COMPOSTION_TYPE As ENM_DIVISION_PATTERN
#End Region

#Region "プロパティ"

    Private Property DRAWED_COMPOSTION_TYPE As ENM_DIVISION_PATTERN
        Get
            Return ENM_PROPERTY_COMPOSTION_TYPE
        End Get
        Set(ByVal value As ENM_DIVISION_PATTERN)
            ENM_PROPERTY_COMPOSTION_TYPE = value
        End Set
    End Property

    Public Property SET_SIZE As Boolean
        Get
            Return BLN_SET_SIZE
        End Get
        Set(ByVal value As Boolean)
            BLN_SET_SIZE = value
        End Set
    End Property
#End Region

#Region "初期化・終了処理"
    Private Sub SUB_CTRL_VALUE_INIT()

        Dim INT_INDEX As Integer
        INT_INDEX = (SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER.Length - 1)
        For i = 1 To INT_INDEX
            Select Case i
                Case 1
                    MNI_COMP_USER_01.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 2
                    MNI_COMP_USER_02.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 3
                    MNI_COMP_USER_03.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 4
                    MNI_COMP_USER_04.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 5
                    MNI_COMP_USER_05.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 6
                    MNI_COMP_USER_06.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 7
                    MNI_COMP_USER_07.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 8
                    MNI_COMP_USER_08.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
                Case 9
                    MNI_COMP_USER_09.Header = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(i).NAME
            End Select
        Next

    End Sub
#End Region

#Region "外部呼出"
    Public Sub SUB_SET_LOCATION_CONTROL(ByVal INT_MOVE_POINT As Integer)

        Dim TIK_CONTROL As System.Windows.Thickness
        Dim TIK_SET As System.Windows.Thickness

        TIK_CONTROL = CHK_OPACITY.Margin
        TIK_SET = New Thickness(TIK_CONTROL.Left, TIK_CONTROL.Top + INT_MOVE_POINT, TIK_CONTROL.Right, TIK_CONTROL.Bottom)
        CHK_OPACITY.Margin = TIK_SET

        TIK_CONTROL = CHK_CONFIRM.Margin
        TIK_SET = New Thickness(TIK_CONTROL.Left, TIK_CONTROL.Top + INT_MOVE_POINT, TIK_CONTROL.Right, TIK_CONTROL.Bottom)
        CHK_CONFIRM.Margin = TIK_SET

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
            Case ENM_WINDOW_EXEC.DO_CAPTURE
                Call SUB_CAPTURE()
            Case ENM_WINDOW_EXEC.DO_CAPTURE_JPEG
                Call SUB_CAPTURE_JPEG()
            Case ENM_WINDOW_EXEC.DO_KEY_W
                Call SUB_KEY_W()
            Case ENM_WINDOW_EXEC.DO_KEY_A
                Call SUB_KEY_A()
            Case ENM_WINDOW_EXEC.DO_KEY_S
                Call SUB_KEY_S()
            Case ENM_WINDOW_EXEC.DO_KEY_D
                Call SUB_KEY_D()
            Case ENM_WINDOW_EXEC.DO_KEY_ALLOW_UP
                Call SUB_KEY_ALLOW_UP()
            Case ENM_WINDOW_EXEC.DO_KEY_ALLOW_LEFT
                Call SUB_KEY_ALLOW_LEFT()
            Case ENM_WINDOW_EXEC.DO_KEY_ALLOW_DOWN
                Call SUB_KEY_ALLOW_DOWN()
            Case ENM_WINDOW_EXEC.DO_KEY_ALLOW_RIGHT
                Call SUB_KEY_ALLOW_RIGHT()
            Case ENM_WINDOW_EXEC.DO_KEY_PAGE_UP
                Call SUB_KEY_PAGE_UP()
            Case ENM_WINDOW_EXEC.DO_KEY_PAGE_DOWN
                Call SUB_KEY_PAGE_DOWN()
            Case ENM_WINDOW_EXEC.DO_FIT_WINDOW
                Call SUB_FIT_WINDOW()
            Case ENM_WINDOW_EXEC.DO_FIT_WINDOW_VERTICAL
                Call SUB_FIT_WINDOW_VERTICAL()
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

    Private Sub SUB_CAPTURE()

        Dim BMP_PROCESS_CLIENT As System.Drawing.Bitmap
        BMP_PROCESS_CLIENT = Nothing

        Dim GRP_PROCESS_CLIENT As System.Drawing.Graphics
        GRP_PROCESS_CLIENT = Nothing

        Call SUB_PRINT_CLIENT_WINDOW(PRC_APP_TARGET, GRP_PROCESS_CLIENT, BMP_PROCESS_CLIENT)

        Dim SRT_TRIM As RECT_WH
        SRT_TRIM = FUNC_GET_RECT_ME_TRIMING()
        Dim BMP_MAKE As System.Drawing.Bitmap
        BMP_MAKE = FUNC_MAKE_BITMAP_FROM_PRINT(BMP_PROCESS_CLIENT, SRT_TRIM, RotateFlipType.RotateNoneFlipNone)

        Dim BLN_OK_CONFIRM As Boolean
        Dim SRT_RET_CONFIRM As WPF_SAVE_CONFIRM.SRT_RETURN_SAVE_CONFIRM
        If CHK_CONFIRM.IsChecked Then
            SRT_RET_CONFIRM = FUNC_SHOW_CONFIRM(BMP_MAKE, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.SAVE.FILE.COPYRIGHT))
            If SRT_RET_CONFIRM.CANCEL Then
                Exit Sub
            End If
            BLN_OK_CONFIRM = True
            If Not SRT_RET_CONFIRM.ROTATE = RotateFlipType.RotateNoneFlipNone Then
                Call BMP_MAKE.RotateFlip(SRT_RET_CONFIRM.ROTATE)
            End If
        Else
            BLN_OK_CONFIRM = False
        End If

        Call SUB_FIXED_PHRASE_INIT(Now, SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX)
        Dim SRT_SAVE As SRT_SAVE_IMAGE_PARAM
        With SRT_SAVE
            .SAVE_DIR = FUNC_GET_NAME_SAVE_DIR(SRT_APP_SETTINGS_VALUE.SAVE.DIRECTORY, SRT_APP_SETTINGS_VALUE.SAVE.FILE.DIRECTORY)
            .SAVE_NAME_FILE = FUNC_GET_NAME_SAVE_FILE(SRT_APP_SETTINGS_VALUE.SAVE.FILE.NAME)
            .TYPE = FUNC_GET_TYPE_IMAGE01_FROM_STRING(SRT_APP_SETTINGS_VALUE.SAVE.FILE.TYPE)
            .QUALITY = SRT_APP_SETTINGS_VALUE.SAVE.FILE.QUALITY
            .ADD_CR = FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.SAVE.FILE.COPYRIGHT)
            .EXIF = FUNC_GET_EXIF_DEFAULT(SRT_APP_SETTINGS_VALUE.PROCESS_NAME, Now)
            .SAVE_NAME_FILE_REAL = FUNC_GET_NAME_FILE_REAL(.SAVE_DIR, .SAVE_NAME_FILE)

            If BLN_OK_CONFIRM Then
                .ADD_CR = SRT_RET_CONFIRM.ADD_CR
            End If
        End With
        If Not FUNC_SAVE_IMAGE(BMP_MAKE, SRT_SAVE) Then
            Exit Sub
        End If

        Dim SRT_SAVE_02 As SRT_SAVE_IMAGE_PARAM
        With SRT_SAVE_02
            .SAVE_DIR = SRT_SAVE.SAVE_DIR
            .SAVE_NAME_FILE = SRT_SAVE.SAVE_NAME_FILE
            .TYPE = FUNC_GET_TYPE_IMAGE02_FROM_STRING(SRT_APP_SETTINGS_VALUE.SAVE.FILE.TYPE)
            .QUALITY = SRT_SAVE.QUALITY
            .ADD_CR = True
            .EXIF = SRT_SAVE.EXIF
            .SAVE_NAME_FILE_REAL = SRT_SAVE.SAVE_NAME_FILE_REAL
        End With
        If Not FUNC_SAVE_IMAGE(BMP_MAKE, SRT_SAVE_02) Then
            Exit Sub
        End If

        SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX += 1
        Call My.Computer.Audio.Play(My.Resources.SHUTTER_02_SHORT, Microsoft.VisualBasic.AudioPlayMode.Background)
    End Sub

    Private Sub SUB_CAPTURE_JPEG()
        Dim BMP_PROCESS_CLIENT As System.Drawing.Bitmap
        BMP_PROCESS_CLIENT = Nothing

        Dim GRP_PROCESS_CLIENT As System.Drawing.Graphics
        GRP_PROCESS_CLIENT = Nothing

        Call SUB_PRINT_CLIENT_WINDOW(PRC_APP_TARGET, GRP_PROCESS_CLIENT, BMP_PROCESS_CLIENT)

        Dim SRT_TRIM As RECT_WH
        SRT_TRIM = FUNC_GET_RECT_ME_TRIMING()
        Dim BMP_MAKE As System.Drawing.Bitmap
        BMP_MAKE = FUNC_MAKE_BITMAP_FROM_PRINT(BMP_PROCESS_CLIENT, SRT_TRIM, RotateFlipType.RotateNoneFlipNone)

        Dim BLN_OK_CONFIRM As Boolean
        Dim SRT_RET_CONFIRM As WPF_SAVE_CONFIRM.SRT_RETURN_SAVE_CONFIRM
        If CHK_CONFIRM.IsChecked Then
            SRT_RET_CONFIRM = FUNC_SHOW_CONFIRM(BMP_MAKE, True)
            If SRT_RET_CONFIRM.CANCEL Then
                Exit Sub
            End If
            BLN_OK_CONFIRM = True
            If Not SRT_RET_CONFIRM.ROTATE = RotateFlipType.RotateNoneFlipNone Then
                Call BMP_MAKE.RotateFlip(SRT_RET_CONFIRM.ROTATE)
            End If
        Else
            BLN_OK_CONFIRM = False
        End If

        Call SUB_FIXED_PHRASE_INIT(Now, SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX)
        Dim SRT_SAVE As SRT_SAVE_IMAGE_PARAM
        With SRT_SAVE
            .SAVE_DIR = FUNC_GET_NAME_SAVE_DIR(SRT_APP_SETTINGS_VALUE.SAVE.DIRECTORY, SRT_APP_SETTINGS_VALUE.SAVE.FILE.DIRECTORY)
            .SAVE_NAME_FILE = FUNC_GET_NAME_SAVE_FILE(SRT_APP_SETTINGS_VALUE.SAVE.FILE.NAME)
            .TYPE = ENM_IMAGE_TYPE.JPEG
            .QUALITY = SRT_APP_SETTINGS_VALUE.SAVE.FILE.QUALITY
            .ADD_CR = True
            .EXIF = FUNC_GET_EXIF_DEFAULT(SRT_APP_SETTINGS_VALUE.PROCESS_NAME, Now)
            .SAVE_NAME_FILE_REAL = FUNC_GET_NAME_FILE_REAL(.SAVE_DIR, .SAVE_NAME_FILE)
        End With
        If Not FUNC_SAVE_IMAGE(BMP_MAKE, SRT_SAVE) Then
            Exit Sub
        End If

        SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX += 1
        Call My.Computer.Audio.Play(My.Resources.SHUTTER_SHORT, Microsoft.VisualBasic.AudioPlayMode.Background)
    End Sub


    Private Sub SUB_KEY_W()
        Call SUB_SEND_KEYS_W(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_A()
        Call SUB_SEND_KEYS_A(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_S()
        Call SUB_SEND_KEYS_S(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_D()
        Call SUB_SEND_KEYS_D(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WASD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_ALLOW_UP()
        Call SUB_SEND_KEYS_UP(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_ALLOW_RIGHT()
        Call SUB_SEND_KEYS_RIGHT(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_ALLOW_DOWN()
        Call SUB_SEND_KEYS_DOWN(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_ALLOW_LEFT()
        Call SUB_SEND_KEYS_LEFT(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.ARROW_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_PAGE_UP()
        Call SUB_SEND_KEYS_PGUP(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.PAGEUD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub

    Private Sub SUB_KEY_PAGE_DOWN()
        Call SUB_SEND_KEYS_PGDN(PRC_APP_TARGET, SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.PAGEUD_PUSH_WEIGHT, FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE))
    End Sub


    Private Sub SUB_FIT_WINDOW()

        Dim SRT_CRIENT_RECT_WH As MOD_PROCESS_WINDOW.RECT_WH
        SRT_CRIENT_RECT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)

        Dim INT_CLIENT_WIDTH As Integer
        INT_CLIENT_WIDTH = SRT_CRIENT_RECT_WH.width

        Dim INT_CLIENT_HEIGHT As Integer
        INT_CLIENT_HEIGHT = SRT_CRIENT_RECT_WH.height

        Dim INT_CLIENT_LEFT As Integer
        INT_CLIENT_LEFT = MOD_PROCESS_WINDOW.FUNC_GET_LEFT_CLIENT(PRC_APP_TARGET)

        Dim INT_CLIENT_TOP As Integer
        INT_CLIENT_TOP = MOD_PROCESS_WINDOW.FUNC_GET_TOP_CLIENT(PRC_APP_TARGET)

        Dim INT_SET_WIDTH As Integer
        INT_SET_WIDTH = INT_CLIENT_WIDTH
        Dim INT_SET_HEIGHT As Integer
        INT_SET_HEIGHT = INT_CLIENT_HEIGHT
        Dim INT_SET_LEFT As Integer
        INT_SET_LEFT = INT_CLIENT_LEFT
        Dim INT_SET_TOP As Integer
        INT_SET_TOP = INT_CLIENT_TOP

        Me.Left = INT_SET_LEFT
        Me.Top = INT_SET_TOP
        BLN_SET_SIZE = True
        Me.Width = INT_SET_WIDTH
        Me.Height = INT_SET_HEIGHT
        BLN_SET_SIZE = False
        Call SUB_SET_RATE(ENM_RATE_CURRENT)

        If ENM_RATE_CURRENT = ENM_WINDOW_RATE.RATE_FREE Then
            Exit Sub
        End If

        'アスペクト比フリー以外の場合は位置の調整を行う
        Call SUB_FIT_MOVE(INT_SET_WIDTH, INT_SET_HEIGHT)
    End Sub

    Private Sub SUB_FIT_WINDOW_VERTICAL()

        Dim SRT_CRIENT_RECT_WH As MOD_PROCESS_WINDOW.RECT_WH
        SRT_CRIENT_RECT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)

        Dim INT_CLIENT_WIDTH As Integer
        INT_CLIENT_WIDTH = SRT_CRIENT_RECT_WH.width

        Dim INT_CLIENT_HEIGHT As Integer
        INT_CLIENT_HEIGHT = SRT_CRIENT_RECT_WH.height

        Dim INT_CLIENT_LEFT As Integer
        INT_CLIENT_LEFT = MOD_PROCESS_WINDOW.FUNC_GET_LEFT_CLIENT(PRC_APP_TARGET)

        Dim INT_CLIENT_TOP As Integer
        INT_CLIENT_TOP = MOD_PROCESS_WINDOW.FUNC_GET_TOP_CLIENT(PRC_APP_TARGET)

        '縦画面補助に置換
        Dim INT_SET_WIDTH As Integer
        Dim INT_SET_HEIGHT As Integer
        Dim INT_SET_LEFT As Integer
        Dim INT_SET_TOP As Integer

        If INT_CLIENT_WIDTH > INT_CLIENT_HEIGHT Then '横＞縦（通常）
            INT_SET_HEIGHT = INT_CLIENT_HEIGHT '高さをそのまま転送
            Dim DEC_RATE As Decimal
            DEC_RATE = CDec(CDec(INT_SET_HEIGHT) / CDec(INT_CLIENT_WIDTH)) '転送した高さの既存幅との比を産出

            INT_SET_WIDTH = CInt(CDec(INT_SET_HEIGHT) * DEC_RATE)

            INT_SET_TOP = INT_CLIENT_TOP '縦をそのまま転送
            INT_SET_LEFT = INT_CLIENT_LEFT + CInt((INT_CLIENT_WIDTH - INT_SET_WIDTH) / 2)
        Else
            INT_SET_WIDTH = INT_CLIENT_WIDTH '幅をそのまま転送
            Dim DEC_RATE As Decimal
            DEC_RATE = CDec(CDec(INT_SET_WIDTH) / CDec(INT_CLIENT_HEIGHT)) '転送した幅の既存高さとの比を産出
            INT_SET_HEIGHT = CInt(CDec(INT_SET_WIDTH) * DEC_RATE)

            INT_SET_LEFT = INT_CLIENT_LEFT '横をそのまま転送
            INT_SET_TOP = INT_CLIENT_TOP + CInt((INT_CLIENT_HEIGHT - INT_SET_HEIGHT) / 2)
        End If

        Me.Left = INT_SET_LEFT
        Me.Top = INT_SET_TOP
        BLN_SET_SIZE = True
        Me.Width = INT_SET_WIDTH
        Me.Height = INT_SET_HEIGHT
        BLN_SET_SIZE = False
        Call SUB_SET_RATE(ENM_RATE_CURRENT, INT_SET_WIDTH, INT_SET_HEIGHT)

        If ENM_RATE_CURRENT = ENM_WINDOW_RATE.RATE_FREE Then
            Exit Sub
        End If

        'アスペクト比フリー以外の場合は位置の調整を行う
        Call SUB_FIT_MOVE(INT_SET_WIDTH, INT_SET_HEIGHT)
    End Sub

    Private Sub SUB_FIT_MOVE(ByVal INT_SET_WIDTH As Integer, ByVal INT_SET_HEIGHT As Integer)
        '余白部分を中央寄せ
        Dim INT_WIDTH_SUB As Integer
        INT_WIDTH_SUB = INT_SET_WIDTH - Me.Width
        Dim INT_HEIGHT_SUB As Integer
        INT_HEIGHT_SUB = INT_SET_HEIGHT - Me.Height

        If INT_WIDTH_SUB > INT_HEIGHT_SUB Then '左右中央寄せ
            Me.Left = Me.Left + Math.Truncate(INT_WIDTH_SUB / 2)
        Else '上下中央寄せ
            Me.Top = Me.Top + Math.Truncate(INT_HEIGHT_SUB / 2)
        End If
    End Sub
#End Region

#Region "構図補助"
    Private Sub SUB_CHANGE_DRAW_COMPOTION(ByVal ENM_PAT As ENM_DIVISION_PATTERN)

        If Me.DRAWED_COMPOSTION_TYPE = ENM_PAT Then
            Exit Sub
        End If

        Call SUB_COMP_ALL_UNCHECKED()

        Select Case ENM_PAT
            Case ENM_DIVISION_PATTERN.TWO_DIV
                MNI_COMP_2DIV.IsChecked = True
            Case ENM_DIVISION_PATTERN.THREE_DIV
                MNI_COMP_3DIV.IsChecked = True
            Case ENM_DIVISION_PATTERN.QUAD_DIV
                MNI_COMP_4DIV.IsChecked = True
            Case ENM_DIVISION_PATTERN.THREE_DIV_PHI
                MNI_COMP_3DIV_PHI.IsChecked = True
            Case ENM_DIVISION_PATTERN.CROSS_DIV
                MNI_COMP_CROSS_DIV.IsChecked = True
            Case ENM_DIVISION_PATTERN.DIAGONAL_DIV_LL
                MNI_COMP_DIAGONAL_DIV_LL.IsChecked = True
            Case ENM_DIVISION_PATTERN.DIAGONAL_DIV_UL
                MNI_COMP_DIAGONAL_DIV_UL.IsChecked = True
            Case ENM_DIVISION_PATTERN.V_SHAPE_DOWN
                MNI_COMP_V_SHAPE_DOWN.IsChecked = True
            Case ENM_DIVISION_PATTERN.V_SHAPE_UP
                MNI_COMP_V_SHAPE_UP.IsChecked = True
            Case ENM_DIVISION_PATTERN.V_SHAPE_RIGHT
                MNI_COMP_V_SHAPE_RIGHT.IsChecked = True
            Case ENM_DIVISION_PATTERN.V_SHAPE_LEFT
                MNI_COMP_V_SHAPE_LEFT.IsChecked = True
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_HLL
                MNI_COMP_HARMONIOUS_TRIANGLE_HLL.IsChecked = True
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_HUL
                MNI_COMP_HARMONIOUS_TRIANGLE_HUL.IsChecked = True
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_VLL
                MNI_COMP_HARMONIOUS_TRIANGLE_VLL.IsChecked = True
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_VUL
                MNI_COMP_HARMONIOUS_TRIANGLE_VUL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_HLL
                MNI_COMP_GOLDEN_TRIANGLE_HLL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_HUL
                MNI_COMP_GOLDEN_TRIANGLE_HUL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_VLL
                MNI_COMP_GOLDEN_TRIANGLE_VLL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_VUL
                MNI_COMP_GOLDEN_TRIANGLE_VUL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLL
                MNI_COMP_GOLDEN_RECTANGLE_HLL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUR
                MNI_COMP_GOLDEN_RECTANGLE_HUR.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUL
                MNI_COMP_GOLDEN_RECTANGLE_HUL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLR
                MNI_COMP_GOLDEN_RECTANGLE_HLR.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLL
                MNI_COMP_GOLDEN_RECTANGLE_VLL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUR
                MNI_COMP_GOLDEN_RECTANGLE_VUR.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUL
                MNI_COMP_GOLDEN_RECTANGLE_VUL.IsChecked = True
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLR
                MNI_COMP_GOLDEN_RECTANGLE_VLR.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_01
                MNI_COMP_USER_01.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_02
                MNI_COMP_USER_02.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_03
                MNI_COMP_USER_03.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_04
                MNI_COMP_USER_04.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_05
                MNI_COMP_USER_05.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_06
                MNI_COMP_USER_06.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_07
                MNI_COMP_USER_07.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_08
                MNI_COMP_USER_08.IsChecked = True
            Case ENM_DIVISION_PATTERN.USER_09
                MNI_COMP_USER_09.IsChecked = True
            Case Else
                MNI_COMP_NONE.IsChecked = True
        End Select

        Call SUB_DRAW_COMPOSTION(ENM_PAT)

        Me.DRAWED_COMPOSTION_TYPE = ENM_PAT
        SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.TYPE = Me.DRAWED_COMPOSTION_TYPE
    End Sub

    Private Sub SUB_DRAW_COMPOSTION(ByVal ENM_PAT As ENM_DIVISION_PATTERN)
        Dim INT_WIDTH As Integer
        INT_WIDTH = CInt(PCB_COMPOSITION.Width)

        Dim INT_HEIGHT As Integer
        INT_HEIGHT = CInt(PCB_COMPOSITION.Height)

        If INT_WIDTH <= 0 Then
            Exit Sub
        End If

        If INT_HEIGHT <= 0 Then
            Exit Sub
        End If

        Dim BMP_CANVAS As Bitmap
        BMP_CANVAS = New Bitmap(INT_WIDTH, INT_HEIGHT)

        Dim GRP_DRAW As Graphics
        GRP_DRAW = Graphics.FromImage(BMP_CANVAS)

        Const cstPEN_SIZE As Single = 3.0
        Dim penDRAW As System.Drawing.Pen
        'penDRAW = New System.Drawing.Pen(System.Drawing.Color.Cyan, cstPEN_SIZE)
        penDRAW = New System.Drawing.Pen(System.Drawing.Color.FromArgb(64, System.Drawing.Color.Black), cstPEN_SIZE)

        Dim penFORE As System.Drawing.Pen
        penFORE = New System.Drawing.Pen(System.Drawing.Color.White, 1.0)

        GRP_DRAW.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Call SUB_DRAW_OUTLINE(GRP_DRAW, penDRAW)
        Call SUB_DRAW_OUTLINE(GRP_DRAW, penFORE)

        If ENM_PAT >= ENM_DIVISION_PATTERN.USER_01 Then 'ユーザー定義の場合は
            Dim INT_INDEX As Integer
            INT_INDEX = FUNC_GET_NUMBER_USER(ENM_PAT)
            If INT_INDEX > 0 Then
                Dim INT_COUNT As Integer
                INT_COUNT = (SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(INT_INDEX).TYPE.Length - 1)
                For i = 1 To INT_COUNT
                    Dim INT_TYPE As Integer
                    INT_TYPE = SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.USER(INT_INDEX).TYPE(i)
                    If INT_TYPE > 0 Then
                        Call SUB_DRAW_COMPOSTION_ONE(INT_TYPE, GRP_DRAW, penDRAW, PCB_COMPOSITION)
                        Call SUB_DRAW_COMPOSTION_ONE(INT_TYPE, GRP_DRAW, penFORE, PCB_COMPOSITION)
                    End If
                Next
            End If
        Else
            Call SUB_DRAW_COMPOSTION_ONE(ENM_PAT, GRP_DRAW, penDRAW, PCB_COMPOSITION)
            Call SUB_DRAW_COMPOSTION_ONE(ENM_PAT, GRP_DRAW, penFORE, PCB_COMPOSITION)
        End If

        PCB_COMPOSITION.Source = FUNC_GET_IMAGESOURCE(BMP_CANVAS)
        Call GRP_DRAW.Dispose()

    End Sub

    Private Function FUNC_GET_NUMBER_USER(ByVal ENM_PAT As ENM_DIVISION_PATTERN) As Integer
        Dim INT_RET As Integer

        INT_RET = (CInt(ENM_PAT) - CInt(ENM_DIVISION_PATTERN.USER_01))
        INT_RET += 1
        Return INT_RET
    End Function

    Private Sub SUB_DRAW_OUTLINE(ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen)
        Dim rct As System.Drawing.Rectangle

        rct = New System.Drawing.Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Call GRP_DRAW.DrawRectangle(PEN_DRAW, rct)
    End Sub

    Private Sub SUB_DRAW_COMPOSTION_ONE(ByVal ENM_PAT As ENM_DIVISION_PATTERN, ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image)

        Select Case ENM_PAT
            Case ENM_DIVISION_PATTERN.TWO_DIV
                Call SUB_DRAW_TWO_DIV(GRP_DRAW, PEN_DRAW)
            Case ENM_DIVISION_PATTERN.THREE_DIV
                Call SUB_DRAW_THREE_DIV(GRP_DRAW, PEN_DRAW)
            Case ENM_DIVISION_PATTERN.QUAD_DIV
                Call SUB_DRAW_QUAD_DIV(GRP_DRAW, PEN_DRAW)
            Case ENM_DIVISION_PATTERN.THREE_DIV_PHI
                Call SUB_DRAW_THREE_DIV_PHI(GRP_DRAW, PEN_DRAW)
            Case ENM_DIVISION_PATTERN.CROSS_DIV
                Call SUB_DRAW_CROSS_DIV(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.DIAGONAL_DIV_LL
                Call SUB_DRAW_DIAGONAL_DIV_LL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.DIAGONAL_DIV_UL
                Call SUB_DRAW_DIAGONAL_DIV_UL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.V_SHAPE_DOWN
                Call SUB_DRAW_V_SHAPE_DOWN(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.V_SHAPE_UP
                Call SUB_DRAW_V_SHAPE_UP(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.V_SHAPE_RIGHT
                Call SUB_DRAW_V_SHAPE_RIGHT(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.V_SHAPE_LEFT
                Call SUB_DRAW_V_SHAPE_LEFT(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_HLL
                Call SUB_DRAW_HARMONIOUS_TRIANGLE_HLL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_HUL
                Call SUB_DRAW_HARMONIOUS_TRIANGLE_HUL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_VLL
                Call SUB_DRAW_HARMONIOUS_TRIANGLE_VLL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_VUL
                Call SUB_DRAW_HARMONIOUS_TRIANGLE_VUL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_HLL
                Call SUB_DRAW_GOLDEN_TRIANGLE_HLL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_HUL
                Call SUB_DRAW_GOLDEN_TRIANGLE_HUL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_VLL
                Call SUB_DRAW_GOLDEN_TRIANGLE_VLL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_VUL
                Call SUB_DRAW_GOLDEN_TRIANGLE_VUL(GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_LEFT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_RIGHT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_LEFT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_RIGHT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_LEFT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_RIGHT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_LEFT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_RIGHT, GRP_DRAW, PEN_DRAW, IMG_CANVAS)
            Case Else

        End Select
    End Sub

#Region "各種構図補助線描画"

    Private Sub SUB_DRAW_TWO_DIV(ByRef grpDRAW As Graphics, ByRef penDRAW As System.Drawing.Pen)
        Dim intWIDTH As Integer
        Dim intHEIGHT As Integer

        Dim intHEIGT_2 As Integer
        Dim intWIDTH_2 As Integer

        intWIDTH = PCB_COMPOSITION.Width
        intHEIGHT = PCB_COMPOSITION.Height

        intHEIGT_2 = Math.Floor(intHEIGHT / 2)
        intWIDTH_2 = Math.Floor(intWIDTH / 2)

        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_2, intWIDTH, intHEIGT_2)
        Call grpDRAW.DrawLine(penDRAW, intWIDTH_2, 0, intWIDTH_2, intHEIGHT)

    End Sub

    Private Sub SUB_DRAW_THREE_DIV(ByRef grpDRAW As Graphics, ByRef penDRAW As System.Drawing.Pen)
        Dim intWIDTH As Integer
        Dim intHEIGHT As Integer

        Dim intHEIGT_3 As Integer
        Dim intWIDTH_3 As Integer

        intWIDTH = PCB_COMPOSITION.Width
        intHEIGHT = PCB_COMPOSITION.Height

        intHEIGT_3 = Math.Floor(intHEIGHT / 3)
        intWIDTH_3 = Math.Floor(intWIDTH / 3)


        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_3, intWIDTH, intHEIGT_3)
        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_3 * 2, intWIDTH, intHEIGT_3 * 2)

        Call grpDRAW.DrawLine(penDRAW, intWIDTH_3, 0, intWIDTH_3, intHEIGHT)
        Call grpDRAW.DrawLine(penDRAW, intWIDTH_3 * 2, 0, intWIDTH_3 * 2, intHEIGHT)

    End Sub

    Private Sub SUB_DRAW_QUAD_DIV(ByRef grpDRAW As Graphics, ByRef penDRAW As System.Drawing.Pen)
        Dim intWIDTH As Integer
        Dim intHEIGHT As Integer

        Dim intHEIGT_4 As Integer
        Dim intWIDTH_4 As Integer

        intWIDTH = PCB_COMPOSITION.Width
        intHEIGHT = PCB_COMPOSITION.Height

        intHEIGT_4 = Math.Floor(intHEIGHT / 4)
        intWIDTH_4 = Math.Floor(intWIDTH / 4)

        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_4, intWIDTH, intHEIGT_4)
        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_4 * 2, intWIDTH, intHEIGT_4 * 2)
        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_4 * 3, intWIDTH, intHEIGT_4 * 3)

        Call grpDRAW.DrawLine(penDRAW, intWIDTH_4, 0, intWIDTH_4, intHEIGHT)
        Call grpDRAW.DrawLine(penDRAW, intWIDTH_4 * 2, 0, intWIDTH_4 * 2, intHEIGHT)
        Call grpDRAW.DrawLine(penDRAW, intWIDTH_4 * 3, 0, intWIDTH_4 * 3, intHEIGHT)

    End Sub

    Private Sub SUB_DRAW_THREE_DIV_PHI(ByRef grpDRAW As Graphics, ByRef penDRAW As System.Drawing.Pen)
        Dim intWIDTH As Integer
        Dim intHEIGHT As Integer

        Dim decHEIGHT_ONE As Decimal
        Dim decWIDTH_ONE As Decimal

        Dim intHEIGT_1 As Integer
        Dim intWIDTH_1 As Integer

        Const cstGOLDEN_VALUE As Integer = 1618
        Dim intRATE_VALUE As Integer

        intRATE_VALUE = cstGOLDEN_VALUE + 1000 + cstGOLDEN_VALUE

        intWIDTH = PCB_COMPOSITION.Width
        intHEIGHT = PCB_COMPOSITION.Height

        decWIDTH_ONE = intWIDTH / intRATE_VALUE
        decHEIGHT_ONE = intHEIGHT / intRATE_VALUE

        intHEIGT_1 = Math.Floor(decHEIGHT_ONE * (cstGOLDEN_VALUE))
        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_1, intWIDTH, intHEIGT_1)
        intHEIGT_1 = Math.Floor(decHEIGHT_ONE * (cstGOLDEN_VALUE + 1000))
        Call grpDRAW.DrawLine(penDRAW, 0, intHEIGT_1, intWIDTH, intHEIGT_1)

        intWIDTH_1 = Math.Floor(decWIDTH_ONE * (cstGOLDEN_VALUE))
        Call grpDRAW.DrawLine(penDRAW, intWIDTH_1, 0, intWIDTH_1, intHEIGHT)
        intWIDTH_1 = Math.Floor(decWIDTH_ONE * (cstGOLDEN_VALUE + 1000))
        Call grpDRAW.DrawLine(penDRAW, intWIDTH_1, 0, intWIDTH_1, intHEIGHT)

    End Sub


#End Region

#End Region

#Region "縦横比"

    Private ENM_RATE_CURRENT As ENM_WINDOW_RATE = -1
    Private Sub SUB_CHANGE_RATE(ByVal ENM_RATE As ENM_WINDOW_RATE)

        If ENM_RATE_CURRENT = ENM_RATE Then
            Exit Sub
        End If

        Call SUB_RATE_ALL_UNCHECKED()
        Select Case ENM_RATE
            Case ENM_WINDOW_RATE.RATE_FREE
                MNI_RATE_FREE.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_1_1
                MNI_RATE_1_1.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_2_3
                MNI_RATE_2_3.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_3_2
                MNI_RATE_3_2.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_3_4
                MNI_RATE_3_4.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_4_3
                MNI_RATE_4_3.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_5_7
                MNI_RATE_5_7.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_7_5
                MNI_RATE_7_5.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_5_8
                MNI_RATE_5_8.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_8_5
                MNI_RATE_8_5.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_5_12
                MNI_RATE_5_12.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_12_5
                MNI_RATE_12_5.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_9_16
                MNI_RATE_9_16.IsChecked = True
            Case ENM_WINDOW_RATE.RATE_16_9
                MNI_RATE_16_9.IsChecked = True
        End Select

        Call SUB_SET_RATE(ENM_RATE)
        ENM_RATE_CURRENT = ENM_RATE
        SRT_APP_SETTINGS_VALUE.TRIM.ASPECT_RATIO.TYPE = ENM_RATE_CURRENT
    End Sub

    Private BLN_SET_SIZE As Boolean = False
    Private Sub SUB_SET_RATE(ByVal ENM_RATE As ENM_WINDOW_RATE, Optional ByVal INT_MATCH_WIDTH As Integer = 0, Optional ByVal INT_MATCH_HEIGHT As Integer = 0)
        If ENM_RATE < 0 Then
            Exit Sub
        End If

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

        If INT_MATCH_WIDTH <= 0 Then
            intWIDTH = Me.ActualWidth
        Else
            intWIDTH = INT_MATCH_WIDTH
        End If
        If INT_MATCH_HEIGHT <= 0 Then
            intHEIGHT = Me.ActualHeight
        Else
            intHEIGHT = INT_MATCH_HEIGHT
        End If

        Select Case ENM_RATE
            Case ENM_WINDOW_RATE.RATE_FREE
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = intHEIGHT
                intWIDTH_02 = intWIDTH
                intHEIGHT_02 = intHEIGHT
            Case ENM_WINDOW_RATE.RATE_1_1
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 1) * 1)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 1) * 1)
            Case ENM_WINDOW_RATE.RATE_2_3
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 2) * 3)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 3) * 2)
            Case ENM_WINDOW_RATE.RATE_3_2
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 3) * 2)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 2) * 3)
            Case ENM_WINDOW_RATE.RATE_3_4
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 3) * 4)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 4) * 3)
            Case ENM_WINDOW_RATE.RATE_4_3
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 4) * 3)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 3) * 4)
            Case ENM_WINDOW_RATE.RATE_5_7
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 1000) * 1414)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 1414) * 1000)
            Case ENM_WINDOW_RATE.RATE_7_5
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 1414) * 1000)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 1000) * 1414)
            Case ENM_WINDOW_RATE.RATE_5_8
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 1000) * 1618)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 1618) * 1000)
            Case ENM_WINDOW_RATE.RATE_8_5
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 1618) * 1000)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 1000) * 1618)
            Case ENM_WINDOW_RATE.RATE_5_12
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 1000) * 2414)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 2414) * 1000)
            Case ENM_WINDOW_RATE.RATE_12_5
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 2414) * 1000)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 1000) * 2414)
            Case ENM_WINDOW_RATE.RATE_9_16
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 9) * 16)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 16) * 9)
            Case ENM_WINDOW_RATE.RATE_16_9
                intWIDTH_01 = intWIDTH
                intHEIGHT_01 = CInt((intWIDTH_01 / 16) * 9)
                intHEIGHT_02 = intHEIGHT
                intWIDTH_02 = CInt((intHEIGHT_02 / 9) * 16)
        End Select

        intAREA_01 = intWIDTH_01 * intHEIGHT_01
        intAREA_02 = intWIDTH_02 * intHEIGHT_02

        Dim SRT_RECT_WH As MOD_PROCESS_WINDOW.RECT_WH
        SRT_RECT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)
        Dim INT_CLIENT_WIDTH As Integer
        INT_CLIENT_WIDTH = SRT_RECT_WH.width
        Dim INT_CLIENT_HEIGHT As Integer
        INT_CLIENT_HEIGHT = SRT_RECT_WH.height

        If INT_MATCH_WIDTH <= 0 Then
            INT_MATCH_WIDTH = INT_CLIENT_WIDTH
        End If

        If INT_MATCH_HEIGHT <= 0 Then
            INT_MATCH_HEIGHT = INT_CLIENT_HEIGHT
        End If

        If intAREA_01 <= intAREA_02 Then '基本的には大きい方を採用する
            If INT_MATCH_WIDTH >= intWIDTH_02 And INT_MATCH_HEIGHT >= intHEIGHT_02 Then 'ただし画面サイズを超えていない場合だけ
                intWIDTH_SET = intWIDTH_02
                intHEIGHT_SET = intHEIGHT_02
            Else
                intWIDTH_SET = intWIDTH_01
                intHEIGHT_SET = intHEIGHT_01
            End If
        Else
            If INT_MATCH_WIDTH >= intWIDTH_01 And INT_MATCH_HEIGHT >= intHEIGHT_01 Then 'ただし画面サイズを超えていない場合だけ
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
        PCB_COMPOSITION.Width = intWIDTH_SET
        PCB_COMPOSITION.Height = intHEIGHT_SET
        Call SUB_DRAW_COMPOSTION(Me.DRAWED_COMPOSTION_TYPE)
        Call SUB_CHANGE_BUTTON_SIZE()
        Call SUB_PUT_GUIDE()
        BLN_SET_SIZE = False
    End Sub

#End Region

#Region "画面サイズ"

    Private Sub SUB_CHANGE_BUTTON_SIZE()

        Dim ENM_WINDOW As ENM_WINDOW_SIZE
        ENM_WINDOW = FUNC_GET_WINDOW_SIZE()

        Dim INT_SIZE As Integer
        Select Case ENM_WINDOW
            Case ENM_WINDOW_SIZE.SIZE_6
                INT_SIZE = 64
            Case ENM_WINDOW_SIZE.SIZE_5
                INT_SIZE = 56
            Case ENM_WINDOW_SIZE.SIZE_4
                INT_SIZE = 48
            Case ENM_WINDOW_SIZE.SIZE_3
                INT_SIZE = 40
            Case ENM_WINDOW_SIZE.SIZE_2
                INT_SIZE = 32
            Case ENM_WINDOW_SIZE.SIZE_1
                INT_SIZE = 24
            Case ENM_WINDOW_SIZE.SIZE_0
                INT_SIZE = 16
            Case Else
                INT_SIZE = 16
        End Select

        Dim THK_CURRENT As Thickness

        Const INT_MARGIN_ONE As Integer = 5

        Dim INT_X_CUR As Integer
        Dim INT_Y_CUR As Integer

        'カメラ
        Dim BTN_SET As System.Windows.Controls.Button
        BTN_SET = PCB_BUTTON_SHUTTER_JPEG
        Dim INT_BUTTON_NUM As Integer
        INT_BUTTON_NUM = 0
        INT_X_CUR = INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        BTN_SET = PCB_BUTTON_SHUTTER
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        INT_X_CUR += INT_SIZE

        'PU,PD
        BTN_SET = PCB_BUTTON_KEY_PAGE_DOWN
        INT_BUTTON_NUM = 0
        INT_X_CUR = INT_X_CUR + INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        BTN_SET = PCB_BUTTON_KEY_PAGE_UP
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        INT_X_CUR += INT_SIZE

        '↑→↓←
        BTN_SET = PCB_BUTTON_KEY_ALLOW_RIGHT
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR + INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        INT_Y_CUR = CInt(INT_Y_CUR / 2)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        INT_X_CUR += INT_SIZE

        BTN_SET = PCB_BUTTON_KEY_ALLOW_DOWN
        INT_BUTTON_NUM = 0
        INT_X_CUR = INT_X_CUR + INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        BTN_SET = PCB_BUTTON_KEY_ALLOW_UP
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        INT_X_CUR += INT_SIZE

        BTN_SET = PCB_BUTTON_KEY_ALLOW_LEFT
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR + INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        INT_Y_CUR = CInt(INT_Y_CUR / 2)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        INT_X_CUR += INT_SIZE

        'WASD
        BTN_SET = PCB_BUTTON_KEY_D
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR + INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        INT_Y_CUR = CInt(INT_Y_CUR / 2)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        INT_X_CUR += INT_SIZE

        BTN_SET = PCB_BUTTON_KEY_S
        INT_BUTTON_NUM = 0
        INT_X_CUR = INT_X_CUR + INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        BTN_SET = PCB_BUTTON_KEY_W
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

        INT_X_CUR += INT_SIZE

        BTN_SET = PCB_BUTTON_KEY_A
        INT_BUTTON_NUM = 1
        INT_X_CUR = INT_X_CUR + INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        INT_Y_CUR = CInt(INT_Y_CUR / 2)
        BTN_SET.Width = INT_SIZE
        BTN_SET.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        BTN_SET.Margin = THK_CURRENT

    End Sub


    Private Function FUNC_GET_WINDOW_SIZE() As ENM_WINDOW_SIZE
        Dim INT_WIDTH As Integer
        INT_WIDTH = Me.Width

        Dim INT_HEIGHT As Integer
        INT_HEIGHT = Me.Height

        Dim INT_WIDTH_NUM As Integer
        INT_WIDTH_NUM = Math.Floor(INT_WIDTH / 4)

        Dim INT_HEIGHT_NUM As Integer
        INT_HEIGHT_NUM = Math.Floor(INT_WIDTH / 3)

        Dim INT_VALUE_CULC As Integer
        If INT_HEIGHT_NUM > INT_WIDTH_NUM Then
            INT_VALUE_CULC = INT_WIDTH_NUM
        Else
            INT_VALUE_CULC = INT_HEIGHT_NUM
        End If

        Dim ENM_RET As ENM_WINDOW_SIZE
        Select Case INT_VALUE_CULC
            Case >= 240
                ENM_RET = ENM_WINDOW_SIZE.SIZE_6
            Case >= 200
                ENM_RET = ENM_WINDOW_SIZE.SIZE_5
            Case >= 160
                ENM_RET = ENM_WINDOW_SIZE.SIZE_4
            Case >= 120
                ENM_RET = ENM_WINDOW_SIZE.SIZE_3
            Case >= 80
                ENM_RET = ENM_WINDOW_SIZE.SIZE_2
            Case >= 40
                ENM_RET = ENM_WINDOW_SIZE.SIZE_1
            Case Else
                ENM_RET = ENM_WINDOW_SIZE.SIZE_0
        End Select

        Return ENM_RET
    End Function
#End Region

#Region "その他"

    Private Function FUNC_GET_RECT_ME_TRIMING() As RECT_WH
        Dim SRT_RET As RECT_WH

        With SRT_RET
            Dim SRT_WINDOW_RECT_WH As RECT_WH
            SRT_WINDOW_RECT_WH = FUNC_GET_WINDOW_RECT_WH(PRC_APP_TARGET)

            Dim SRT_CRIENT_RECT_WH As RECT_WH
            SRT_CRIENT_RECT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)

            Dim INT_BORDER As Integer
            Dim INT_WIDTH_SUB As Integer
            Dim INT_HEIGHT_SUB As Integer

            If SRT_WINDOW_RECT_WH.left = 0 _
            And SRT_WINDOW_RECT_WH.top = 0 _
            And SRT_WINDOW_RECT_WH.width = SRT_CRIENT_RECT_WH.width _
            And SRT_WINDOW_RECT_WH.height = SRT_CRIENT_RECT_WH.height _
            Then '仮想フルスクリーン
                INT_WIDTH_SUB = 0
                INT_BORDER = 0
                INT_HEIGHT_SUB = 0
            Else
                INT_WIDTH_SUB = SRT_WINDOW_RECT_WH.width - SRT_CRIENT_RECT_WH.width
                INT_BORDER = Math.Floor(INT_WIDTH_SUB / 2)

                INT_HEIGHT_SUB = SRT_WINDOW_RECT_WH.height - SRT_CRIENT_RECT_WH.height
            End If

            .left = (Me.Left - SRT_WINDOW_RECT_WH.left) - (INT_BORDER)
            .top = (Me.Top - SRT_WINDOW_RECT_WH.top) - (INT_HEIGHT_SUB - INT_BORDER)
            .width = Me.Width
            .height = Me.Height
        End With

        Return SRT_RET
    End Function

    Private Sub SUB_COMP_ALL_UNCHECKED()
        MNI_COMP_NONE.IsChecked = False
        MNI_COMP_2DIV.IsChecked = False
        MNI_COMP_3DIV.IsChecked = False
        MNI_COMP_4DIV.IsChecked = False
        MNI_COMP_3DIV_PHI.IsChecked = False
        MNI_COMP_CROSS_DIV.IsChecked = False
        MNI_COMP_DIAGONAL_DIV_LL.IsChecked = False
        MNI_COMP_DIAGONAL_DIV_UL.IsChecked = False
        MNI_COMP_V_SHAPE_DOWN.IsChecked = False
        MNI_COMP_V_SHAPE_UP.IsChecked = False
        MNI_COMP_V_SHAPE_RIGHT.IsChecked = False
        MNI_COMP_V_SHAPE_LEFT.IsChecked = False
        MNI_COMP_HARMONIOUS_TRIANGLE_HLL.IsChecked = False
        MNI_COMP_HARMONIOUS_TRIANGLE_HUL.IsChecked = False
        MNI_COMP_HARMONIOUS_TRIANGLE_VLL.IsChecked = False
        MNI_COMP_HARMONIOUS_TRIANGLE_VUL.IsChecked = False
        MNI_COMP_GOLDEN_TRIANGLE_HLL.IsChecked = False
        MNI_COMP_GOLDEN_TRIANGLE_HUL.IsChecked = False
        MNI_COMP_GOLDEN_TRIANGLE_VLL.IsChecked = False
        MNI_COMP_GOLDEN_TRIANGLE_VUL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_HLL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_HUR.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_HUL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_HLR.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VLL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VUR.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VUL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VLR.IsChecked = False
        MNI_COMP_USER_01.IsChecked = False
        MNI_COMP_USER_02.IsChecked = False
        MNI_COMP_USER_03.IsChecked = False
        MNI_COMP_USER_04.IsChecked = False
        MNI_COMP_USER_05.IsChecked = False
        MNI_COMP_USER_06.IsChecked = False
        MNI_COMP_USER_07.IsChecked = False
        MNI_COMP_USER_08.IsChecked = False
        MNI_COMP_USER_09.IsChecked = False
    End Sub

    Private Sub SUB_RATE_ALL_UNCHECKED()
        MNI_RATE_FREE.IsChecked = False
        MNI_RATE_1_1.IsChecked = False
        MNI_RATE_2_3.IsChecked = False
        MNI_RATE_3_2.IsChecked = False
        MNI_RATE_3_4.IsChecked = False
        MNI_RATE_4_3.IsChecked = False
        MNI_RATE_5_7.IsChecked = False
        MNI_RATE_7_5.IsChecked = False
        MNI_RATE_5_8.IsChecked = False
        MNI_RATE_8_5.IsChecked = False
        MNI_RATE_5_12.IsChecked = False
        MNI_RATE_12_5.IsChecked = False
        MNI_RATE_9_16.IsChecked = False
        MNI_RATE_16_9.IsChecked = False
    End Sub

    Private Sub SUB_PUT_GUIDE()
        Dim STR_PUT As String

        STR_PUT = ""
        STR_PUT &= "X:" & Me.Left & System.Environment.NewLine
        STR_PUT &= "Y:" & Me.Top & System.Environment.NewLine
        STR_PUT &= "W:" & Me.ActualWidth & System.Environment.NewLine
        STR_PUT &= "H:" & Me.ActualHeight & System.Environment.NewLine

        LBL_GUIDE.Content = STR_PUT
    End Sub

    Private Sub SUB_CHK_OPACITY_CHECK_CHANGE()
        Dim COL_SET As System.Windows.Media.Color
        If CHK_OPACITY.IsChecked Then
            COL_SET = System.Windows.Media.Color.FromArgb(0, 0, 0, 255)
        Else
            COL_SET = System.Windows.Media.Color.FromArgb(16, 0, 0, 255)
        End If

        Me.Background = New System.Windows.Media.SolidColorBrush(COL_SET)
    End Sub

    Private Function FUNC_SHOW_CONFIRM(ByRef BMP_VIEW As System.Drawing.Bitmap, ByVal BLN_CR_DEFAULT As Boolean) As WPF_SAVE_CONFIRM.SRT_RETURN_SAVE_CONFIRM
        Dim WPF_SHOW As WPF_SAVE_CONFIRM
        WPF_SHOW = New WPF_SAVE_CONFIRM

        WPF_SHOW.VIEW_IMAGE = BMP_VIEW.Clone
        WPF_SHOW.ADD_COPYRIGHT_DEFAULT = BLN_CR_DEFAULT
        Call WPF_SHOW.ShowDialog()

        Dim SRT_RET As WPF_SAVE_CONFIRM.SRT_RETURN_SAVE_CONFIRM
        SRT_RET = WPF_SHOW.RETURN_CONFIRM
        WPF_SHOW = Nothing

        Return SRT_RET
    End Function

    Public Sub SUB_SET_SIZE_AND_LOCATION_DEFAULT()
        BLN_SET_SIZE = True
        Me.Left = SRT_APP_SETTINGS_VALUE.TRIM.LOCATION.LEFT
        Me.Top = SRT_APP_SETTINGS_VALUE.TRIM.LOCATION.TOP
        Me.Width = SRT_APP_SETTINGS_VALUE.TRIM.SIZE.WIDTH
        Me.Height = SRT_APP_SETTINGS_VALUE.TRIM.SIZE.HEIGHT
        Call SUB_PUT_GUIDE()
        BLN_SET_SIZE = False
        Call SUB_CHANGE_RATE(SRT_APP_SETTINGS_VALUE.TRIM.ASPECT_RATIO.TYPE)
        Call SUB_CHANGE_DRAW_COMPOTION(SRT_APP_SETTINGS_VALUE.TRIM.COMPOTION.TYPE)
    End Sub
#End Region

#Region "NEW"
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        AddHandler Me.MouseLeftButtonDown, Sub(sender, e) Me.DragMove() 'ウィンドウをマウスのドラッグで移動できるようにする

        Call SUB_START_TIMER_TOPMOST()
    End Sub

#End Region

#Region "イベント-ボタンクリック"

    Private Sub PCB_BUTTON_SHUTTER_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_SHUTTER.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CAPTURE)
    End Sub

    Private Sub PCB_BUTTON_SHUTTER_JPEG_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_SHUTTER_JPEG.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CAPTURE_JPEG)
    End Sub

    Private Sub PCB_BUTTON_KEY_W_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_W.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_W)
    End Sub

    Private Sub PCB_BUTTON_KEY_A_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_A.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_A)
    End Sub

    Private Sub PCB_BUTTON_KEY_S_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_S.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_S)
    End Sub

    Private Sub PCB_BUTTON_KEY_D_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_D.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_D)
    End Sub

    Private Sub PCB_BUTTON_KEY_ALLOW_UP_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_ALLOW_UP.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_ALLOW_UP)
    End Sub

    Private Sub PCB_BUTTON_KEY_ALLOW_LEFT_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_ALLOW_LEFT.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_ALLOW_LEFT)
    End Sub

    Private Sub PCB_BUTTON_KEY_ALLOW_DOWN_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_ALLOW_DOWN.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_ALLOW_DOWN)
    End Sub

    Private Sub PCB_BUTTON_KEY_ALLOW_RIGHT_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_ALLOW_RIGHT.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_ALLOW_RIGHT)
    End Sub

    Private Sub PCB_BUTTON_KEY_PAGE_UP_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_PAGE_UP.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_PAGE_UP)
    End Sub

    Private Sub PCB_BUTTON_KEY_PAGE_DOWN_Click(sender As Object, e As RoutedEventArgs) Handles PCB_BUTTON_KEY_PAGE_DOWN.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_KEY_PAGE_DOWN)
    End Sub
#End Region

#Region "イベント-コンテキストメニュークリック"

    Private Sub MNI_CAPT_Click(sender As Object, e As RoutedEventArgs) Handles MNI_CAPT.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CAPTURE)
    End Sub

    Private Sub MNI_CAPT_SNS_Click(sender As Object, e As RoutedEventArgs) Handles MNI_CAPT_SNS.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_CAPTURE_JPEG)
    End Sub

    Private Sub MNI_CLOSE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_CLOSE.Click
        Call Me.Hide()
    End Sub

    Private Sub MNI_FIT_WINDOW_Click(sender As Object, e As RoutedEventArgs) Handles MNI_FIT_WINDOW.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_FIT_WINDOW)
    End Sub

    Private Sub MNI_FIT_WINDOW_VERTICAL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_FIT_WINDOW_VERTICAL.Click
        Call SUB_EXEC_DO(ENM_WINDOW_EXEC.DO_FIT_WINDOW_VERTICAL)
    End Sub

#Region "縦横比"
    Private Sub MNI_RATE_FREE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_FREE.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_FREE)
    End Sub

    Private Sub MNI_RATE_1_1_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_1_1.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_1_1)
    End Sub

    Private Sub MNI_RATE_2_3_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_2_3.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_2_3)
    End Sub

    Private Sub MNI_RATE_3_2_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_3_2.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_3_2)
    End Sub

    Private Sub MNI_RATE_3_4_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_3_4.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_3_4)
    End Sub

    Private Sub MNI_RATE_4_3_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_4_3.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_4_3)
    End Sub

    Private Sub MNI_RATE_5_7_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_5_7.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_5_7)
    End Sub

    Private Sub MNI_RATE_7_5_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_7_5.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_7_5)
    End Sub

    Private Sub MNI_RATE_5_8_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_5_8.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_5_8)
    End Sub

    Private Sub MNI_RATE_8_5_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_8_5.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_8_5)
    End Sub

    Private Sub MNI_RATE_5_12_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_5_12.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_5_12)
    End Sub

    Private Sub MNI_RATE_12_5_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_12_5.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_12_5)
    End Sub

    Private Sub MNI_RATE_9_16_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_9_16.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_9_16)
    End Sub

    Private Sub MNI_RATE_16_9_Click(sender As Object, e As RoutedEventArgs) Handles MNI_RATE_16_9.Click
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_16_9)
    End Sub
#End Region

#Region "構図補助"
    Private Sub MNI_COMP_NONE_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_NONE.Click
        Call SUB_CHANGE_DRAW_COMPOTION(-1)
    End Sub

    Private Sub MNI_COMP_2DIV_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_2DIV.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.TWO_DIV)
    End Sub

    Private Sub MNI_COMP_3DIV_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_3DIV.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.THREE_DIV)
    End Sub

    Private Sub MNI_COMP_4DIV_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_4DIV.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.QUAD_DIV)
    End Sub

    Private Sub MNI_COMP_3DIV_PHI_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_3DIV_PHI.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.THREE_DIV_PHI)
    End Sub

    Private Sub MNI_COMP_CROSS_DIV_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_CROSS_DIV.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.CROSS_DIV)
    End Sub

    Private Sub MNI_COMP_DIAGONAL_DIV_LL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_DIAGONAL_DIV_LL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.DIAGONAL_DIV_LL)
    End Sub

    Private Sub MNI_COMP_DIAGONAL_DIV_UL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_DIAGONAL_DIV_UL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.DIAGONAL_DIV_UL)
    End Sub

    Private Sub MNI_COMP_V_SHAPE_DOWN_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_V_SHAPE_DOWN.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.V_SHAPE_DOWN)
    End Sub

    Private Sub MNI_COMP_V_SHAPE_UP_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_V_SHAPE_UP.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.V_SHAPE_UP)
    End Sub

    Private Sub MNI_COMP_V_SHAPE_RIGHT_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_V_SHAPE_RIGHT.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.V_SHAPE_RIGHT)
    End Sub

    Private Sub MNI_COMP_V_SHAPE_LEFT_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_V_SHAPE_LEFT.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.V_SHAPE_LEFT)
    End Sub

    Private Sub MNI_COMP_HARMONIOUS_TRIANGLE_HLL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_HARMONIOUS_TRIANGLE_HLL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_HLL)
    End Sub

    Private Sub MNI_COMP_HARMONIOUS_TRIANGLE_HUL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_HARMONIOUS_TRIANGLE_HUL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_HUL)
    End Sub

    Private Sub MNI_COMP_HARMONIOUS_TRIANGLE_VLL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_HARMONIOUS_TRIANGLE_VLL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_VLL)
    End Sub

    Private Sub MNI_COMP_HARMONIOUS_TRIANGLE_VUL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_HARMONIOUS_TRIANGLE_VUL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.HARMONIOUS_TRIANGLE_VUL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_TRIANGLE_HLL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_TRIANGLE_HLL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_HLL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_TRIANGLE_HUL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_TRIANGLE_HUL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_HUL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_TRIANGLE_VLL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_TRIANGLE_VLL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_VLL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_TRIANGLE_VUL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_TRIANGLE_VUL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_TRIANGLE_VUL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_HLL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_HLL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_HUR_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_HUR.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUR)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_HUL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_HUL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_HLR_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_HLR.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLR)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_VLL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_VLL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_VUR_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_VUR.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUR)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_VUL_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_VUL.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUL)
    End Sub

    Private Sub MNI_COMP_GOLDEN_RECTANGLE_VLR_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_GOLDEN_RECTANGLE_VLR.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLR)
    End Sub

    Private Sub MNI_COMP_USER_01_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_01.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_01)
    End Sub

    Private Sub MNI_COMP_USER_02_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_02.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_02)
    End Sub

    Private Sub MNI_COMP_USER_03_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_03.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_03)
    End Sub

    Private Sub MNI_COMP_USER_04_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_04.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_04)
    End Sub

    Private Sub MNI_COMP_USER_05_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_05.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_05)
    End Sub

    Private Sub MNI_COMP_USER_06_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_06.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_06)
    End Sub

    Private Sub MNI_COMP_USER_07_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_07.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_07)
    End Sub

    Private Sub MNI_COMP_USER_08_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_08.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_08)
    End Sub

    Private Sub MNI_COMP_USER_09_Click(sender As Object, e As RoutedEventArgs) Handles MNI_COMP_USER_09.Click
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.USER_09)
    End Sub
#End Region

#End Region

#Region "イベント-チェック"
    Private Sub CHK_OPACITY_Checked(sender As Object, e As RoutedEventArgs) Handles CHK_OPACITY.Checked

        Call SUB_CHK_OPACITY_CHECK_CHANGE()
    End Sub

    Private Sub CHK_OPACITY_Unchecked(sender As Object, e As RoutedEventArgs) Handles CHK_OPACITY.Unchecked
        Call SUB_CHK_OPACITY_CHECK_CHANGE()
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

    Private Sub SUB_STOP_DISPATCHER_TIMER()
        If Not (TIM_TOPMOST Is Nothing) Then
            Call TIM_TOPMOST.Stop()
            TIM_TOPMOST.IsEnabled = False
            RemoveHandler TIM_TOPMOST.Tick, AddressOf TIM_TOPMOST_TICK
            TIM_TOPMOST = Nothing
        End If
    End Sub

#End Region

    Private Sub TIM_TOPMOST_TICK(sender As Object, e As EventArgs)

        If Not Me.Visibility = Visibility.Visible Then
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
#End Region

#Region "イベント-キーアップ"
    Private Sub SUB_KEY_UP(ByVal ENM_KEY As System.Windows.Input.Key)

        Dim INT_LEFT As Integer
        Dim INT_TOP As Integer

        Select Case ENM_KEY
            Case Key.Up
                INT_LEFT = Me.Left
                INT_TOP = Me.Top - 1
            Case Key.Right
                INT_LEFT = Me.Left + 1
                INT_TOP = Me.Top
            Case Key.Down
                INT_LEFT = Me.Left
                INT_TOP = Me.Top + 1
            Case Key.Left
                INT_LEFT = Me.Left - 1
                INT_TOP = Me.Top
            Case Else
                INT_LEFT = Me.Left
                INT_TOP = Me.Top
        End Select

        Me.Left = INT_LEFT
        Me.Top = INT_TOP
    End Sub

    Private Sub SUB_KEY_UP_CTRL(ByVal ENM_KEY As System.Windows.Input.Key)
        Dim INT_W As Integer
        Dim INT_H As Integer

        Select Case ENM_KEY
            Case Key.Up
                INT_W = Me.Width
                INT_H = Me.Height - 1
            Case Key.Right
                INT_W = Me.Width + 1
                INT_H = Me.Height
            Case Key.Down
                INT_W = Me.Width
                INT_H = Me.Height + 1
            Case Key.Left
                INT_W = Me.Width - 1
                INT_H = Me.Height
            Case Else
                INT_W = Me.Width
                INT_H = Me.Height
        End Select

        Me.Width = INT_W
        Me.Height = INT_H
    End Sub

    Private Sub SUB_GET_KEY_MASK()
        Dim ENM_STATES As KeyStates

        SRT_MASK.SHIFT = False
        ENM_STATES = Keyboard.GetKeyStates(Key.LeftShift)
        If (ENM_STATES And KeyStates.Down) = KeyStates.Down Then
            SRT_MASK.SHIFT = True
        End If
        ENM_STATES = Keyboard.GetKeyStates(Key.RightShift)
        If (ENM_STATES And KeyStates.Down) = KeyStates.Down Then
            SRT_MASK.SHIFT = True
        End If

        SRT_MASK.CTRL = False
        ENM_STATES = Keyboard.GetKeyStates(Key.LeftCtrl)
        If (ENM_STATES And KeyStates.Down) = KeyStates.Down Then
            SRT_MASK.CTRL = True
        End If
        ENM_STATES = Keyboard.GetKeyStates(Key.RightCtrl)
        If (ENM_STATES And KeyStates.Down) = KeyStates.Down Then
            SRT_MASK.CTRL = True
        End If

        SRT_MASK.ALT = False
        ENM_STATES = Keyboard.GetKeyStates(Key.LeftAlt)
        If (ENM_STATES And KeyStates.Down) = KeyStates.Down Then
            SRT_MASK.ALT = True
        End If
        ENM_STATES = Keyboard.GetKeyStates(Key.RightAlt)
        If (ENM_STATES And KeyStates.Down) = KeyStates.Down Then
            SRT_MASK.ALT = True
        End If
    End Sub
#End Region

    Private Sub WPF_TRIM_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CTRL_VALUE_INIT()
    End Sub

    Private Sub WPF_TRIM_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub WPF_TRIM_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub

    Private Sub WPF_TRIM_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If BLN_SET_SIZE Then
            Exit Sub
        End If
        Call SUB_SET_RATE(ENM_RATE_CURRENT)
        Call SUB_PUT_GUIDE()

        SRT_APP_SETTINGS_VALUE.TRIM.SIZE.WIDTH = Me.Width
        SRT_APP_SETTINGS_VALUE.TRIM.SIZE.HEIGHT = Me.Height

    End Sub

    Private Sub WPF_TRIM_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If BLN_SET_SIZE Then
            Exit Sub
        End If
        Call SUB_PUT_GUIDE()

        SRT_APP_SETTINGS_VALUE.TRIM.LOCATION.LEFT = Me.Left
        SRT_APP_SETTINGS_VALUE.TRIM.LOCATION.TOP = Me.Top

    End Sub

    Private Sub WPF_TRIM_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        Call SUB_GET_KEY_MASK()

        Select Case True
            Case SRT_MASK.SHIFT
            Case SRT_MASK.CTRL
                Call SUB_KEY_UP_CTRL(e.Key)
            Case SRT_MASK.ALT
            Case Else
                Call SUB_KEY_UP(e.Key)
        End Select
    End Sub

    Private Sub WPF_TRIM_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

    End Sub

End Class
