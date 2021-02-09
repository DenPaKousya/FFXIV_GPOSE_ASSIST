Module MOD_INIT

    Friend Function FUNC_APPL_INIT(ByRef STR_ERROR_DETAIL As String) As Boolean

        STR_ERROR_DETAIL = ""

        If Not FUNC_GET_APP_CONFIG() Then
            STR_ERROR_DETAIL = "設定ファイルの読込に失敗しました。"
            Return False
        End If

        Call SUB_INIT_GAPHICS()

        If Not FUNC_INIT_EXIF(".\RES\IMG\\SAMPLE.tif") Then
            STR_ERROR_DETAIL = "EXIFの初期化に失敗しました。"
            Return False
        End If
        Return True
    End Function

    Private Function FUNC_GET_APP_CONFIG() As Boolean

        Dim STR_TEMP As String
        STR_TEMP = ""

        Try
            With SRT_APP_SETTINGS_VALUE
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_PROCESS_NAME)
                .PROCESS_NAME = CStr(STR_TEMP)
                If .PROCESS_NAME = "" Then
                    Return False
                End If

                'SAVE<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_SAVE_DIRECTORY)
                .SAVE.DIRECTORY = CStr(STR_TEMP)
                If .SAVE.DIRECTORY = "" Then
                    STR_TEMP = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                    .SAVE.DIRECTORY = STR_TEMP & "\" & .PROCESS_NAME & "_CAPTURE"
                End If

                'SAVE.FILE<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_DIRECTORY)
                .SAVE.FILE.DIRECTORY = CStr(STR_TEMP)

                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_NAME)
                .SAVE.FILE.NAME = CStr(STR_TEMP)

                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_TYPE)
                .SAVE.FILE.TYPE = CStr(STR_TEMP)

                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_QUALITY)
                If Not STR_TEMP = "" Then
                    .SAVE.FILE.QUALITY = CInt(STR_TEMP)
                End If

                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_COPYRIGHT)
                If Not STR_TEMP = "" Then
                    .SAVE.FILE.COPYRIGHT = CInt(STR_TEMP)
                End If

                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_INDEX)
                If Not STR_TEMP = "" Then
                    .SAVE.FILE.INDEX = CInt(STR_TEMP)
                End If
                '>SAVE.FILE
                '>SAVE

                'GUIDE<
                'GUIDE.LOCATION<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_GUIDE_LOCATION_ALIGNMENT)
                .GUIDE.LOCATION.ALIGNMENT = CStr(STR_TEMP)
                '>GUIDE.LOCATION
                '>GUIDE

                'TRIM<
                'TRIM.LOCATION<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_TRIM_LOCATION_LEFT)
                If Not STR_TEMP = "" Then
                    .TRIM.LOCATION.LEFT = CInt(STR_TEMP)
                End If
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_TRIM_LOCATION_TOP)
                If Not STR_TEMP = "" Then
                    .TRIM.LOCATION.TOP = CInt(STR_TEMP)
                End If
                '>TRIM.LOCATION
                'TRIM.SIZE<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_TRIM_SIZE_WIDTH)
                If Not STR_TEMP = "" Then
                    .TRIM.SIZE.WIDTH = CInt(STR_TEMP)
                End If
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_TRIM_SIZE_HEIGHT)
                If Not STR_TEMP = "" Then
                    .TRIM.SIZE.HEIGHT = CInt(STR_TEMP)
                End If
                '>TRIM.SIZE
                'TRIM.ASPECT_RATIO<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_TRIM_ASPECT_RATIO_TYPE)
                If Not STR_TEMP = "" Then
                    .TRIM.ASPECT_RATIO.TYPE = CInt(STR_TEMP)
                End If
                '>TRIM.ASPECT_RATIO
                'TRIM.COMPOTION<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_TRIM_COMPOTION_TYPE)
                If Not STR_TEMP = "" Then
                    .TRIM.COMPOTION.TYPE = CInt(STR_TEMP)
                End If
                '>TRIM.COMPOTION
                '>TRIM

            End With
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
End Module
