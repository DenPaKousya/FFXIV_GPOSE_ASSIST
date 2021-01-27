Module MOD_CONFIG_TOOL

#Region "外部用・構造体"
    Public Structure SRT_APP_SETTINGS
        Public PROCESS_NAME As String
        Public SAVE As SRT_APP_SETTINGS_SAVE
    End Structure

    Public Structure SRT_APP_SETTINGS_SAVE
        Public DIRECTORY As String
        Public FILE As SRT_APP_SETTINGS_SAVE_FILE
    End Structure

    Public Structure SRT_APP_SETTINGS_SAVE_FILE
        Public DIRECTORY As String
        Public NAME As String
        Public TYPE As String
        Public QUALITY As Integer
        Public COPYRIGHT As Integer
        Public INDEX As Integer
    End Structure
#End Region

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
