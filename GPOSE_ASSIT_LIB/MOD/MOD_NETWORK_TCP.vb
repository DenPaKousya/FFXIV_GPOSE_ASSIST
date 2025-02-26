Imports System.Net.Sockets

Public Module MOD_NETWORK_TCP

#Region "外部用・変数"
    Private FRM_PARENT As Object
#End Region

#Region "モジュール用・変数"
    Private TCL_SERVER_LISTNER As System.Net.Sockets.TcpListener
#End Region

    Public Function FUNC_SERVER_START(ByVal INT_NUMBER_PORT As Integer, ByRef OBJ_PARENT As Object) As Boolean

        Try
            TCL_SERVER_LISTNER = New System.Net.Sockets.TcpListener(IPAddress.Any, INT_NUMBER_PORT)
            Call TCL_SERVER_LISTNER.Start()
        Catch ex As Exception
            Return False
        End Try

        FRM_PARENT = OBJ_PARENT
        Return True
    End Function

    Public Function FUNC_SERVER_STOP() As Boolean

        If TCL_SERVER_LISTNER Is Nothing Then
            Return True
        End If

        Try
            Call TCL_SERVER_LISTNER.Stop()
        Catch ex As Exception
            Return False
        End Try
        Call TCL_SERVER_LISTNER.Dispose()
        TCL_SERVER_LISTNER = Nothing
        Return True
    End Function

End Module
