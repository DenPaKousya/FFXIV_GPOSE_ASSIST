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
        Dim strTEMP As String
        Dim strRET As String

        Dim strYEAR As String
        Dim strMONTH As String
        Dim strDAY As String
        Dim strHOUR As String
        Dim strMINUTE As String
        Dim strSECOND As String
        Dim strINDEX As String

        strRET = ""

        strYEAR = String.Format("{0:" & "0000" & "}", datPARM_DATE_BASE.Year)
        strMONTH = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Month)
        strDAY = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Day)
        strHOUR = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Hour)
        strMINUTE = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Minute)
        strSECOND = String.Format("{0:" & "00" & "}", datPARM_DATE_BASE.Second)
        strINDEX = String.Format("{0:" & "00000" & "}", intPARM_INDEX)

        strTEMP = strBASE
        strTEMP = strTEMP.Replace(CST_YEAR, strYEAR)
        strTEMP = strTEMP.Replace(CST_MONTH, strMONTH)
        strTEMP = strTEMP.Replace(CST_DAY, strDAY)
        strTEMP = strTEMP.Replace(CST_HOUR, strHOUR)
        strTEMP = strTEMP.Replace(CST_MINUTE, strMINUTE)
        strTEMP = strTEMP.Replace(CST_SECOND, strSECOND)
        strTEMP = strTEMP.Replace(CST_INDEX, strINDEX)

        strTEMP = strTEMP.Replace("\", "")

        strRET = strTEMP

        Return strRET
    End Function

End Module
