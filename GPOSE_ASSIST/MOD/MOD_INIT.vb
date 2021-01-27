Module MOD_INIT

    Friend Function FUNC_APPL_INIT(ByRef STR_ERROR_DETAIL As String) As Boolean

        STR_ERROR_DETAIL = ""

        If Not FUNC_GET_APP_CONFIG() Then
            STR_ERROR_DETAIL = "設定ファイルの読込に失敗しました。"
            Return False
        End If

        Call SUB_INIT_GAPHICS()

        Return True
    End Function

    Private Function FUNC_GET_APP_CONFIG() As Boolean

        Dim STR_TEMP As String
        STR_TEMP = ""

        Try
            STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_PROCESS_NAME)
            STR_APP_CONF_PROCESS_NAME = CStr(STR_TEMP)
            If STR_APP_CONF_PROCESS_NAME = "" Then
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
End Module
