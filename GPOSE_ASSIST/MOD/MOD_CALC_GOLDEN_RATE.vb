Public Module MOD_CALC_GOLDEN_RATE

#Region "モジュール内定数"
    Private Const CST_GOLDEN_RATE_LONG_SIDE As Integer = 1000 '長辺
    Private Const CST_GOLDEN_RATE_SHORT_SIDE As Integer = 618 '短辺

    Private Const CST_GOLDEN_RATE_BASE_DIVISION = 1000 '除算数
#End Region

    Private Function FUNC_GET_GONLDEN_RATE_BASE_POINT(ByVal INT_POINT As Integer) As Decimal
        Dim DEC_TEMP As Decimal
        DEC_TEMP = CDec(INT_POINT)

        Dim DEC_BASE As Decimal
        DEC_BASE = CDec(CST_GOLDEN_RATE_LONG_SIDE + CST_GOLDEN_RATE_SHORT_SIDE)

        Dim DEC_RET As Decimal
        DEC_RET = (DEC_TEMP / DEC_BASE)

        Return DEC_RET
    End Function

    Public Function FUNC_GET_GOLDEN_RATE_LONG_SIDE(ByVal INT_POINT As Integer) As Integer
        Dim DEC_TEMP As Decimal
        DEC_TEMP = FUNC_GET_GONLDEN_RATE_BASE_POINT(INT_POINT)

        Dim DEC_RET As Decimal
        DEC_RET = CDec(DEC_TEMP * CDec(CST_GOLDEN_RATE_LONG_SIDE))

        Dim INT_RET As Integer
        INT_RET = Math.Floor(DEC_RET)

        Return INT_RET
    End Function

    Public Function FUNC_GET_GOLDEN_RATE_SHORT_SIDE(ByVal INT_POINT As Integer) As Integer
        Dim DEC_TEMP As Decimal
        DEC_TEMP = FUNC_GET_GONLDEN_RATE_BASE_POINT(INT_POINT)

        Dim DEC_RET As Decimal
        DEC_RET = CDec(DEC_TEMP * CDec(CST_GOLDEN_RATE_SHORT_SIDE))

        Dim INT_RET As Integer
        INT_RET = Math.Floor(DEC_RET)

        Return INT_RET
    End Function

End Module
