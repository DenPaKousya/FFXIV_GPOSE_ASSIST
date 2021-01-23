Public Module MOD_DRAW_GOLDEN_RATE

#Region "モジュール用・定数"
    Private Const CST_GOLDEN_RATE_L As Integer = 1618
    Private Const CST_GOLDEN_RATE_S As Integer = 1000
#End Region

#Region "モジュール用・列挙定数"
    Public Enum ENM_GOLDEN_RATE_TYPE
        HORIZONTAL_START_LOWER_LEFT = 1
        HORIZONTAL_START_UPPER_RIGHT = 2
        HORIZONTAL_START_UPPER_LEFT = 3
        HORIZONTAL_START_LOWER_RIGHT = 4
        VERTICAL_START_LOWER_LEFT = 5
        VERTICAL_START_UPPER_RIGHT = 6
        VERTICAL_START_UPPER_LEFT = 7
        VERTICAL_START_LOWER_RIGHT = 8
    End Enum
#End Region

#Region "モジュール用・構造体"
    Private Structure SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Public POINT_W As Integer
        Public POINT_H As Integer

        Public POINT_ARC_X As Integer
        Public POINT_ARC_Y As Integer
        Public ANGLE_START As Integer
        Public ANGLE_SWEEP As Integer
    End Structure

#End Region

    Public Sub SUB_DRAW_GOLDEN_RECTANGLE(ByVal ENM_TYPE As ENM_GOLDEN_RATE_TYPE, ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image)
        Dim INT_DRAW_X As Integer
        Dim INT_DRAW_Y As Integer
        Dim INT_DRAW_W As Integer
        Dim INT_DRAW_H As Integer
        INT_DRAW_X = 0
        INT_DRAW_Y = 0
        INT_DRAW_W = 0
        INT_DRAW_H = 0
        Select Case ENM_TYPE
            Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_LEFT, ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_RIGHT, ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_LEFT, ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_RIGHT
                Call SUB_DRAW_GOLDEN_RATE_FRAME_H(GRP_DRAW, PEN_DRAW, IMG_CANVAS, INT_DRAW_X, INT_DRAW_Y, INT_DRAW_W, INT_DRAW_H)
            Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_LEFT, ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_RIGHT, ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_LEFT, ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_RIGHT
                Call SUB_DRAW_GOLDEN_RATE_FRAME_V(GRP_DRAW, PEN_DRAW, IMG_CANVAS, INT_DRAW_X, INT_DRAW_Y, INT_DRAW_W, INT_DRAW_H)
        End Select

        Dim INT_RATE_X As Integer
        Dim INT_RATE_Y As Integer
        Select Case ENM_TYPE
            Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_LEFT
                INT_RATE_X = 0
                INT_RATE_Y = 1
            Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_RIGHT
                INT_RATE_X = 1
                INT_RATE_Y = 0
            Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_LEFT
                INT_RATE_X = 0
                INT_RATE_Y = 0
            Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_RIGHT
                INT_RATE_X = 1
                INT_RATE_Y = 1
            Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_LEFT
                INT_RATE_X = 0
                INT_RATE_Y = 1
            Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_RIGHT
                INT_RATE_X = 1
                INT_RATE_Y = 0
            Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_LEFT
                INT_RATE_X = 0
                INT_RATE_Y = 0
            Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_RIGHT
                INT_RATE_X = 1
                INT_RATE_Y = 1
        End Select

        Dim INT_NEXT_X As Integer
        INT_NEXT_X = INT_DRAW_X + (INT_DRAW_W * INT_RATE_X)
        Dim INT_NEXT_Y As Integer
        INT_NEXT_Y = INT_DRAW_Y + (INT_DRAW_H * INT_RATE_Y)
        Dim INT_POINT_WH As Integer
        Select Case ENM_TYPE
            Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_LEFT, ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_RIGHT, ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_LEFT, ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_RIGHT
                INT_POINT_WH = INT_DRAW_W
            Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_LEFT, ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_RIGHT, ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_LEFT, ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_RIGHT
                INT_POINT_WH = INT_DRAW_H
        End Select

        For i = 1 To 16
            Dim INT_MOD As Integer
            Dim INT_POINT_X As Integer
            Dim INT_POINT_Y As Integer

            INT_POINT_WH = FUNC_GET_LENGTH_LONG(INT_POINT_WH)

            If INT_POINT_WH <= 10 Then
                Exit For
            End If

            INT_POINT_X = INT_NEXT_X
            INT_POINT_Y = INT_NEXT_Y

            INT_MOD = i Mod 4

            Dim SRT_PARAM As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
            Select Case ENM_TYPE
                Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_LEFT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_HLL_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
                Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_RIGHT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_HUR_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
                Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_UPPER_LEFT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_HUL_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
                Case ENM_GOLDEN_RATE_TYPE.HORIZONTAL_START_LOWER_RIGHT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_HLR_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
                Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_LEFT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_VLL_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
                Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_RIGHT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_VUR_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
                Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_UPPER_LEFT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_VUL_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
                Case ENM_GOLDEN_RATE_TYPE.VERTICAL_START_LOWER_RIGHT
                    SRT_PARAM = FUNC_GET_GOLDEN_RATE_VLR_FRAME_AND_ARC_PARAM(INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_MOD)
            End Select
            With SRT_PARAM
                Call SUB_DRAW_RECTANGLE(GRP_DRAW, PEN_DRAW, INT_POINT_X, INT_POINT_Y, .POINT_W, .POINT_H)
                Call GRP_DRAW.DrawArc(PEN_DRAW, .POINT_ARC_X, .POINT_ARC_Y, INT_POINT_WH * 2, INT_POINT_WH * 2, .ANGLE_START, .ANGLE_SWEEP)

                INT_NEXT_X = INT_POINT_X + .POINT_W
                INT_NEXT_Y = INT_POINT_Y + .POINT_H
            End With
        Next

    End Sub

#Region "水平"
    Private Function FUNC_GET_GOLDEN_RATE_HLL_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 2
            Case 2
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 3
            Case 3
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 0
            Case 0
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 1
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function

    Private Function FUNC_GET_GOLDEN_RATE_HUR_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 0
            Case 2
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 1
            Case 3
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 2
            Case 0
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 3
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function

    Private Function FUNC_GET_GOLDEN_RATE_HUL_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 1
            Case 2
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 0
            Case 3
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 3
            Case 0
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 2
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function

    Private Function FUNC_GET_GOLDEN_RATE_HLR_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 3
            Case 2
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 2
            Case 3
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 1
            Case 0
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 0
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function

#End Region

#Region "垂直"
    Private Function FUNC_GET_GOLDEN_RATE_VLL_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 0
            Case 2
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 3
            Case 3
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 2
            Case 0
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 1
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function

    Private Function FUNC_GET_GOLDEN_RATE_VUR_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 2
            Case 2
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 1
            Case 3
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 0
            Case 0
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 3
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function

    Private Function FUNC_GET_GOLDEN_RATE_VUL_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 3
            Case 2
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 0
            Case 3
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 1
            Case 0
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 2
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function

    Private Function FUNC_GET_GOLDEN_RATE_VLR_FRAME_AND_ARC_PARAM(ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer, ByVal INT_COUNT As Integer) As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Dim SRT_RET As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM
        Call SUB_INIT_GOLDEN_RATE_PARAM(SRT_RET)

        Dim INT_SIGN_W As Integer
        Dim INT_SIGN_H As Integer
        Dim INT_RATE_ARC_X As Integer
        Dim INT_RATE_ARC_Y As Integer
        Dim INT_RATE_ANGLE As Integer
        Select Case INT_COUNT
            Case 1
                INT_SIGN_W = -1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 2
                INT_RATE_ANGLE = 1
            Case 2
                INT_SIGN_W = 1
                INT_SIGN_H = -1
                INT_RATE_ARC_X = 0
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 2
            Case 3
                INT_SIGN_W = 1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 1
                INT_RATE_ARC_Y = 0
                INT_RATE_ANGLE = 3
            Case 0
                INT_SIGN_W = -1
                INT_SIGN_H = 1
                INT_RATE_ARC_X = 2
                INT_RATE_ARC_Y = 1
                INT_RATE_ANGLE = 0
        End Select

        Call SUB_MAKE_GOLDEN_RATE_PARAM(SRT_RET, INT_POINT_X, INT_POINT_Y, INT_POINT_WH, INT_SIGN_W, INT_SIGN_H, INT_RATE_ARC_X, INT_RATE_ARC_Y, INT_RATE_ANGLE)

        Return SRT_RET
    End Function
#End Region

    Private Sub SUB_INIT_GOLDEN_RATE_PARAM(ByRef SRT_DATA As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM)
        With SRT_DATA
            .POINT_W = 0
            .POINT_H = 0
            .POINT_ARC_X = 0
            .POINT_ARC_Y = 0
            .ANGLE_START = 0
            .ANGLE_SWEEP = 0
        End With
    End Sub

    Private Sub SUB_MAKE_GOLDEN_RATE_PARAM(
    ByRef SRT_DATA As SRT_GOLDEN_RATE_FRAME_AND_ARC_PARAM,
    ByVal INT_POINT_X As Integer, ByVal INT_POINT_Y As Integer, ByVal INT_POINT_WH As Integer,
    ByVal INT_SIGN_W As Integer, ByVal INT_SIGN_H As Integer, ByVal INT_RATE_ARC_X As Integer, ByVal INT_RATE_ARC_Y As Integer, ByVal INT_RATE_ANGLE As Integer
    )
        With SRT_DATA
            .POINT_W = INT_SIGN_W * INT_POINT_WH
            .POINT_H = INT_SIGN_H * INT_POINT_WH
            .POINT_ARC_X = INT_POINT_X - (INT_RATE_ARC_X * INT_POINT_WH)
            .POINT_ARC_Y = INT_POINT_Y - (INT_RATE_ARC_Y * INT_POINT_WH)
            .ANGLE_START = INT_RATE_ANGLE * 90
            .ANGLE_SWEEP = 90
        End With
    End Sub

    Private Sub SUB_DRAW_GOLDEN_RATE_FRAME_H(
    ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image,
    ByRef INT_DRAW_X As Integer, ByRef INT_DRAW_Y As Integer, ByRef INT_DRAW_W As Integer, ByRef INT_DRAW_H As Integer
    )
        Dim DEC_ONE_W As Decimal
        Dim DEC_ONE_H As Decimal

        DEC_ONE_W = IMG_CANVAS.Width / CST_GOLDEN_RATE_L
        DEC_ONE_H = IMG_CANVAS.Height / CST_GOLDEN_RATE_S

        If DEC_ONE_W > DEC_ONE_H Then
            Dim DEC_ONE As Decimal
            DEC_ONE = DEC_ONE_H

            Dim INT_WIDTH As Integer
            INT_WIDTH = CInt(DEC_ONE * CST_GOLDEN_RATE_L)

            Dim INT_LEFT_1 As Integer
            INT_LEFT_1 = (IMG_CANVAS.Width - INT_WIDTH) / 2

            Dim INT_LEFT_2 As Integer
            INT_LEFT_2 = INT_LEFT_1 + INT_WIDTH

            INT_DRAW_X = INT_LEFT_1
            INT_DRAW_Y = 0
            INT_DRAW_W = INT_LEFT_2 - INT_LEFT_1
            INT_DRAW_H = CInt(IMG_CANVAS.Height)
        Else
            Dim DEC_ONE As Decimal
            DEC_ONE = DEC_ONE_W

            Dim INT_HEIGHT As Integer
            INT_HEIGHT = (DEC_ONE * CST_GOLDEN_RATE_S)

            Dim INT_TOP_1 As Integer
            INT_TOP_1 = (IMG_CANVAS.Height - INT_HEIGHT) / 2

            Dim INT_TOP_2 As Integer
            INT_TOP_2 = INT_TOP_1 + INT_HEIGHT

            INT_DRAW_X = 0
            INT_DRAW_Y = INT_TOP_1
            INT_DRAW_W = CInt(IMG_CANVAS.Width)
            INT_DRAW_H = INT_TOP_2 - INT_TOP_1
        End If

        Call GRP_DRAW.DrawRectangle(PEN_DRAW, INT_DRAW_X, INT_DRAW_Y, INT_DRAW_W, INT_DRAW_H)
    End Sub

    Private Sub SUB_DRAW_GOLDEN_RATE_FRAME_V(
    ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image,
    ByRef INT_DRAW_X As Integer, ByRef INT_DRAW_Y As Integer, ByRef INT_DRAW_W As Integer, ByRef INT_DRAW_H As Integer
    )
        Dim DEC_ONE_W As Decimal
        Dim DEC_ONE_H As Decimal

        DEC_ONE_W = IMG_CANVAS.Width / CST_GOLDEN_RATE_S
        DEC_ONE_H = IMG_CANVAS.Height / CST_GOLDEN_RATE_L

        If DEC_ONE_W > DEC_ONE_H Then
            Dim DEC_ONE As Decimal
            DEC_ONE = DEC_ONE_H

            Dim INT_WIDTH As Integer
            INT_WIDTH = CInt(DEC_ONE * CST_GOLDEN_RATE_S)

            Dim INT_LEFT_1 As Integer
            INT_LEFT_1 = (IMG_CANVAS.Width - INT_WIDTH) / 2

            Dim INT_LEFT_2 As Integer
            INT_LEFT_2 = INT_LEFT_1 + INT_WIDTH

            INT_DRAW_X = INT_LEFT_1
            INT_DRAW_Y = 0
            INT_DRAW_W = INT_LEFT_2 - INT_LEFT_1
            INT_DRAW_H = CInt(IMG_CANVAS.Height)
        Else
            Dim DEC_ONE As Decimal
            DEC_ONE = DEC_ONE_W

            Dim INT_HEIGHT As Integer
            INT_HEIGHT = (DEC_ONE * CST_GOLDEN_RATE_L)

            Dim INT_TOP_1 As Integer
            INT_TOP_1 = (IMG_CANVAS.Height - INT_HEIGHT) / 2

            Dim INT_TOP_2 As Integer
            INT_TOP_2 = INT_TOP_1 + INT_HEIGHT

            INT_DRAW_X = 0
            INT_DRAW_Y = INT_TOP_1
            INT_DRAW_W = CInt(IMG_CANVAS.Width)
            INT_DRAW_H = INT_TOP_2 - INT_TOP_1
        End If

        Call GRP_DRAW.DrawRectangle(PEN_DRAW, INT_DRAW_X, INT_DRAW_Y, INT_DRAW_W, INT_DRAW_H)
    End Sub

    Private Sub SUB_DRAW_RECTANGLE(ByRef grpDRAW As Graphics, ByRef penDRAW As System.Drawing.Pen, ByVal intX As Integer, ByVal intY As Integer, ByVal intW As Integer, ByVal intH As Integer)
        Dim intX_SET As Integer
        Dim intY_SET As Integer
        Dim intW_SET As Integer
        Dim intH_SET As Integer

        If intW < 0 Then
            intX_SET = intX + intW
            intW_SET = -1 * intW
        Else
            intX_SET = intX
            intW_SET = intW
        End If

        If intH < 0 Then
            intY_SET = intY + intH
            intH_SET = -1 * intH
        Else
            intY_SET = intY
            intH_SET = intH
        End If

        Call grpDRAW.DrawRectangle(penDRAW, intX_SET, intY_SET, intW_SET, intH_SET)

    End Sub

    Private Function FUNC_GET_LENGTH_LONG(ByVal INT_LENGTH As Integer) As Integer
        Dim DEC_ONE As Decimal
        DEC_ONE = (INT_LENGTH / CST_GOLDEN_RATE_L)

        Dim DEC_RET As Decimal
        DEC_RET = DEC_ONE * CST_GOLDEN_RATE_S

        Dim INT_RET As Integer
        INT_RET = CInt(DEC_RET)
        Return INT_RET
    End Function
End Module
