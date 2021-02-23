Imports System.Runtime.InteropServices
Module MOD_HOT_KEY

#Region "WINAPI"
    <DllImport("user32", EntryPoint:="RegisterHotKey")> Private Function RegisterHotKey(ByVal hWnd As IntPtr, ByVal id As Integer, ByVal fsModifier As Integer, ByVal vk As Integer) As Integer
    End Function

    <DllImport("user32", EntryPoint:="UnregisterHotKey")> Private Function UnregisterHotKey(ByVal hWnd As IntPtr, ByVal id As Integer) As Integer
    End Function

    'ホットキーの修飾キー
    Private Const MOD_ALT As Byte = &H1
    Private Const MOD_CONTROL As Byte = &H2
    Private Const MOD_SHIFT As Byte = &H4

    <Flags()>
    Public Enum KeyModifiers As Integer
        None = 0
        Alt = MOD_ALT
        Control = MOD_CONTROL
        Shift = MOD_SHIFT
    End Enum
#End Region

    ' ホットキーの設定を行う
    Public Function FUNC_REGISTER_HOT_KEY(ByVal hWnd As IntPtr, ByRef id As Integer, ByRef lParam As IntPtr, ByVal modifier As KeyModifiers, ByVal key As Keys) As Boolean
        Dim INT_MOD As Integer
        INT_MOD = CInt(modifier)

        Dim INT_KEY As Integer
        INT_KEY = CInt(key)

        id = (INT_MOD * &H100) Or INT_KEY

        Dim INT_PARAM As Integer
        INT_PARAM = INT_MOD Or INT_KEY * &H10000
        lParam = New IntPtr(INT_PARAM)

        Dim INT_RET As Integer
        INT_RET = RegisterHotKey(hWnd, id, CInt(modifier), CInt(key))

        Return (INT_RET <> 0)

    End Function

    Public Function FUNC_UNREGISTER_HOT_KEY(ByVal hWnd As IntPtr, ByRef id As Integer) As Boolean
        Dim INT_RET As Integer
        INT_RET = UnregisterHotKey(hWnd, id)

        Return (INT_RET <> 0)
    End Function

    Public Function FUNC_GET_KEY_HOTKEY(ByVal STR_KEY_NAME As String) As System.Windows.Forms.Keys
        Dim ENM_RET As System.Windows.Forms.Keys
        Select Case STR_KEY_NAME
            Case "A", "a"
                ENM_RET = Keys.A
            Case "B", "b"
                ENM_RET = Keys.B
            Case "C", "c"
                ENM_RET = Keys.C
            Case "D", "d"
                ENM_RET = Keys.D
            Case "E", "e"
                ENM_RET = Keys.E
            Case "F", "f"
                ENM_RET = Keys.F
            Case "G", "g"
                ENM_RET = Keys.G

            Case "F1"
                ENM_RET = Keys.F1
            Case "F2"
                ENM_RET = Keys.F2
            Case "F3"
                ENM_RET = Keys.F3
            Case "F4"
                ENM_RET = Keys.F4
            Case "F5"
                ENM_RET = Keys.F5
            Case "F6"
                ENM_RET = Keys.F6
            Case "F7"
                ENM_RET = Keys.F7
            Case "F8"
                ENM_RET = Keys.F8
            Case "F9"
                ENM_RET = Keys.F9
            Case "F10"
                ENM_RET = Keys.F10
            Case "F11"
                ENM_RET = Keys.F11
            Case "F12"
                ENM_RET = Keys.F12
            Case Else
                ENM_RET = Keys.None
        End Select

        Return ENM_RET
    End Function

    Public Function FUNC_GET_MOD_HOTKEY(ByVal STR_MOD_NAME As String) As KeyModifiers
        Dim ENM_RET As KeyModifiers
        Select Case STR_MOD_NAME
            Case "Ctrl", "Control"
                ENM_RET = KeyModifiers.Control
            Case "Alt"
                ENM_RET = KeyModifiers.Alt
            Case "Shift"
                ENM_RET = KeyModifiers.Shift
            Case Else
                ENM_RET = KeyModifiers.None
        End Select

        Return ENM_RET
    End Function
End Module
