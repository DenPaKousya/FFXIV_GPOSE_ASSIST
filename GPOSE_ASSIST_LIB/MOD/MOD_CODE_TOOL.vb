Public Module MOD_CODE_TOOL

    Public Function FUNC_APPEND_STRING_ARRAY(ByRef STR_ROW() As String, ByVal STR_VALUE As String) As Integer

        Dim INT_INDEX As Integer
        If STR_ROW Is Nothing Then
            INT_INDEX = 0
        Else
            INT_INDEX = STR_ROW.Length
        End If

        ReDim Preserve STR_ROW(INT_INDEX)
        STR_ROW(INT_INDEX) = STR_VALUE
        Return INT_INDEX
    End Function
End Module
