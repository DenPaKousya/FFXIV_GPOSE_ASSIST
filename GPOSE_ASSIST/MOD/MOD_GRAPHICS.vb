Imports System.Runtime.InteropServices
Public Module MOD_GRAPHICS

#Region "WINAPI"
    <DllImport("user32.dll")> Private Function PrintWindow(ByVal hWnd As System.IntPtr, ByVal dc As System.IntPtr, ByVal reservedFlag As UInteger) As Integer
    End Function
#End Region

#Region "構造体"
    Private Structure SRT_CLIENT_IMAGE
        Public RECT As MOD_PROCESS_WINDOW.RECT
        Public PIXEL_FORMAT As System.Drawing.Imaging.PixelFormat
        Public HEIGHT As Integer
        Public WIDTH As Integer
        Public GRAPH As System.Drawing.Graphics
        Public IMAGE As System.Drawing.Bitmap
    End Structure

#End Region

#Region "変数"
    Private srtPARAM As SRT_CLIENT_IMAGE
#End Region

    Delegate Function DelPrintWindow(ByVal hWnd As System.IntPtr, ByVal dc As System.IntPtr, ByVal reservedFlag As Integer) As Boolean

    Private DEL_PRINT_WINDOW As [Delegate]
    Private MTD_PRINT_WINDOW As DelPrintWindow
    Private LOD_USER32 As DynamicLibraryLoader

    Public Sub SUB_INIT_GAPHICS()
        Dim blnRET As Boolean

        LOD_USER32 = New DynamicLibraryLoader

        blnRET = LOD_USER32.Load("user32.dll")

        If Not blnRET Then
            LOD_USER32 = Nothing
            Exit Sub
        End If

        DEL_PRINT_WINDOW = LOD_USER32.GetDelegate("PrintWindow", GetType(DelPrintWindow))

        If DEL_PRINT_WINDOW Is Nothing Then
            Call LOD_USER32.Free()
            Exit Sub
        End If

        MTD_PRINT_WINDOW = CType(DEL_PRINT_WINDOW, DelPrintWindow)
    End Sub

    Public Sub SUB_END_GAPHICS()

        If Not DEL_PRINT_WINDOW Is Nothing Then
            DEL_PRINT_WINDOW = Nothing
        End If

        If LOD_USER32 Is Nothing Then
            Exit Sub
        End If

        Call LOD_USER32.Free()

        LOD_USER32 = Nothing
    End Sub

    Public Sub SUB_PRINT_WINDOW_TEST(ByRef GRP_BITMAP As Graphics, PRC_TARGET As Process)

        If GRP_BITMAP Is Nothing Then
            Exit Sub
        End If

        If PRC_TARGET Is Nothing Then
            Exit Sub
        End If

        If DEL_PRINT_WINDOW Is Nothing Then
            Exit Sub
        End If

        Dim PTR_GRAPHIC As IntPtr
        PTR_GRAPHIC = GRP_BITMAP.GetHdc

        Const CST_FLAG As Integer = (1 Or 2)
        'Const CST_FLAG As Integer = (1)
        Dim BLN_RET As Boolean
        BLN_RET = MTD_PRINT_WINDOW(PRC_TARGET.MainWindowHandle, PTR_GRAPHIC, CST_FLAG)

        Call GRP_BITMAP.ReleaseHdc(PTR_GRAPHIC)
    End Sub

End Module
