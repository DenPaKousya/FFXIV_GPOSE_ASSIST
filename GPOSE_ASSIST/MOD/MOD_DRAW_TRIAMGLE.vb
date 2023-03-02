Public Module MOD_DRAW_TRIAMGLE

    Friend Sub SUB_DRAW_HARMONIOUS_TRIANGLE_HLL(ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image)
        Dim INT_WIDTH As Integer
        INT_WIDTH = IMG_CANVAS.Width

        Dim INT_HIGHT As Integer
        INT_HIGHT = IMG_CANVAS.Height

        Dim X(2) As Integer
        Dim Y(2) As Integer

        '1本目
        X(1) = 0
        Y(1) = INT_HIGHT
        X(2) = CInt(Math.Floor(INT_WIDTH / 2))
        Y(2) = 0
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))

        '2本目
        X(1) = CInt(Math.Floor(INT_WIDTH / 2))
        Y(1) = INT_HIGHT
        X(2) = INT_WIDTH
        Y(2) = 0
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))
    End Sub

    Friend Sub SUB_DRAW_HARMONIOUS_TRIANGLE_HUL(ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image)
        Dim INT_WIDTH As Integer
        INT_WIDTH = IMG_CANVAS.Width

        Dim INT_HIGHT As Integer
        INT_HIGHT = IMG_CANVAS.Height

        Dim X(2) As Integer
        Dim Y(2) As Integer

        '1本目
        X(1) = 0
        Y(1) = 0
        X(2) = CInt(Math.Floor(INT_WIDTH / 2))
        Y(2) = INT_HIGHT
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))

        '2本目
        X(1) = CInt(Math.Floor(INT_WIDTH / 2))
        Y(1) = 0
        X(2) = INT_WIDTH
        Y(2) = INT_HIGHT
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))
    End Sub

    Friend Sub SUB_DRAW_HARMONIOUS_TRIANGLE_VLL(ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image)
        Dim INT_WIDTH As Integer
        INT_WIDTH = IMG_CANVAS.Width

        Dim INT_HIGHT As Integer
        INT_HIGHT = IMG_CANVAS.Height

        Dim X(2) As Integer
        Dim Y(2) As Integer

        '1本目
        X(1) = 0
        Y(1) = INT_HIGHT
        X(2) = INT_WIDTH
        Y(2) = CInt(Math.Floor(INT_HIGHT / 2))
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))

        '2本目
        X(1) = 0
        Y(1) = CInt(Math.Floor(INT_HIGHT / 2))
        X(2) = INT_WIDTH
        Y(2) = 0
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))
    End Sub

    Friend Sub SUB_DRAW_HARMONIOUS_TRIANGLE_VUL(ByRef GRP_DRAW As Graphics, ByRef PEN_DRAW As System.Drawing.Pen, ByRef IMG_CANVAS As System.Windows.Controls.Image)
        Dim INT_WIDTH As Integer
        INT_WIDTH = IMG_CANVAS.Width

        Dim INT_HIGHT As Integer
        INT_HIGHT = IMG_CANVAS.Height

        Dim X(2) As Integer
        Dim Y(2) As Integer

        '1本目
        X(1) = 0
        Y(1) = 0
        X(2) = INT_WIDTH
        Y(2) = CInt(Math.Floor(INT_HIGHT / 2))
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))

        '2本目
        X(1) = 0
        Y(1) = CInt(Math.Floor(INT_HIGHT / 2))
        X(2) = INT_WIDTH
        Y(2) = INT_HIGHT
        Call GRP_DRAW.DrawLine(PEN_DRAW, X(1), Y(1), X(2), Y(2))
    End Sub
End Module
