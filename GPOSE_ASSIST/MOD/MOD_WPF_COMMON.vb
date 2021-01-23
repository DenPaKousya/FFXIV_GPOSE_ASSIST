Public Module MOD_WPF_COMMON

    Public Enum ENM_POSITION_WPF_LOCATION
        UNKNOWN = -1
        CENTER = 0
        LEFT_TOP = 1
        LEFT_BOTTOM = 2
        RIGHT_TOP = 3
        RIGHT_BOTTOM = 4
    End Enum

    Public Sub SUB_SET_LOCATION_OVERLAY_WPF(
    ByRef WPF_OVERLAY As System.Windows.Window, ENM_POSITION_OVERLAY As ENM_POSITION_WPF_LOCATION
    )

        If ENM_POSITION_OVERLAY = ENM_POSITION_WPF_LOCATION.UNKNOWN Then
            Exit Sub
        End If

        Dim SRT_RECT_WINDOW As MOD_PROCESS_WINDOW.RECT_WH
        SRT_RECT_WINDOW = FUNC_GET_WINDOW_RECT_WH(PRC_APP_TARGET)
        Dim INT_WINDOW_LEFT As Integer
        Dim INT_WINDOW_TOP As Integer
        Dim INT_WINDOW_WIDTH As Integer
        Dim INT_WINDOW_HEIGHT As Integer
        INT_WINDOW_LEFT = SRT_RECT_WINDOW.left
        INT_WINDOW_TOP = SRT_RECT_WINDOW.top
        INT_WINDOW_WIDTH = SRT_RECT_WINDOW.width
        INT_WINDOW_HEIGHT = SRT_RECT_WINDOW.height

        Dim SRT_RECT_CLIENT As MOD_PROCESS_WINDOW.RECT_WH
        SRT_RECT_CLIENT = FUNC_GET_CRIENT_RECT_WH(PRC_APP_TARGET)
        Dim INT_CLIENT_WIDTH As Integer
        Dim INT_CLIENT_HEIGHT As Integer
        INT_CLIENT_WIDTH = SRT_RECT_CLIENT.width
        INT_CLIENT_HEIGHT = SRT_RECT_CLIENT.height

        Dim INT_LEFT As Integer
        Dim INT_TOP As Integer
        Select Case ENM_POSITION_OVERLAY
            Case ENM_POSITION_WPF_LOCATION.CENTER
                INT_LEFT = CInt(CDec(INT_WINDOW_WIDTH / 2) - CDec(WPF_OVERLAY.Width / 2))
                INT_TOP = CInt(CDec(INT_WINDOW_HEIGHT / 2) - CDec(WPF_OVERLAY.Height / 2))
            Case ENM_POSITION_WPF_LOCATION.LEFT_TOP
                INT_LEFT = 0
                INT_TOP = 0
            Case ENM_POSITION_WPF_LOCATION.LEFT_BOTTOM
                INT_LEFT = 0
                INT_TOP = INT_WINDOW_HEIGHT - WPF_OVERLAY.Height
            Case ENM_POSITION_WPF_LOCATION.RIGHT_TOP
                INT_LEFT = INT_WINDOW_WIDTH - WPF_OVERLAY.Width
                INT_TOP = 0
            Case ENM_POSITION_WPF_LOCATION.RIGHT_BOTTOM
                INT_LEFT = INT_WINDOW_WIDTH - WPF_OVERLAY.Width
                INT_TOP = INT_WINDOW_HEIGHT - WPF_OVERLAY.Height
            Case Else
                INT_LEFT = 0
                INT_TOP = 0
        End Select

        Dim INT_WIDTH_SUB As Integer
        Dim INT_HEIGHT_SUB As Integer
        INT_WIDTH_SUB = INT_WINDOW_WIDTH - INT_CLIENT_WIDTH
        INT_HEIGHT_SUB = INT_WINDOW_HEIGHT - INT_CLIENT_HEIGHT

        Dim INT_BORDER As Integer
        INT_BORDER = Math.Floor(INT_WIDTH_SUB / 2)

        Dim INT_LEFT_SIGN As Integer
        Select Case ENM_POSITION_OVERLAY
            Case ENM_POSITION_WPF_LOCATION.LEFT_TOP, ENM_POSITION_WPF_LOCATION.LEFT_BOTTOM
                INT_LEFT_SIGN = 1
            Case ENM_POSITION_WPF_LOCATION.CENTER
                INT_LEFT_SIGN = 1
            Case ENM_POSITION_WPF_LOCATION.RIGHT_TOP, ENM_POSITION_WPF_LOCATION.RIGHT_BOTTOM
                INT_LEFT_SIGN = -1
            Case Else
                INT_LEFT_SIGN = 1
        End Select
        Dim INT_WINDOW_LEFT_ADD As Integer
        INT_WINDOW_LEFT_ADD = INT_WINDOW_LEFT + (INT_LEFT_SIGN * INT_BORDER)

        Dim INT_TOP_SIGN As Integer
        Select Case ENM_POSITION_OVERLAY
            Case ENM_POSITION_WPF_LOCATION.LEFT_TOP, ENM_POSITION_WPF_LOCATION.RIGHT_TOP
                INT_TOP_SIGN = 1
            Case ENM_POSITION_WPF_LOCATION.CENTER
                INT_TOP_SIGN = 1
            Case ENM_POSITION_WPF_LOCATION.LEFT_BOTTOM, ENM_POSITION_WPF_LOCATION.RIGHT_BOTTOM
                INT_TOP_SIGN = -1
            Case Else
                INT_TOP_SIGN = 1
        End Select
        Dim INT_WINDOW_TOP_ADD As Integer
        INT_WINDOW_TOP_ADD = INT_WINDOW_TOP + (INT_TOP_SIGN * (INT_HEIGHT_SUB - INT_BORDER))

        INT_LEFT += INT_WINDOW_LEFT_ADD
        INT_TOP += INT_WINDOW_TOP_ADD

        Dim PNT_LOCATION As System.Drawing.Point
        PNT_LOCATION = New System.Drawing.Point(INT_LEFT, INT_TOP)
        Try
            WPF_OVERLAY.Left = INT_LEFT
            WPF_OVERLAY.Top = INT_TOP
        Catch ex As Exception

        End Try

    End Sub
End Module
