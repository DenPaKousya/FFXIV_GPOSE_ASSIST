Imports System.Runtime.InteropServices
Public Module MOD_DWM

#Region "定数"
    Public Const DWM_TNP_VISIBLE As Integer = &H8
    Public Const DWM_TNP_OPACITY As Integer = &H4
    Public Const DWM_TNP_RECTDESTINATION As Integer = &H1
#End Region

#Region "列挙定数"
    Public Structure PSIZE
        Public x As Integer
        Public y As Integer
    End Structure

    Public Structure RECT_DWM
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure

    Public Structure DWM_THUMBNAIL_PROPERTIES
        Public dwFlags As Integer
        Public rcDestination As MOD_DWM.RECT_DWM
        Public rcSource As MOD_DWM.RECT_DWM
        Public opacity As Byte
        Public fVisible As Boolean
        Public fSourceClientAreaOnly As Boolean
    End Structure

#End Region

    <DllImport("dwmapi.dll")>
    Public Function DwmRegisterThumbnail(ByVal dest As IntPtr, ByVal src As IntPtr, ByRef thumb As IntPtr) As Integer
    End Function

    <DllImport("dwmapi.dll")>
    Public Function DwmQueryThumbnailSourceSize(ByVal thumb As IntPtr, ByRef size As PSIZE) As Integer
    End Function

    <DllImport("dwmapi.dll")>
    Public Function DwmUpdateThumbnailProperties(ByVal hThumb As IntPtr, ByRef props As DWM_THUMBNAIL_PROPERTIES) As Integer
    End Function

End Module
