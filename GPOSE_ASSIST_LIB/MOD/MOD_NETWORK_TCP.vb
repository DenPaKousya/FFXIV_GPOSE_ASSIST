Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports Microsoft.SqlServer

Public Module MOD_NETWORK_TCP

#Region "外部用・変数"
    Private FRM_PARENT As Object
#End Region

#Region "モジュール用・変数"
    Private TCL_SERVER_LISTNER As System.Net.Sockets.TcpListener

    Private SMR_SERVER_READER As System.IO.StreamReader
    Private SMR_SERVER_WRITER As System.IO.StreamWriter

    Private THR_SERVER As System.Threading.Thread

    Private TCC_CLIENT_SESSION As System.Net.Sockets.TcpClient
    Private SMR_CLIENT_READER As System.IO.StreamReader
    Private SMR_CLIENT_WRITER As System.IO.StreamWriter
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

    Public Function FUNC_SERVER_INIT() As Boolean
        Try

            If Not THR_SERVER Is Nothing Then
                Call THR_SERVER.Interrupt()
            End If

            THR_SERVER = New Thread(New ThreadStart(AddressOf SUB_SERVER_THREAD))
            THR_SERVER.IsBackground = True
            Call THR_SERVER.Start()
        Catch ex As Exception

            Return False
        End Try

        Return True
    End Function

    Public Function FUNC_SERVER_FIN() As Boolean

        If Not THR_SERVER Is Nothing Then
            Call THR_SERVER.Interrupt()
        End If

        Call System.Threading.Thread.Sleep(100)
        THR_SERVER = Nothing

        Return True
    End Function

    Private Sub SUB_SERVER_THREAD()

        Dim TCC_CLIENT As System.Net.Sockets.TcpClient
        Try
            TCC_CLIENT = TCL_SERVER_LISTNER.AcceptTcpClient()
        Catch ex As Exception
            Exit Sub
        End Try

        Dim STM_N_STREAM As System.Net.Sockets.NetworkStream
        STM_N_STREAM = TCC_CLIENT.GetStream()

        SMR_SERVER_READER = New StreamReader(STM_N_STREAM, System.Text.Encoding.UTF8)
        SMR_SERVER_WRITER = New StreamWriter(STM_N_STREAM, System.Text.Encoding.UTF8)

        While True
            Try
                Call ProcessMessage(SMR_SERVER_READER)
            Catch ex As ThreadInterruptedException
                Exit Sub
            Catch ex As Exception

            End Try

        End While
    End Sub

    Private OBJ_THREAD_LOCK As New Object
    Private Sub ProcessMessage(ByRef SMR_MESSAGE As System.IO.StreamReader)

        SyncLock OBJ_THREAD_LOCK
            If SMR_MESSAGE Is Nothing Then
                Exit Sub
            End If

            Dim STR_MESSAGE As String
            STR_MESSAGE = SMR_MESSAGE.ReadLine()
            If STR_MESSAGE Is Nothing Then
                Exit Sub
            End If

            STR_MESSAGE &= "aaa"
        End SyncLock

    End Sub

    Public Function FUNC_CLIENT_INIT(ByVal STR_IP As String, ByVal INT_NUMBER_PORT As Integer) As Boolean

        Try
            TCC_CLIENT_SESSION = New TcpClient()
            Call TCC_CLIENT_SESSION.Connect(STR_IP, INT_NUMBER_PORT)
        Catch ex As Exception
            Return False
        End Try

        Dim STM_N_STREAM As System.Net.Sockets.NetworkStream
        STM_N_STREAM = TCC_CLIENT_SESSION.GetStream()
        SMR_CLIENT_READER = New StreamReader(STM_N_STREAM, System.Text.Encoding.UTF8)
        SMR_CLIENT_WRITER = New StreamWriter(STM_N_STREAM, System.Text.Encoding.UTF8)

        Return True
    End Function

    Public Function FUNC_CLIENT_FIN() As Boolean

        If Not SMR_CLIENT_READER Is Nothing Then
            Call SMR_CLIENT_READER.BaseStream.Close()
            Call SMR_CLIENT_READER.Close()
            Call SMR_CLIENT_READER.Dispose()
        End If
        SMR_CLIENT_READER = Nothing

        If Not SMR_CLIENT_WRITER Is Nothing Then
            Try
                Call SMR_CLIENT_WRITER.Close()
                Call SMR_CLIENT_WRITER.Dispose()
            Catch ex As Exception

            End Try
        End If
        SMR_CLIENT_WRITER = Nothing

        If Not TCC_CLIENT_SESSION Is Nothing Then
            Call TCC_CLIENT_SESSION.Close()
            Call TCC_CLIENT_SESSION.Dispose()
            TCC_CLIENT_SESSION = Nothing
        End If

        Return True
    End Function

    Public Function FUNC_CLIENT_SEND(ByVal STR_SEND As String) As Boolean
        Try
            Call SMR_CLIENT_WRITER.WriteLine(STR_SEND)
            Call SMR_CLIENT_WRITER.Flush()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
End Module
