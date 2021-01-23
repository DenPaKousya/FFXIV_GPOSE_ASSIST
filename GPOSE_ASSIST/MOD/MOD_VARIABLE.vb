Module MOD_VARIABLE

#Region "PROCESS"
    Public PRC_APP_TARGET As Process = Nothing
    Public Function FUNC_GET_ID_APP_TARGET() As Integer
        If PRC_APP_TARGET Is Nothing Then
            Return 0
        End If

        Dim INT_RET As Integer
        Try
            INT_RET = PRC_APP_TARGET.Id
        Catch ex As Exception
            INT_RET = 0
        End Try

        Return INT_RET
    End Function
#End Region

#Region "APP.CONFIG"
    Public STR_APP_CONF_PROCESS_NAME As String = ""
#End Region

#Region "MAIN.WINDOW"
    Public FRM_APPL_MAIN As FRM_MAIN
#End Region

End Module
