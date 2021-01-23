Module MOD_CONFIG_TOOL

    Public Function FUNC_GET_APP_SETTINGS(ByVal STR_KEY As String) As String
        Dim OBJ_TEMP As Object
        Dim STR_RET As String

        Try
            OBJ_TEMP = System.Configuration.ConfigurationManager.AppSettings(STR_KEY)
        Catch ex As Exception
            Return ""
        End Try

        If OBJ_TEMP Is Nothing Then
            Return ""
        End If

        STR_RET = OBJ_TEMP.ToString

        Return STR_RET
    End Function

End Module
