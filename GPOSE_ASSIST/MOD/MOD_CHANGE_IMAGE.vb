Public Module MOD_CHANGE_IMAGE
    Private STR_DIR_IMAGE As String = ""

    Public Sub SUB_INIT_CHANGE_IMAGE(ByVal STR_SET_DIR As String)
        STR_DIR_IMAGE = STR_SET_DIR
    End Sub
    Public Function FUNC_GET_CHANEG_IMAGE(ByVal STR_NAME_FILE As String) As Bitmap
        Dim STR_PATH As String
        STR_PATH = ""
        STR_PATH &= STR_DIR_IMAGE
        STR_PATH &= "\"
        STR_PATH &= STR_NAME_FILE

        If Not FUNC_FILE_CHECK(STR_PATH) Then
            Return Nothing
        End If

        Dim BMP_RET As Bitmap
        BMP_RET = System.Drawing.Image.FromFile(STR_PATH)
        Return BMP_RET
    End Function
End Module
