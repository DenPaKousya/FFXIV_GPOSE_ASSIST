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

    Public Function FUNC_GET_WINDOW_RECT_WH(ByRef PRC_WINDOW As Process) As RECT_WH
        Dim SRT_RECT As RECT
        SRT_RECT = FUNC_GET_WINDOW_RECT(PRC_WINDOW)

        Dim SRT_RET As RECT_WH
        SRT_RET = FUNC_CNV_RECT_TO_RECTWH(SRT_RECT)

        Return SRT_RET
    End Function

    Public Function FUNC_GET_LEFT_CLIENT(ByRef PRC_WINDOW As Process) As Integer
        Dim SRT_WINDOW_WH As RECT_WH
        SRT_WINDOW_WH = FUNC_GET_WINDOW_RECT_WH(PRC_WINDOW)

        Dim intWINDOW_LEFT As Integer
        Dim intWINDOW_TOP As Integer
        intWINDOW_LEFT = SRT_WINDOW_WH.left

        If intWINDOW_LEFT = 0 Then
            Return 0
        End If

        intWINDOW_TOP = SRT_WINDOW_WH.top

        Dim intWINDOW_WIDTH As Integer
        Dim intWINDOW_HEIGHT As Integer
        intWINDOW_WIDTH = SRT_WINDOW_WH.width
        intWINDOW_HEIGHT = SRT_WINDOW_WH.height

        Dim SRT_CLIENT_WH As RECT_WH
        SRT_CLIENT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_WINDOW)
        Dim intCLIENT_WIDTH As Integer
        Dim intCLIENT_HEIGHT As Integer
        intCLIENT_WIDTH = SRT_CLIENT_WH.width
        intCLIENT_HEIGHT = SRT_CLIENT_WH.height

        Dim intWIDTH_SUB As Integer
        Dim intHEIGHT_SUB As Integer
        intWIDTH_SUB = intWINDOW_WIDTH - intCLIENT_WIDTH
        intHEIGHT_SUB = intWINDOW_HEIGHT - intCLIENT_HEIGHT

        Dim INT_BORDER As Integer
        INT_BORDER = (intWIDTH_SUB / 2)

        Dim INT_RET As Integer
        INT_RET = intWINDOW_LEFT + INT_BORDER
        Return INT_RET
    End Function

    Public Function FUNC_GET_TOP_CLIENT(ByRef PRC_WINDOW As Process) As Integer
        Dim SRT_WINDOW_WH As RECT_WH
        SRT_WINDOW_WH = FUNC_GET_WINDOW_RECT_WH(PRC_WINDOW)

        Dim intWINDOW_LEFT As Integer
        Dim intWINDOW_TOP As Integer
        intWINDOW_LEFT = SRT_WINDOW_WH.left
        intWINDOW_TOP = SRT_WINDOW_WH.top

        If intWINDOW_TOP = 0 Then
            Return 0
        End If

        Dim intWINDOW_WIDTH As Integer
        Dim intWINDOW_HEIGHT As Integer
        intWINDOW_WIDTH = SRT_WINDOW_WH.width
        intWINDOW_HEIGHT = SRT_WINDOW_WH.height

        Dim SRT_CLIENT_WH As RECT_WH
        SRT_CLIENT_WH = FUNC_GET_CRIENT_RECT_WH(PRC_WINDOW)
        Dim intCLIENT_WIDTH As Integer
        Dim intCLIENT_HEIGHT As Integer
        intCLIENT_WIDTH = SRT_CLIENT_WH.width
        intCLIENT_HEIGHT = SRT_CLIENT_WH.height

        Dim intWIDTH_SUB As Integer
        Dim intHEIGHT_SUB As Integer
        intWIDTH_SUB = intWINDOW_WIDTH - intCLIENT_WIDTH
        intHEIGHT_SUB = intWINDOW_HEIGHT - intCLIENT_HEIGHT

        Dim INT_BORDER As Integer
        INT_BORDER = (intHEIGHT_SUB / 2)

        Dim INT_RET As Integer
        INT_RET = intWINDOW_TOP + INT_BORDER

        Return INT_RET
    End Function

#Region "内部処理"
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

#End Region

End Module
