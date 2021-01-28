Public Module MOD_CAPTURE

#Region "外部用・列挙定数"
    Public Enum ENM_IMAGE_TYPE
        NONE = 0
        PNG
        TIFF
        JPEG
    End Enum
#End Region

#Region "外部用・構造体"
    Public Structure SRT_SAVE_IMAGE_PARAM
        Public SAVE_DIR As String
        Public SAVE_NAME_FILE As String
        Public TYPE As ENM_IMAGE_TYPE
        Public QUALITY As Integer
        Public ADD_CL As Boolean
        Public EXIF As SRT_EXIF_SET

        Public SAVE_NAME_FILE_REAL As String
    End Structure
#End Region

#Region "モジュール用・定数"
    Private Const CST_FILE_EXETENT_PNG As String = ".png"
    Private Const CST_FILE_EXETENT_JPEG As String = ".jpg"
    Private Const CST_FILE_EXETENT_TIFF As String = ".tif"
#End Region

    Public Sub SUB_PRINT_CLIENT_WINDOW(ByRef PRC_TARGET As Process, ByRef GRP_PROCESS_CLIENT As System.Drawing.Graphics, ByRef BMP_PROCESS_CLIENT As System.Drawing.Bitmap)
        Dim SRT_RECT_CLIENT_WH As RECT_WH
        SRT_RECT_CLIENT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_TARGET)

        Dim PFT_BITMAP As System.Drawing.Imaging.PixelFormat
        PFT_BITMAP = System.Drawing.Imaging.PixelFormat.Format24bppRgb '24bit指定

        BMP_PROCESS_CLIENT = New System.Drawing.Bitmap(SRT_RECT_CLIENT_WH.width, SRT_RECT_CLIENT_WH.height, PFT_BITMAP)
        GRP_PROCESS_CLIENT = System.Drawing.Graphics.FromImage(BMP_PROCESS_CLIENT)

        Call SUB_PRINT_WINDOW_TEST(GRP_PROCESS_CLIENT, PRC_APP_TARGET)
    End Sub

    Public Function FUNC_MAKE_BITMAP_FROM_PRINT(ByRef BMP_PROCESS_CLIENT As System.Drawing.Bitmap, ByVal SRT_TRIM As RECT_WH, ByVal ENM_ROTATE_FLIP_TYPE As RotateFlipType) As System.Drawing.Bitmap

        Dim BLN_TRIM As Boolean
        If SRT_TRIM.left = 0 _
        And SRT_TRIM.top = 0 _
        And BMP_PROCESS_CLIENT.Width = SRT_TRIM.width _
        And BMP_PROCESS_CLIENT.Height = SRT_TRIM.height _
        Then '起点(0,0)でサイズが同じなら
            BLN_TRIM = False 'トリミングしない
        Else
            BLN_TRIM = True 'トリミングする
        End If

        Dim BMP_RET As System.Drawing.Bitmap
        If BLN_TRIM Then
            Dim BMP_TRIM As Bitmap
            BMP_TRIM = New Bitmap(SRT_TRIM.width, SRT_TRIM.height)

            Dim RCT_SRC As System.Drawing.Rectangle
            RCT_SRC = New System.Drawing.Rectangle(SRT_TRIM.left, SRT_TRIM.top, SRT_TRIM.width, SRT_TRIM.height)

            Dim RCT_DEST As System.Drawing.Rectangle
            RCT_DEST = New System.Drawing.Rectangle(0, 0, RCT_SRC.Width, RCT_SRC.Height)

            Dim GRP_TRIM As Graphics = Graphics.FromImage(BMP_TRIM)
            Call GRP_TRIM.DrawImage(BMP_PROCESS_CLIENT, RCT_DEST, RCT_SRC, GraphicsUnit.Pixel)
            Call GRP_TRIM.Dispose()
            BMP_RET = BMP_TRIM.Clone
        Else
            BMP_RET = BMP_PROCESS_CLIENT.Clone
        End If

        If Not (ENM_ROTATE_FLIP_TYPE = RotateFlipType.RotateNoneFlipNone) Then
            Call BMP_RET.RotateFlip(ENM_ROTATE_FLIP_TYPE)
        End If

        Return BMP_RET
    End Function

    Public Function FUNC_SAVE_IMAGE(ByRef BMP_SAVE As System.Drawing.Bitmap, ByVal SRT_PARAM As SRT_SAVE_IMAGE_PARAM) As Boolean

        If BMP_SAVE Is Nothing Then
            Return False
        End If

        If SRT_PARAM.TYPE = ENM_IMAGE_TYPE.NONE Then
            Return True
        End If

        If Not FUNC_DIR_MAKE(SRT_PARAM.SAVE_DIR) Then
            Return False
        End If

        With SRT_PARAM
            'ファイル名の確定（連番付与）ココカラ
            Dim STR_NAME_FILE As String
            STR_NAME_FILE = .SAVE_NAME_FILE_REAL
            'ファイル名の確定（連番付与）ココマデ

            '拡張子付与ココカラ
            Dim STR_EXETENT As String
            STR_EXETENT = FUNC_GET_EXTENT_IMAGE(.TYPE)
            Dim STR_PATH As String
            STR_PATH = .SAVE_DIR & "\" & STR_NAME_FILE & STR_EXETENT
            '拡張子付与ココマデ

            '形式ココカラ
            Dim IFT_SAVE As System.Drawing.Imaging.ImageFormat
            IFT_SAVE = FUNC_GET_IMAGE_FORMAT_IMAGE(.TYPE)
            Dim ICI_SAVE As System.Drawing.Imaging.ImageCodecInfo
            ICI_SAVE = FUNC_GET_ENCODER_INFO(IFT_SAVE)
            '形式ココマデ

            'エンコードココカラ
            Dim ENM_SAVE As System.Drawing.Imaging.Encoder
            ENM_SAVE = System.Drawing.Imaging.Encoder.Quality
            Dim EPR_ONE As System.Drawing.Imaging.EncoderParameter
            EPR_ONE = New System.Drawing.Imaging.EncoderParameter(ENM_SAVE, CLng(.QUALITY))
            Dim EPS_SAVE As System.Drawing.Imaging.EncoderParameters
            EPS_SAVE = New System.Drawing.Imaging.EncoderParameters(1)
            EPS_SAVE.Param(0) = EPR_ONE
            'エンコードココマデ

            'コピーライトココカラ
            If .ADD_CL Then
                Call SUB_ADD_COPYRIGHT(BMP_SAVE)
            End If
            'コピーライトココマデ

            'EXIFココカラ
            Dim SRT_EXIF As SRT_EXIF_SET
            SRT_EXIF = .EXIF
            Call SUB_SET_EXIF_TO_IMAGE(BMP_SAVE, SRT_EXIF)
            'EXIFココマデ

            Try
                Call BMP_SAVE.Save(STR_PATH, ICI_SAVE, EPS_SAVE)
            Catch ex As Exception
                Return False
            End Try

        End With

        Return True

    End Function

    Public Function FUNC_GET_TYPE_IMAGE01_FROM_STRING(ByVal STR_CPTURE_TYPE As String) As ENM_IMAGE_TYPE
        Dim ENM_RET As ENM_IMAGE_TYPE
        Select Case STR_CPTURE_TYPE
            Case "PNG"
                ENM_RET = ENM_IMAGE_TYPE.PNG
            Case "JPEG"
                ENM_RET = ENM_IMAGE_TYPE.JPEG
            Case "TIFF"
                ENM_RET = ENM_IMAGE_TYPE.TIFF
            Case "PNG+JPEG"
                ENM_RET = ENM_IMAGE_TYPE.PNG
            Case "TIFF+JPEG"
                ENM_RET = ENM_IMAGE_TYPE.TIFF
            Case Else
                ENM_RET = ENM_IMAGE_TYPE.PNG
        End Select

        Return ENM_RET
    End Function

    Public Function FUNC_GET_TYPE_IMAGE02_FROM_STRING(ByVal STR_CPTURE_TYPE As String) As ENM_IMAGE_TYPE
        Dim ENM_RET As ENM_IMAGE_TYPE
        Select Case STR_CPTURE_TYPE
            Case "PNG+JPEG"
                ENM_RET = ENM_IMAGE_TYPE.JPEG
            Case "TIFF+JPEG"
                ENM_RET = ENM_IMAGE_TYPE.JPEG
            Case Else
                ENM_RET = ENM_IMAGE_TYPE.NONE
        End Select

        Return ENM_RET
    End Function

    Public Function FUNC_GET_NAME_FILE_REAL(ByVal STR_NAME_DIR As String, ByVal STR_NAME_FILE As String) As String
        Dim STR_EXTENT_ROW() As String
        STR_EXTENT_ROW = FUNC_GET_EXTENT_ROW_IMAGE_ALL()
        STR_NAME_FILE = FUNC_GET_FILE_NAME_SUB(STR_NAME_DIR, STR_NAME_FILE, STR_EXTENT_ROW)
        Return STR_NAME_FILE
    End Function

    Public Function FUNC_GET_NAME_SAVE_DIR(ByVal STR_DIR_BASE As String, ByVal STR_FIXED_PHRASE As String) As String
        Dim STR_DIR_SAVE As String
        STR_DIR_SAVE = STR_DIR_BASE
        If Not FUNC_DIR_MAKE(STR_DIR_SAVE) Then
            Return ""
        End If

        Dim STR_DIR As String
        STR_DIR = STR_DIR_SAVE & "\" & FUNC_GET_FOLDER_NAME_CAPTURE(STR_FIXED_PHRASE)

        Return STR_DIR
    End Function

    Public Function FUNC_GET_NAME_SAVE_FILE(ByVal STR_FIXED_PHRASE As String) As String
        Dim STR_FILE_NAME As String
        STR_FILE_NAME = FUNC_GET_FILE_NAME_CAPTURE(STR_FIXED_PHRASE)

        Return STR_FILE_NAME
    End Function

#Region "内部処理"

    Private Function FUNC_GET_EXTENT_ROW_IMAGE_ALL() As String()
        Dim STR_RET() As String

        ReDim STR_RET(0)

        Dim INT_INDEX As Integer

        INT_INDEX = STR_RET.Length
        ReDim Preserve STR_RET(INT_INDEX)
        STR_RET(INT_INDEX) = CST_FILE_EXETENT_PNG

        INT_INDEX = STR_RET.Length
        ReDim Preserve STR_RET(INT_INDEX)
        STR_RET(INT_INDEX) = CST_FILE_EXETENT_JPEG

        INT_INDEX = STR_RET.Length
        ReDim Preserve STR_RET(INT_INDEX)
        STR_RET(INT_INDEX) = CST_FILE_EXETENT_TIFF

        Return STR_RET
    End Function

    Private Function FUNC_GET_IMAGE_FORMAT_IMAGE(ByVal ENM_TYPE As ENM_IMAGE_TYPE) As System.Drawing.Imaging.ImageFormat
        Dim IFT_RET As System.Drawing.Imaging.ImageFormat
        Select Case ENM_TYPE
            Case ENM_IMAGE_TYPE.PNG
                IFT_RET = System.Drawing.Imaging.ImageFormat.Png
            Case ENM_IMAGE_TYPE.TIFF
                IFT_RET = System.Drawing.Imaging.ImageFormat.Tiff
            Case ENM_IMAGE_TYPE.JPEG
                IFT_RET = System.Drawing.Imaging.ImageFormat.Jpeg
            Case Else
                IFT_RET = System.Drawing.Imaging.ImageFormat.Png
        End Select

        Return IFT_RET
    End Function

    Private Function FUNC_GET_EXTENT_IMAGE(ByVal ENM_TYPE As ENM_IMAGE_TYPE) As String
        Dim STR_RET As String

        Select Case ENM_TYPE
            Case ENM_IMAGE_TYPE.PNG
                STR_RET = CST_FILE_EXETENT_PNG
            Case ENM_IMAGE_TYPE.TIFF
                STR_RET = CST_FILE_EXETENT_TIFF
            Case ENM_IMAGE_TYPE.JPEG
                STR_RET = CST_FILE_EXETENT_JPEG
            Case Else
                STR_RET = CST_FILE_EXETENT_PNG
        End Select

        Return STR_RET
    End Function

    Private Function FUNC_GET_FOLDER_NAME_CAPTURE(ByVal STR_FIXED_PHRASE As String) As String
        Dim strRET As String

        strRET = FUNC_GET_FIXED_PHRASE(STR_FIXED_PHRASE)

        Return strRET
    End Function

    Private Function FUNC_GET_FILE_NAME_CAPTURE(ByVal STR_FIXED_PHRASE As String) As String
        Dim strRET As String

        strRET = FUNC_GET_FIXED_PHRASE(STR_FIXED_PHRASE)

        Return strRET
    End Function

    Private Sub SUB_ADD_COPYRIGHT(ByRef BMP_BASE As Bitmap)
        Dim INT_WH_LONG As Integer
        If BMP_BASE.Height > BMP_BASE.Width Then
            INT_WH_LONG = BMP_BASE.Height
        Else
            INT_WH_LONG = BMP_BASE.Width
        End If

        Dim BLN_LARGE As Boolean
        BLN_LARGE = (INT_WH_LONG > 1920)

        Dim STR_EXE As String
        STR_EXE = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim STR_DIR As String
        STR_DIR = FUNC_PATH_TO_DIR_PATH(STR_EXE)
        Call SUB_INIT_CHANGE_IMAGE(STR_DIR & "\RES\IMG\COPYRIGHT")

        Dim BMP_CR As Bitmap
        BMP_CR = Nothing
        If BLN_LARGE Then
            BMP_CR = FUNC_GET_CHANEG_IMAGE("COPYRIGHT_L.png")
            If BMP_CR Is Nothing Then
                BMP_CR = My.Resources.COPYRIGHT_L
            End If
        Else
            BMP_CR = FUNC_GET_CHANEG_IMAGE("COPYRIGHT_S.png")
            If BMP_CR Is Nothing Then
                BMP_CR = My.Resources.COPYRIGHT_S
            End If
        End If

        Call BMP_CR.MakeTransparent()

        Dim GRP_DRAW As Graphics
        GRP_DRAW = Graphics.FromImage(BMP_BASE)
        Call GRP_DRAW.DrawImage(BMP_CR, 0, BMP_BASE.Height - BMP_CR.Height, BMP_CR.Width, BMP_CR.Height)
        Call GRP_DRAW.Dispose()
    End Sub
#End Region

End Module
