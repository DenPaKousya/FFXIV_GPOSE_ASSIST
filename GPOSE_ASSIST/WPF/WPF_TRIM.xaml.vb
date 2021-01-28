Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input

Public Class WPF_TRIM

#Region "画面用・列挙定数"
    Private Enum ENM_WINDOW_EXEC
        DO_CAPTURE = 0
        DO_CAPTURE_JPEG
        DO_FIT_WINDOW
    End Enum

    Private Enum ENM_DIVISION_PATTERN
        TWO_DIV = 2
        THREE_DIV = 3
        QUAD_DIV = 4
        THREE_DIV_PHI = 5

        RATE_16_TO_9 = 6
        RATE_3_TO_2 = 7

        GOLDEN_RECTANGLE_HLL = 8
        GOLDEN_RECTANGLE_HUR = 9
        GOLDEN_RECTANGLE_HUL = 10
        GOLDEN_RECTANGLE_HLR = 11

        GOLDEN_RECTANGLE_VLL = 12
        GOLDEN_RECTANGLE_VUR = 13
        GOLDEN_RECTANGLE_VUL = 14
        GOLDEN_RECTANGLE_VLR = 15

        MIN = TWO_DIV
        MAX = GOLDEN_RECTANGLE_VLL
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

#Region "画面用・変数"
    Private BLN_WINDOW_EXEC_DO As Boolean = False

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
            Case ENM_WINDOW_EXEC.DO_FIT_WINDOW
                Call SUB_FIT_WINDOW()
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

        Call SUB_FIXED_PHRASE_INIT(Now, SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX)
        Dim SRT_SAVE As SRT_SAVE_IMAGE_PARAM
        With SRT_SAVE
            .SAVE_DIR = FUNC_GET_NAME_SAVE_DIR(SRT_APP_SETTINGS_VALUE.SAVE.DIRECTORY, SRT_APP_SETTINGS_VALUE.SAVE.FILE.DIRECTORY)
            .SAVE_NAME_FILE = FUNC_GET_NAME_SAVE_FILE(SRT_APP_SETTINGS_VALUE.SAVE.FILE.NAME)
            .TYPE = FUNC_GET_TYPE_IMAGE01_FROM_STRING(SRT_APP_SETTINGS_VALUE.SAVE.FILE.TYPE)
            .QUALITY = SRT_APP_SETTINGS_VALUE.SAVE.FILE.QUALITY
            .ADD_CL = FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.SAVE.FILE.COPYRIGHT)
            .EXIF = FUNC_GET_EXIF_DEFAULT(SRT_APP_SETTINGS_VALUE.PROCESS_NAME, Now)
            .SAVE_NAME_FILE_REAL = FUNC_GET_NAME_FILE_REAL(.SAVE_DIR, .SAVE_NAME_FILE)
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
            .ADD_CL = True
            .EXIF = SRT_SAVE.EXIF
            .SAVE_NAME_FILE_REAL = SRT_SAVE.SAVE_NAME_FILE_REAL
        End With
        If Not FUNC_SAVE_IMAGE(BMP_MAKE, SRT_SAVE_02) Then
            Exit Sub
        End If

        SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX += 1
        Call My.Computer.Audio.Play(My.Resources.SHUTTER_SHORT, Microsoft.VisualBasic.AudioPlayMode.Background)
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

        Call SUB_FIXED_PHRASE_INIT(Now, SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX)
        Dim SRT_SAVE As SRT_SAVE_IMAGE_PARAM
        With SRT_SAVE
            .SAVE_DIR = FUNC_GET_NAME_SAVE_DIR(SRT_APP_SETTINGS_VALUE.SAVE.DIRECTORY, SRT_APP_SETTINGS_VALUE.SAVE.FILE.DIRECTORY)
            .SAVE_NAME_FILE = FUNC_GET_NAME_SAVE_FILE(SRT_APP_SETTINGS_VALUE.SAVE.FILE.NAME)
            .TYPE = ENM_IMAGE_TYPE.JPEG
            .QUALITY = SRT_APP_SETTINGS_VALUE.SAVE.FILE.QUALITY
            .ADD_CL = True
            .EXIF = FUNC_GET_EXIF_DEFAULT(SRT_APP_SETTINGS_VALUE.PROCESS_NAME, Now)
            .SAVE_NAME_FILE_REAL = FUNC_GET_NAME_FILE_REAL(.SAVE_DIR, .SAVE_NAME_FILE)
        End With
        If Not FUNC_SAVE_IMAGE(BMP_MAKE, SRT_SAVE) Then
            Exit Sub
        End If

        SRT_APP_SETTINGS_VALUE.SAVE.FILE.INDEX += 1
        Call My.Computer.Audio.Play(My.Resources.SHUTTER_SHORT, Microsoft.VisualBasic.AudioPlayMode.Background)
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

        Me.Left = INT_CLIENT_LEFT
        Me.Top = INT_CLIENT_TOP
        BLN_SET_SIZE = True
        Me.Width = INT_CLIENT_WIDTH
        Me.Height = INT_CLIENT_HEIGHT
        BLN_SET_SIZE = False
        Call SUB_SET_RATE(ENM_RATE_CURRENT)

        If ENM_RATE_CURRENT = ENM_WINDOW_RATE.RATE_FREE Then
            Exit Sub
        End If

        'アスペクト比フリー以外の場合は位置の調整を行う
        '余白部分を中央寄せ
        Dim INT_WIDTH_SUB As Integer
        INT_WIDTH_SUB = INT_CLIENT_WIDTH - Me.Width
        Dim INT_HEIGHT_SUB As Integer
        INT_HEIGHT_SUB = INT_CLIENT_HEIGHT - Me.Height

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
            Case Else
                MNI_COMP_NONE.IsChecked = True
        End Select

        Call SUB_DRAW_COMPOSTION(ENM_PAT)

        Me.DRAWED_COMPOSTION_TYPE = ENM_PAT
    End Sub

    Private Sub SUB_DRAW_COMPOSTION(ByVal enmPAT As ENM_DIVISION_PATTERN)
        Dim intWIDTH As Integer
        Dim intHEIGHT As Integer
        intWIDTH = CInt(PCB_COMPOSITION.Width)
        intHEIGHT = CInt(PCB_COMPOSITION.Height)

        Dim bmpCANVAS As Bitmap
        bmpCANVAS = New Bitmap(intWIDTH, intHEIGHT)

        Dim grpDRAW As Graphics
        grpDRAW = Graphics.FromImage(bmpCANVAS)

        Const cstPEN_SIZE As Single = 3.0
        Dim penDRAW As System.Drawing.Pen
        'penDRAW = New System.Drawing.Pen(System.Drawing.Color.Cyan, cstPEN_SIZE)
        penDRAW = New System.Drawing.Pen(System.Drawing.Color.FromArgb(64, System.Drawing.Color.Black), cstPEN_SIZE)

        Dim penFORE As System.Drawing.Pen
        penFORE = New System.Drawing.Pen(System.Drawing.Color.White, 1.0)

        grpDRAW.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Call SUB_DRAW_OUTLINE(grpDRAW, penDRAW)
        Call SUB_DRAW_OUTLINE(grpDRAW, penFORE)

        Select Case enmPAT
            Case ENM_DIVISION_PATTERN.TWO_DIV
                Call SUB_DRAW_TWO_DIV(grpDRAW, penDRAW)
                Call SUB_DRAW_TWO_DIV(grpDRAW, penFORE)
            Case ENM_DIVISION_PATTERN.THREE_DIV
                Call SUB_DRAW_THREE_DIV(grpDRAW, penDRAW)
                Call SUB_DRAW_THREE_DIV(grpDRAW, penFORE)
            Case ENM_DIVISION_PATTERN.QUAD_DIV
                Call SUB_DRAW_QUAD_DIV(grpDRAW, penDRAW)
                Call SUB_DRAW_QUAD_DIV(grpDRAW, penFORE)
            Case ENM_DIVISION_PATTERN.THREE_DIV_PHI
                Call SUB_DRAW_THREE_DIV_PHI(grpDRAW, penDRAW)
                Call SUB_DRAW_THREE_DIV_PHI(grpDRAW, penFORE)
            Case ENM_DIVISION_PATTERN.RATE_16_TO_9

            Case ENM_DIVISION_PATTERN.RATE_3_TO_2

            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_LEFT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_LEFT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_RIGHT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_RIGHT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HUL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_LEFT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_LEFT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_HLR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_RIGHT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_RIGHT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_LEFT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_LEFT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_RIGHT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_RIGHT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VUL
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_LEFT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_LEFT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case ENM_DIVISION_PATTERN.GOLDEN_RECTANGLE_VLR
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_RIGHT, grpDRAW, penDRAW, PCB_COMPOSITION)
                Call SUB_DRAW_GOLDEN_RECTANGLE(ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_RIGHT, grpDRAW, penFORE, PCB_COMPOSITION)
            Case Else

        End Select

        PCB_COMPOSITION.Source = FUNC_GET_IMAGESOURCE(bmpCANVAS)
        Call grpDRAW.Dispose()

    End Sub

    Private Sub SUB_DRAW_OUTLINE(ByRef grpDRAW As Graphics, ByRef penDRAW As System.Drawing.Pen)
        Dim rct As System.Drawing.Rectangle

        rct = New System.Drawing.Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Call grpDRAW.DrawRectangle(penDRAW, rct)
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
    End Sub

    Private BLN_SET_SIZE As Boolean = False
    Private Sub SUB_SET_RATE(ByVal ENM_RATE As ENM_WINDOW_RATE)
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

        intWIDTH = Me.ActualWidth
        intHEIGHT = Me.ActualHeight

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
        Dim intWIDTH_CLIENT As Integer
        Dim intHEIGHT_CLIENT As Integer
        intWIDTH_CLIENT = SRT_RECT_WH.width
        intHEIGHT_CLIENT = SRT_RECT_WH.height

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
        Dim INT_BUTTON_NUM As Integer

        INT_BUTTON_NUM = 0
        INT_X_CUR = INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        PCB_BUTTON_SHUTTER_JPEG.Width = INT_SIZE
        PCB_BUTTON_SHUTTER_JPEG.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        PCB_BUTTON_SHUTTER_JPEG.Margin = THK_CURRENT

        INT_BUTTON_NUM += 1
        INT_X_CUR = INT_MARGIN_ONE
        INT_Y_CUR = (INT_MARGIN_ONE * (INT_BUTTON_NUM + 1)) + (INT_SIZE * INT_BUTTON_NUM)
        PCB_BUTTON_SHUTTER.Width = INT_SIZE
        PCB_BUTTON_SHUTTER.Height = INT_SIZE
        THK_CURRENT = New Thickness(0, 0, INT_X_CUR, INT_Y_CUR)
        PCB_BUTTON_SHUTTER.Margin = THK_CURRENT
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

            Dim INT_BORDER_L As Integer
            Dim INT_BORDER_T As Integer
            If SRT_WINDOW_RECT_WH.left = 0 _
            And SRT_WINDOW_RECT_WH.top = 0 _
            And SRT_WINDOW_RECT_WH.width = SRT_CRIENT_RECT_WH.width _
            And SRT_WINDOW_RECT_WH.height = SRT_CRIENT_RECT_WH.height _
            Then '仮想フルスクリーン
                INT_BORDER_L = 0
                INT_BORDER_T = 0
            Else
                Dim INT_WIDTH_SUB As Integer
                INT_WIDTH_SUB = SRT_WINDOW_RECT_WH.width - SRT_CRIENT_RECT_WH.width
                INT_BORDER_L = SRT_WINDOW_RECT_WH.left + Math.Floor(INT_WIDTH_SUB / 2)

                Dim INT_HEIGHT_SUB As Integer
                INT_HEIGHT_SUB = SRT_WINDOW_RECT_WH.height - SRT_CRIENT_RECT_WH.height
                INT_BORDER_T = SRT_WINDOW_RECT_WH.top + Math.Floor(INT_HEIGHT_SUB / 2)
            End If

            .left = Me.Left - INT_BORDER_L
            .top = Me.Top - INT_BORDER_T
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
        MNI_COMP_GOLDEN_RECTANGLE_HLL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_HUR.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_HUL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_HLR.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VLL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VUR.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VUL.IsChecked = False
        MNI_COMP_GOLDEN_RECTANGLE_VLR.IsChecked = False
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
        Dim COL_SET As Windows.Media.Color
        If CHK_OPACITY.IsChecked Then
            COL_SET = Windows.Media.Color.FromArgb(0, 0, 0, 255)
        Else
            COL_SET = Windows.Media.Color.FromArgb(16, 0, 0, 255)
        End If

        Me.Background = New System.Windows.Media.SolidColorBrush(COL_SET)
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

    Private Sub WPF_TRIM_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call SUB_CHANGE_RATE(ENM_WINDOW_RATE.RATE_3_2)
        Call SUB_CHANGE_DRAW_COMPOTION(ENM_DIVISION_PATTERN.THREE_DIV)
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
    End Sub

    Private Sub WPF_TRIM_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        Call SUB_PUT_GUIDE()
    End Sub

    Private Sub WPF_TRIM_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Dim INT_LEFT As Integer
        Dim INT_TOP As Integer
        Select Case e.Key
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

End Class
