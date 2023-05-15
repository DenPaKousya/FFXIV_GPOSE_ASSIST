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

    Public Function FUNC_GET_FIXED_PHRASE(ByVal strBASE As String) As String
        Dim strYEAR As String
        strYEAR = String.Format("{0:" & "0000" & "}", datPARM_DATE_BASE.Year)

        Dim strMONTH As String
        strMONTH = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Month)

        Dim strDAY As String
        strDAY = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Day)

        Dim strHOUR As String
        strHOUR = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Hour)

        Dim strMINUTE As String
        strMINUTE = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Minute)

        Dim strSECOND As String
        strSECOND = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Second)

        Dim strINDEX As String
        strINDEX = String.Format("{0:" & "00000" & "}", intPARM_INDEX)

        Dim STR_TEMP As String
        STR_TEMP = strBASE
        STR_TEMP = STR_TEMP.Replace(CST_YEAR, strYEAR)
        STR_TEMP = STR_TEMP.Replace(CST_MONTH, strMONTH)
        STR_TEMP = STR_TEMP.Replace(CST_DAY, strDAY)
        STR_TEMP = STR_TEMP.Replace(CST_HOUR, strHOUR)
        STR_TEMP = STR_TEMP.Replace(CST_MINUTE, strMINUTE)
        STR_TEMP = STR_TEMP.Replace(CST_SECOND, strSECOND)
        STR_TEMP = STR_TEMP.Replace(CST_INDEX, strINDEX)
        STR_TEMP = STR_TEMP.Replace("\", "")

        Dim STR_RET As String
        STR_RET = ""
        STR_RET = STR_TEMP

        Return STR_RET
    End Function

End Module
