Module MOD_FIN

    Public Sub SUB_APPL_FIN()
        Call SUB_END_GAPHICS()

        Call FUNC_SET_APP_SETTING()
    End Sub

    Private Function FUNC_SET_APP_SETTING() As Boolean
        Dim STR_TEMP As String

        With SRT_APP_SETTINGS_VALUE
            Try
                STR_TEMP = CStr(.PROCESS_NAME)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_PROCESS_NAME, STR_TEMP)

                'SAVE<

                'SAVE.FILE<
                STR_TEMP = CStr(.SAVE.FILE.INDEX)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_INDEX, STR_TEMP)
                '>SAVE.FILE
                '>SAVE
            Catch ex As Exception
                Return False
            End Try
        End With

        Return True
    End Function
End Module
