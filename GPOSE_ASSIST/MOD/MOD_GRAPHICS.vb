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

    Private delPRINT_WINDOW As [Delegate]
    Private mtdPRINT_WINDOW As DelPrintWindow
    Private lodUSER32 As DynamicLibraryLoader

    Public Sub SUB_INIT_GAPHICS()
        Dim blnRET As Boolean

        lodUSER32 = New DynamicLibraryLoader

        blnRET = lodUSER32.Load("user32.dll")

        If Not blnRET Then
            lodUSER32 = Nothing
            Exit Sub
        End If

        delPRINT_WINDOW = lodUSER32.GetDelegate("PrintWindow", GetType(DelPrintWindow))

        If delPRINT_WINDOW Is Nothing Then
            Call lodUSER32.Free()
            Exit Sub
        End If

        mtdPRINT_WINDOW = CType(delPRINT_WINDOW, DelPrintWindow)
    End Sub

    Public Sub SUB_END_GAPHICS()

        If Not delPRINT_WINDOW Is Nothing Then
            delPRINT_WINDOW = Nothing
        End If

        If lodUSER32 Is Nothing Then
            Exit Sub
        End If

        Call lodUSER32.Free()

        lodUSER32 = Nothing
    End Sub

    Public Sub SUB_PRINT_WINDOW_TEST(ByRef GRP_BITMAP As Graphics, PRC_TARGET As Process)

        If GRP_BITMAP Is Nothing Then
            Exit Sub
        End If

        If PRC_TARGET Is Nothing Then
            Exit Sub
        End If

        If delPRINT_WINDOW Is Nothing Then
            Exit Sub
        End If

        Dim PTR_GRAPHIC As IntPtr
        PTR_GRAPHIC = GRP_BITMAP.GetHdc

        Const CST_FLAG As Integer = (1 Or 2)
        'Const CST_FLAG As Integer = (1)
        Dim BLN_RET As Boolean
        BLN_RET = mtdPRINT_WINDOW(PRC_TARGET.MainWindowHandle, PTR_GRAPHIC, CST_FLAG)

        Call GRP_BITMAP.ReleaseHdc(PTR_GRAPHIC)
    End Sub

End Module
