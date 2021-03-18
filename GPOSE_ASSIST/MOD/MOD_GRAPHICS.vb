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
    Delegate Function DelGetDC(ByVal hwnd As IntPtr) As IntPtr
    Delegate Function DelReleaseDC(ByVal hwnd As IntPtr, ByVal hdc As IntPtr) As IntPtr
    Delegate Function DelBitBlt(ByVal hDestDC As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As IntPtr, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer

    Private LOD_USER32 As DynamicLibraryLoader
    Private LOD_GDI32 As DynamicLibraryLoader

    Private DEL_PRINT_WINDOW As [Delegate]
    Private MTD_PRINT_WINDOW As DelPrintWindow

    Private DEL_GET_DC As [Delegate]
    Private MTD_GET_DC As DelGetDC

    Private DEL_RELEASE_DC As [Delegate]
    Private MTD_RELEASE_DC As DelReleaseDC

    Private DEL_BIT_BLT As [Delegate]
    Private MTD_BIT_BLT As DelBitBlt

    Public Sub SUB_INIT_GAPHICS()

        LOD_USER32 = New DynamicLibraryLoader

        Dim BLN_RET As Boolean
        BLN_RET = LOD_USER32.Load("user32.dll")

        If Not BLN_RET Then
            LOD_USER32 = Nothing
            Exit Sub
        End If

        DEL_PRINT_WINDOW = LOD_USER32.GetDelegate("PrintWindow", GetType(DelPrintWindow))
        If DEL_PRINT_WINDOW Is Nothing Then
            Call LOD_USER32.Free()
            Exit Sub
        End If
        MTD_PRINT_WINDOW = CType(DEL_PRINT_WINDOW, DelPrintWindow)

        DEL_GET_DC = LOD_USER32.GetDelegate("GetDC", GetType(DelGetDC))
        If DEL_GET_DC Is Nothing Then
            Call LOD_USER32.Free()
            Exit Sub
        End If
        MTD_GET_DC = CType(DEL_GET_DC, DelGetDC)

        DEL_RELEASE_DC = LOD_USER32.GetDelegate("ReleaseDC", GetType(DelReleaseDC))
        If DEL_RELEASE_DC Is Nothing Then
            Call LOD_USER32.Free()
            Exit Sub
        End If
        MTD_RELEASE_DC = CType(DEL_RELEASE_DC, DelReleaseDC)


        LOD_GDI32 = New DynamicLibraryLoader
        BLN_RET = LOD_GDI32.Load("gdi32.dll")
        If Not BLN_RET Then
            LOD_GDI32 = Nothing
            Exit Sub
        End If

        DEL_BIT_BLT = LOD_GDI32.GetDelegate("BitBlt", GetType(DelBitBlt))
        If DEL_BIT_BLT Is Nothing Then
            Call LOD_GDI32.Free()
            Exit Sub
        End If
        MTD_BIT_BLT = CType(DEL_BIT_BLT, DelBitBlt)

    End Sub

    Public Sub SUB_END_GAPHICS()

        If Not DEL_PRINT_WINDOW Is Nothing Then
            DEL_PRINT_WINDOW = Nothing
        End If

        If Not DEL_GET_DC Is Nothing Then
            DEL_GET_DC = Nothing
        End If

        If Not DEL_RELEASE_DC Is Nothing Then
            DEL_RELEASE_DC = Nothing
        End If

        If Not LOD_USER32 Is Nothing Then
            Call LOD_USER32.Free()
            LOD_USER32 = Nothing
        End If

        If Not DEL_BIT_BLT Is Nothing Then
            DEL_BIT_BLT = Nothing
        End If

        If Not LOD_GDI32 Is Nothing Then
            Call LOD_GDI32.Free()
            LOD_GDI32 = Nothing
        End If

    End Sub

    Public Sub SUB_PRINT_WINDOW_TEST(ByRef GRP_BITMAP As Graphics, ByRef PRC_TARGET As Process)

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

    Private Declare Function GetDC Lib "user32" (ByVal hwnd As IntPtr) As IntPtr
    Private Declare Function ReleaseDC Lib "user32" (ByVal hwnd As IntPtr, ByVal hdc As IntPtr) As IntPtr

    Private Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hwnd As IntPtr) As IntPtr
    Private Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As IntPtr, ByVal nWidth As Long, ByVal nHeight As Long) As IntPtr
    Private Declare Function SelectObject Lib "gdi32" (ByVal hdc As IntPtr, ByVal hgdiobj As IntPtr) As IntPtr

    Private Declare Function DeleteObject Lib "gdi32" (ByVal hObject As IntPtr) As Boolean
    Private Declare Function DeleteDC Lib "gdi32" (ByVal hdc As IntPtr) As IntPtr

    Private Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As IntPtr, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer

#Region "API用定数"
    Private Const SRCCOPY As Integer = &HCC0020
    Private Const SRCPAINT As Integer = &HEE0086
    Private Const CAPTUREBLT As Integer = &H40000000
#End Region

    Public Sub SUB_BIT_BLT(ByRef GRP_BITMAP As Graphics, ByRef PRC_TARGET As Process, ByVal INT_WIDTH As Integer, ByVal INT_HEIGHT As Integer)
        Dim DC_SCREEN As IntPtr

        DC_SCREEN = MTD_GET_DC(PRC_TARGET.MainWindowHandle)

        'Dim DC_COMPATIBLE As IntPtr
        'DC_COMPATIBLE = CreateCompatibleDC(DC_SCREEN)

        'Dim DC_BMP As IntPtr
        'DC_BMP = CreateCompatibleBitmap(DC_SCREEN, INT_WIDTH, INT_HEIGHT)

        'Dim DC_BMP_OLD As IntPtr
        'DC_BMP_OLD = SelectObject(DC_COMPATIBLE, DC_BMP)

        Dim DC_GRP As IntPtr
        DC_GRP = GRP_BITMAP.GetHdc

        Dim INT_X As Integer
        Dim INT_Y As Integer
        INT_X = 0
        INT_Y = 0

        Call MTD_BIT_BLT(DC_GRP, 0, 0, INT_WIDTH, INT_HEIGHT, DC_SCREEN, INT_X, INT_Y, SRCCOPY Or CAPTUREBLT)
        Call GRP_BITMAP.ReleaseHdc(DC_GRP)

        'Call SelectObject(DC_COMPATIBLE, DC_BMP_OLD)

        'Dim BMP_SOURCE As System.Windows.Media.Imaging.BitmapSource
        'BMP_SOURCE = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(DC_BMP, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
        'Call BMP_SOURCE.Freeze()

        'Call DeleteObject(DC_BMP)
        'Call DeleteDC(DC_COMPATIBLE)
        Call MTD_RELEASE_DC(PRC_TARGET.MainWindowHandle, DC_SCREEN)
    End Sub

    Public Function FUNC_GET_BS_FROM_BIT_BLT(ByRef GRP_BITMAP As Graphics, ByRef PRC_TARGET As Process, ByVal INT_WIDTH As Integer, ByVal INT_HEIGHT As Integer) As System.Windows.Media.Imaging.BitmapSource

        Dim DC_SCREEN As IntPtr
        DC_SCREEN = MTD_GET_DC(PRC_TARGET.MainWindowHandle)

        Dim DC_COMPATIBLE As IntPtr
        DC_COMPATIBLE = CreateCompatibleDC(DC_SCREEN)

        Dim DC_BMP As IntPtr
        DC_BMP = CreateCompatibleBitmap(DC_SCREEN, INT_WIDTH, INT_HEIGHT)

        Dim DC_BMP_OLD As IntPtr
        DC_BMP_OLD = SelectObject(DC_COMPATIBLE, DC_BMP)

        Dim INT_X As Integer
        Dim INT_Y As Integer
        INT_X = 0
        INT_Y = 0
        Call MTD_BIT_BLT(DC_COMPATIBLE, 0, 0, INT_WIDTH, INT_HEIGHT, DC_SCREEN, INT_X, INT_Y, SRCCOPY Or CAPTUREBLT)

        Call SelectObject(DC_COMPATIBLE, DC_BMP_OLD)

        Dim BMP_SOURCE As System.Windows.Media.Imaging.BitmapSource
        Try
            BMP_SOURCE = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(DC_BMP, IntPtr.Zero, System.Windows.Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions())
        Catch ex As Exception
            Call DeleteObject(DC_BMP_OLD)
            Call DeleteObject(DC_BMP)
            Call DeleteDC(DC_COMPATIBLE)
            Call MTD_RELEASE_DC(PRC_TARGET.MainWindowHandle, DC_SCREEN)
            Return Nothing
        End Try
        Call BMP_SOURCE.Freeze()

        Call DeleteObject(DC_BMP_OLD)
        Call DeleteObject(DC_BMP)
        Call DeleteDC(DC_COMPATIBLE)
        Call MTD_RELEASE_DC(PRC_TARGET.MainWindowHandle, DC_SCREEN)

        Return BMP_SOURCE
    End Function

End Module
