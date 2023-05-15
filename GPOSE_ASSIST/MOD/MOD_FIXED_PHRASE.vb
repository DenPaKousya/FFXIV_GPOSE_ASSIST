Public Module MOD_FIXED_PHRASE

#Region "モジュール用・定数"
    Private Const CST_YEAR As String = "\YYYY"
    Private Const CST_MONTH As String = "\MM"
    Private Const CST_DAY As String = "\DD"
    Private Const CST_HOUR As String = "\HH"
    Private Const CST_MINUTE As String = "\mm"
    Private Const CST_SECOND As String = "\SS"
    Private Const CST_INDEX As String = "\INDEX"
#End Region

#Region "モジュール用・変数"
    Private datPARM_DATE_BASE As DateTime
    Private intPARM_INDEX As Integer
#End Region

    Public Sub SUB_FIXED_PHRASE_INIT(ByVal DAT_DATE_BASE As DateTime, ByVal INT_INDEX As Integer)
        datPARM_DATE_BASE = DAT_DATE_BASE
        intPARM_INDEX = INT_INDEX
    End Sub

    Public Function FUNC_GET_FIXED_PHRASE(ByVal STR_BASE As String) As String
        Dim STR_YEAR As String
        STR_YEAR = String.Format("{0:" & "0000" & "}", datPARM_DATE_BASE.Year)

        Dim STR_MONTH As String
        STR_MONTH = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Month)

        Dim STR_DAY As String
        STR_DAY = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Day)

        Dim STR_HOUR As String
        STR_HOUR = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Hour)

        Dim STR_MINUTE As String
        STR_MINUTE = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Minute)

        Dim STR_SECOND As String
        STR_SECOND = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Second)

        Dim STR_INDEX As String
        STR_INDEX = String.Format("{0:" & "00000" & "}", intPARM_INDEX)

        Dim STR_TEMP As String
        STR_TEMP = STR_BASE
        STR_TEMP = STR_TEMP.Replace(CST_YEAR, STR_YEAR)
        STR_TEMP = STR_TEMP.Replace(CST_MONTH, STR_MONTH)
        STR_TEMP = STR_TEMP.Replace(CST_DAY, STR_DAY)
        STR_TEMP = STR_TEMP.Replace(CST_HOUR, STR_HOUR)
        STR_TEMP = STR_TEMP.Replace(CST_MINUTE, STR_MINUTE)
        STR_TEMP = STR_TEMP.Replace(CST_SECOND, STR_SECOND)
        STR_TEMP = STR_TEMP.Replace(CST_INDEX, STR_INDEX)
        STR_TEMP = STR_TEMP.Replace("\", "")

        Dim STR_RET As String
        STR_RET = ""
        STR_RET = STR_TEMP

        Return STR_RET
    End Function

End Module
