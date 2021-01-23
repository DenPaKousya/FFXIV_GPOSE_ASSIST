Imports System.Runtime.InteropServices
Public Module MOD_PROCESS_WINDOW

#Region "WIN32API"
    Public Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure

    <DllImport("user32.dll")> Private Function GetWindowRect(ByVal hwnd As IntPtr, ByRef lpRect As RECT) As Integer
    End Function
    <DllImport("user32.dll")> Private Function GetClientRect(ByVal hwnd As IntPtr, ByRef lpRect As RECT) As Integer
    End Function
#End Region

#Region "外部：構造体"
    Public Structure RECT_WH
        Public left As Integer
        Public top As Integer
        Public width As Integer
        Public height As Integer
    End Structure

#End Region

    Private Function FUNC_GET_INIT_RECT() As RECT
        Dim SRT_RET As RECT

        With SRT_RET
            .left = 0
            .top = 0
            .right = 0
            .bottom = 0
        End With

        Return SRT_RET
    End Function

    Public Function FUNC_GET_CRIENT_RECT(ByRef PRC_CRIENT As Process) As RECT
        Dim SRT_RET As RECT
        SRT_RET = FUNC_GET_INIT_RECT()

        If PRC_CRIENT Is Nothing Then
            Return SRT_RET
        End If

        Call GetClientRect(PRC_CRIENT.MainWindowHandle, SRT_RET)

        Return SRT_RET
    End Function

    Public Function FUNC_GET_CRIENT_RECT_WH(ByRef PRC_CRIENT As Process) As RECT_WH
        Dim SRT_RECT As RECT
        SRT_RECT = FUNC_GET_CRIENT_RECT(PRC_CRIENT)

        Dim SRT_RET As RECT_WH
        SRT_RET = FUNC_CNV_RECT_TO_RECTWH(SRT_RECT)

        Return SRT_RET
    End Function

    Public Function FUNC_GET_WINDOW_RECT(ByRef PRC_WINDOW As Process) As RECT
        Dim SRT_RET As RECT
        SRT_RET = FUNC_GET_INIT_RECT()

        If PRC_WINDOW Is Nothing Then
            Return SRT_RET
        End If

        Call GetWindowRect(PRC_WINDOW.MainWindowHandle, SRT_RET)

        Return SRT_RET
    End Function

    Public Function FUNC_GET_WINDOW_RECT_WH(ByRef PRC_CRIENT As Process) As RECT_WH
        Dim SRT_RECT As RECT
        SRT_RECT = FUNC_GET_WINDOW_RECT(PRC_CRIENT)

        Dim SRT_RET As RECT_WH
        SRT_RET = FUNC_CNV_RECT_TO_RECTWH(SRT_RECT)

        Return SRT_RET
    End Function

    Private Function FUNC_GET_RECT_WIDTH(ByRef SRT_RECT As RECT) As Integer
        Dim INT_RET As Integer

        INT_RET = SRT_RECT.right - SRT_RECT.left

        Return INT_RET
    End Function

    Private Function FUNC_GET_RECT_HEIGHT(ByRef SRT_RECT As RECT) As Integer
        Dim INT_RET As Integer

        INT_RET = SRT_RECT.bottom - SRT_RECT.top

        Return INT_RET
    End Function

    Private Function FUNC_CNV_RECT_TO_RECTWH(ByRef SRT_RECT As RECT) As RECT_WH
        Dim SRT_RET As RECT_WH
        With SRT_RET
            .left = SRT_RECT.left
            .top = SRT_RECT.top
            .width = FUNC_GET_RECT_WIDTH(SRT_RECT)
            .height = FUNC_GET_RECT_HEIGHT(SRT_RECT)
        End With

        Return SRT_RET
    End Function


End Module
