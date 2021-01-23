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
End Module
