Module MOD_FIN

    Public Sub SUB_APPL_FIN()
        Call SUB_END_GAPHICS()

        Call FUNC_SET_APP_SETTING()

        Call GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_SERVER_FIN(True)
        Call GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_SERVER_STOP()
    End Sub

    Private Function FUNC_SET_APP_SETTING() As Boolean
        Dim STR_TEMP As String

        With SRT_APP_SETTINGS_VALUE
            Try
                STR_TEMP = CStr(.PROCESS_NAME)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_PROCESS_NAME, STR_TEMP)

                'SAVE<
                STR_TEMP = CStr(.SAVE.DIRECTORY)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_SAVE_DIRECTORY, STR_TEMP)
                'SAVE.FILE<
                STR_TEMP = CStr(.SAVE.FILE.NAME)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_NAME, STR_TEMP)
                STR_TEMP = CStr(.SAVE.FILE.TYPE)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_TYPE, STR_TEMP)
                STR_TEMP = CStr(.SAVE.FILE.QUALITY)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_QUALITY, STR_TEMP)
                STR_TEMP = CStr(.SAVE.FILE.COPYRIGHT)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_COPYRIGHT, STR_TEMP)
                STR_TEMP = CStr(.SAVE.FILE.INDEX)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_SAVE_FILE_INDEX, STR_TEMP)
                '>SAVE.FILE
                '>SAVE

                'GUIDE<
                'GUIDE.LOCATION<
                STR_TEMP = CStr(.GUIDE.LOCATION.ALIGNMENT)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_GUIDE_LOCATION_ALIGNMENT, STR_TEMP)
                '>GUIDE.LOCATION
                '>GUIDE

                'TRIM<
                'TRIM.LOCATION<
                STR_TEMP = CStr(.TRIM.LOCATION.LEFT)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_TRIM_LOCATION_LEFT, STR_TEMP)
                STR_TEMP = CStr(.TRIM.LOCATION.TOP)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_TRIM_LOCATION_TOP, STR_TEMP)
                '>TRIM.LOCATION
                'TRIM.SIZE<
                STR_TEMP = CStr(.TRIM.SIZE.WIDTH)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_TRIM_SIZE_WIDTH, STR_TEMP)
                STR_TEMP = CStr(.TRIM.SIZE.HEIGHT)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_TRIM_SIZE_HEIGHT, STR_TEMP)
                '>TRIM.SIZE
                'TRIM.ASPECT_RATIO<
                STR_TEMP = CStr(.TRIM.ASPECT_RATIO.TYPE)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_TRIM_ASPECT_RATIO_TYPE, STR_TEMP)
                '>TRIM.ASPECT_RATIO
                'TRIM.COMPOTION<
                STR_TEMP = CStr(.TRIM.COMPOTION.TYPE)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_TRIM_COMPOTION_TYPE, STR_TEMP)
                For i = 1 To (.TRIM.COMPOTION.USER.Length - 1)
                    STR_TEMP = CStr(.TRIM.COMPOTION.USER(i).BASE)
                    Dim STR_KEY As String
                    STR_KEY = CST_APP_CONFIG_TRIM_COMPOTION_USER & "." & CStr(i)
                    Call FUNC_WRITE_APP_SETTINGS(STR_KEY, STR_TEMP)
                Next
                '>TRIM.COMPOTION
                '>TRIM

                'CAMERA<
                'CAMERA.CONTROL<
                STR_TEMP = CStr(.CAMERA.CONTROL.WASD_PUSH_WEIGHT)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_CAMERA_CONTROL_WASD_PUSH_WEIGHT, STR_TEMP)
                STR_TEMP = CStr(.CAMERA.CONTROL.ARROW_PUSH_WEIGHT)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_CAMERA_CONTROL_ARROW_PUSH_WEIGHT, STR_TEMP)
                STR_TEMP = CStr(.CAMERA.CONTROL.PAGEUD_PUSH_WEIGHT)
                Call FUNC_WRITE_APP_SETTINGS(CST_APP_CONFIG_CAMERA_CONTROL_PAGEUD_PUSH_WEIGHT, STR_TEMP)
                '>CAMERA.CONTROL
                '>CAMERA
            Catch ex As Exception
                Return False
            End Try
        End With

        Return True
    End Function
End Module
