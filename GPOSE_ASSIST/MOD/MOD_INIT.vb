Module MOD_INIT

    Friend Function FUNC_APPL_INIT(ByRef STR_ERROR_DETAIL As String, ByRef FRM_MAIN As System.Windows.Forms.Form) As Boolean

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

        If FUNC_CAST_INT_TO_BOOL(SRT_APP_SETTINGS_VALUE.NETWORK_ENABLED) Then
            If Not GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_SERVER_START(1234, FRM_MAIN) Then
                STR_ERROR_DETAIL = "ネットワークの待ち受けに失敗しました。"
                Return False
            End If

            If Not GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_SERVER_INIT() Then
                STR_ERROR_DETAIL = "サービススレッドの初期化に失敗しました。"
                Return False
            End If
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

                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_NETWORK_ENABLED)
                .NETWORK_ENABLED = CInt(STR_TEMP)

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

                ReDim .TRIM.COMPOTION.USER(CST_APP_CONFIG_TRIM_COMPOTION_USER_ITEM_COUNT)
                For i = 1 To (.TRIM.COMPOTION.USER.Length - 1) 'USER SET
                    ReDim .TRIM.COMPOTION.USER(i).TYPE(4)
                    STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_TRIM_COMPOTION_USER & "." & CStr(i))
                    If Not STR_TEMP = "" Then
                        .TRIM.COMPOTION.USER(i).BASE = CStr(STR_TEMP)
                    End If
                    Dim STR_ITEM() As String
                    STR_ITEM = .TRIM.COMPOTION.USER(i).BASE.Split(",")
                    .TRIM.COMPOTION.USER(i).NAME = STR_ITEM(0)
                    For j = 1 To (.TRIM.COMPOTION.USER(i).TYPE.Length - 1)
                        .TRIM.COMPOTION.USER(i).TYPE(j) = CInt(STR_ITEM(j))
                    Next
                Next
                '>TRIM.COMPOTION
                '>TRIM

                'CAMERA<
                'CAMERA.CONTROL<
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_CAMERA_CONTROL_WAIT_FOR_GAME_RESPONSE)
                .CAMERA.CONTROL.WAIT_FOR_GAME_RESPONSE = CInt(STR_TEMP)
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_CAMERA_CONTROL_WASD_PUSH_WEIGHT)
                .CAMERA.CONTROL.WASD_PUSH_WEIGHT = CInt(STR_TEMP)
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_CAMERA_CONTROL_ARROW_PUSH_WEIGHT)
                .CAMERA.CONTROL.ARROW_PUSH_WEIGHT = CInt(STR_TEMP)
                STR_TEMP = FUNC_GET_APP_SETTINGS(CST_APP_CONFIG_CAMERA_CONTROL_PAGEUD_PUSH_WEIGHT)
                .CAMERA.CONTROL.PAGEUD_PUSH_WEIGHT = CInt(STR_TEMP)
                '>CAMERA.CONTROL
                '>CAMERA

            End With
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
End Module
