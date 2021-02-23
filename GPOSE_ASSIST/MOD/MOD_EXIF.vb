Public Module MOD_EXIF

#Region "モジュール用・列挙定数"
    Public Enum ENM_EXIF_ID
        Make = 271
        Model = 272
        Software = 305
        ModifyDate = 306
        ISOSpeedRatings = 34855
        DateTimeOriginal = 36867
    End Enum
#End Region

#Region "外部用・構造体"
    Public Structure SRT_EXIF_SET
        Public MAKE As String
        Public MODEL As String
        Public SOFTWARE As String
        Public MODIFY_DATE As String
        Public DATE_TIME_ORIGINAL As String
        Public ISO_SPEED_RATINGS As Integer
    End Structure
#End Region

#Region "モジュール用・変数"
    Private bmpSAMPLE_IMAGE As Bitmap
    Private itmSAMPLE_PITEM As System.Drawing.Imaging.PropertyItem
#End Region

    Public Function FUNC_INIT_EXIF(ByVal STR_PATH_SAMPLE As String) As Boolean
        Try
            bmpSAMPLE_IMAGE = New System.Drawing.Bitmap(STR_PATH_SAMPLE)
            itmSAMPLE_PITEM = bmpSAMPLE_IMAGE.PropertyItems(0)
        Catch ex As Exception
            Return False
        End Try

        Dim intLOOP_INDEX As Integer
        For intLOOP_INDEX = 0 To (bmpSAMPLE_IMAGE.PropertyItems.Length - 1)
            With bmpSAMPLE_IMAGE.PropertyItems(intLOOP_INDEX)
                If .Type = 2 Then
                    Dim bytTEST() As Byte
                    Dim val As String
                    bytTEST = .Value

                    val = System.Text.Encoding.ASCII.GetString(bytTEST)

                    Console.WriteLine(val & " " & .Id)
                End If
            End With
        Next

        Return True
    End Function

    Public Function FUNC_EXIF_INT_DATETIME(ByVal datDATE_TIME As DateTime) As Integer
        Dim intRET As Integer
        Dim intYEAR As Integer
        Dim intMONTH As Integer
        Dim intYEAR_2L As Integer

        intYEAR = datDATE_TIME.Year
        intMONTH = datDATE_TIME.Month

        intYEAR_2L = CInt(intYEAR.ToString.Substring(2, 2))
        intRET = intYEAR_2L * 100 + intMONTH
        Return intRET
    End Function

    Public Function FUNC_EXIF_STRING_DATETIME(ByVal datDATE_TIME As DateTime) As String
        Dim strRET As String
        Dim intYEAR As Integer
        Dim intMONTH As Integer
        Dim intDAY As Integer
        Dim strDATE As String
        Dim intHOUR As Integer
        Dim intMINUTE As Integer
        Dim intSECOND As Integer
        Dim strTIME As String

        intYEAR = datDATE_TIME.Year
        intMONTH = datDATE_TIME.Month
        intDAY = datDATE_TIME.Day

        strDATE = intYEAR.ToString.PadLeft(4, "0") & ":" & intMONTH.ToString.PadLeft(2, "0") & ":" & intDAY.ToString.PadLeft(2, "0")

        intHOUR = datDATE_TIME.Hour
        intMINUTE = datDATE_TIME.Minute
        intSECOND = datDATE_TIME.Second

        strTIME = intHOUR.ToString.PadLeft(2, "0") & ":" & intMINUTE.ToString.PadLeft(2, "0") & ":" & intSECOND.ToString.PadLeft(2, "0")

        strRET = strDATE & " " & strTIME

        Return strRET
    End Function

    Public Sub SUB_SET_EXIF_TO_IMAGE(ByRef BMP_IMAGE As Bitmap, ByRef SRT_SET As SRT_EXIF_SET)
        Call FUNC_ADD_EXIF_STRING(BMP_IMAGE, ENM_EXIF_ID.Make, SRT_SET.MAKE)
        Call FUNC_ADD_EXIF_STRING(BMP_IMAGE, ENM_EXIF_ID.Model, SRT_SET.MODEL)
        Call FUNC_ADD_EXIF_STRING(BMP_IMAGE, ENM_EXIF_ID.Software, SRT_SET.SOFTWARE)
        Call FUNC_ADD_EXIF_STRING(BMP_IMAGE, ENM_EXIF_ID.ModifyDate, SRT_SET.MODIFY_DATE)
        Call FUNC_ADD_EXIF_STRING(BMP_IMAGE, ENM_EXIF_ID.DateTimeOriginal, SRT_SET.DATE_TIME_ORIGINAL)

        Call FUNC_ADD_EXIF_INT16(BMP_IMAGE, ENM_EXIF_ID.ISOSpeedRatings, SRT_SET.ISO_SPEED_RATINGS)
    End Sub

    Public Function FUNC_ADD_EXIF_STRING(ByRef bmpIMAGE As Bitmap, ByVal enmID As ENM_EXIF_ID, ByVal strVALUE As String) As Boolean
        Dim itmSET As System.Drawing.Imaging.PropertyItem
        Dim strSET As String

        strSET = strVALUE & Convert.ToChar(0)
        itmSET = itmSAMPLE_PITEM
        With itmSET
            .Id = CInt(enmID)
            .Type = 2
            .Value = System.Text.Encoding.ASCII.GetBytes(strSET)
            .Len = .Value.Length
        End With

        Call bmpIMAGE.SetPropertyItem(itmSET) '格納する

        Return True
    End Function

    Public Function FUNC_ADD_EXIF_INT16(ByRef bmpIMAGE As Bitmap, ByVal enmID As ENM_EXIF_ID, ByVal intVALUE As Integer) As Boolean
        Dim itmSET As System.Drawing.Imaging.PropertyItem
        itmSET = itmSAMPLE_PITEM
        With itmSET
            .Id = CInt(enmID)
            .Type = 3
            .Value = BitConverter.GetBytes(intVALUE)
            .Len = .Value.Length
        End With

        Call bmpIMAGE.SetPropertyItem(itmSET) '格納する

        Return True
    End Function

    Private Function FUNC_EDIT_EXIF(ByRef bmpIMAGE As Bitmap) As Boolean
        Dim intLOOP_INDEX As Integer

        If bmpIMAGE.PropertyItems Is Nothing OrElse bmpIMAGE.PropertyItems.Length = 0 Then
            Return False
        End If

        For intLOOP_INDEX = 0 To (bmpIMAGE.PropertyItems.Length - 1)
            Dim itmPITEM As System.Drawing.Imaging.PropertyItem = bmpIMAGE.PropertyItems(intLOOP_INDEX)

            With itmPITEM
                If .Id = &H13B And .Type = 2 Then
                    ''値を変更する
                    '.Value = System.Text.Encoding.ASCII.GetBytes(
                    '    artist + ControlChars.NullChar)
                    'pi.Len = pi.Value.Length
                    ''設定する
                    'bmp.SetPropertyItem(pi)
                    Return True
                End If
            End With
        Next

        Return False
    End Function

    Public Function FUNC_GET_EXIF_DEFAULT(ByVal STR_SOFTWARE As String, ByVal DAT_DATE_SAVE As DateTime) As SRT_EXIF_SET
        Dim SRT_RET As SRT_EXIF_SET
        With SRT_RET
            .SOFTWARE = STR_SOFTWARE
            .MAKE = FUNC_GET_MAKE_DEFAULT(.SOFTWARE)
            .MODEL = FUNC_GET_MODEL_DEFAULT(.SOFTWARE)
            .MODIFY_DATE = FUNC_EXIF_STRING_DATETIME(DAT_DATE_SAVE)
            .DATE_TIME_ORIGINAL = .MODIFY_DATE
            .ISO_SPEED_RATINGS = FUNC_EXIF_INT_DATETIME(DAT_DATE_SAVE)
        End With

        Return SRT_RET
    End Function

#Region "内部処理"
    Private Function FUNC_GET_MAKE_DEFAULT(ByVal STR_SOFTWARE As String) As String
        Dim STR_RET As String

        Select Case STR_SOFTWARE
            Case "ffxiv_dx11"
                STR_RET = "SQUARE ENIX"
            Case "chrome"
                STR_RET = "Google"
            Case "notepad"
                STR_RET = "Microsoft"
            Case "FlowScape"
                STR_RET = "Pixel Forest Games"
            Case "GenshinImpact"
                STR_RET = "miHoYo"
            Case Else
                STR_RET = "Unknown"
        End Select

        Return STR_RET
    End Function

    Private Function FUNC_GET_MODEL_DEFAULT(ByVal STR_SOFTWARE As String) As String
        Dim STR_RET As String

        STR_RET = FUNC_GET_APPLICATION_NAME(STR_SOFTWARE)

        Return STR_RET
    End Function
#End Region

End Module
