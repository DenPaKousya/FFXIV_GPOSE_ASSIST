Module MOD_PROCESS

    Public Function FUNC_GET_PROCESS(ByVal STR_PROCESS_NAME As String) As System.Diagnostics.Process

        Dim PRC_GROUP As System.Diagnostics.Process
        For Each PRC_GROUP In Process.GetProcesses()
            'If (PRC_GROUP.MainWindowHandle = IntPtr.Zero) Then
            '    Continue For
            'End If
            If (PRC_GROUP.ProcessName = STR_PROCESS_NAME) Then
                Return PRC_GROUP
            End If
        Next

        Return Nothing
    End Function

    Public Function FUNC_GET_APPLICATION_NAME(ByVal STR_PROCESS_NAME As String) As String
        Dim STR_RET As String

        Select Case STR_PROCESS_NAME
            Case "ffxiv_dx11"
                STR_RET = "FFXIV"
            Case "chrome"
                STR_RET = "Google Chrome"
            Case "notepad"
                STR_RET = "メモ帳"
            Case "FlowScape"
                STR_RET = "FlowScape"
            Case "GenshinImpact"
                STR_RET = "原神"
            Case Else
                STR_RET = "Unknown"
        End Select

        Return STR_RET
    End Function
End Module
