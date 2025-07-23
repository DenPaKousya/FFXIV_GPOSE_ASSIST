Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports Microsoft.SqlServer

Public Module MOD_NETWORK_TCP

#Region "外部用・変数"
    Public STR_SEVER_LAST_RECV As String = ""
    Public STR_MODULE_LAST_ERROR As String = ""
#End Region

#Region "モジュール用・変数"
    Private FRM_SERVER As Object

    Private SMR_SERVER_READER As System.IO.StreamReader
    Private SMR_SERVER_WRITER As System.IO.StreamWriter

    Private THR_SERVER As System.Threading.Thread

    Private TCC_CLIENT_SESSION As System.Net.Sockets.TcpClient
    Private SMR_CLIENT_READER As System.IO.StreamReader
    Private SMR_CLIENT_WRITER As System.IO.StreamWriter

    Private STR_CONNECT_IP As String = ""
    Private INT_CONNECT_NUMBER_PORT As Integer = 0

    Private BLN_SERVER_THREAD_ENABLED As Boolean = False


    Private TCL_SERVER_LISTNER As System.Net.Sockets.TcpListener
    Private INT_COMMON_NUMBER_PORT As Integer

    Private BLN_LISTENER_ENABLED As Boolean = False
#End Region

    Public Function FUNC_SERVER_START(ByVal INT_NUMBER_PORT As Integer, ByRef OBJ_PARENT As Object) As Boolean

        STR_SEVER_LAST_RECV = ""
        Try
            INT_COMMON_NUMBER_PORT = INT_NUMBER_PORT
            TCL_SERVER_LISTNER = New System.Net.Sockets.TcpListener(IPAddress.Any, INT_COMMON_NUMBER_PORT)
            Call TCL_SERVER_LISTNER.Start()
            BLN_LISTENER_ENABLED = True
        Catch ex As Exception
            Return False
        End Try

        FRM_SERVER = OBJ_PARENT
        Return True
    End Function

    Public Function FUNC_SERVER_STOP() As Boolean

        STR_SEVER_LAST_RECV = ""

        If Not SMR_SERVER_READER Is Nothing Then
            Call SMR_SERVER_READER.BaseStream.Close()
            Call SMR_SERVER_READER.Close()
        End If

        If Not SMR_SERVER_WRITER Is Nothing Then
            'Call SMR_SERVER_WRITER.Close()
            'Call SMR_SERVER_WRITER.Dispose()
            SMR_SERVER_WRITER = Nothing
        End If

        If Not THR_SERVER Is Nothing Then
            Call THR_SERVER.Interrupt()
        End If
        THR_SERVER = Nothing

        Call TCL_SERVER_LISTNER.Dispose()
        TCL_SERVER_LISTNER = Nothing
        Return True
    End Function

    Public Function FUNC_SERVER_INIT() As Boolean

        Try

            If Not THR_SERVER Is Nothing Then
                Call THR_SERVER.Interrupt()
            End If

            BLN_SERVER_THREAD_ENABLED = True
            THR_SERVER = New Thread(New ThreadStart(AddressOf SUB_SERVER_THREAD))
            'THR_SERVER.IsBackground = True
            Call THR_SERVER.Start()
        Catch ex As Exception

            Return False
        End Try

        Return True
    End Function

    Public Function FUNC_SERVER_FIN(ByVal BLN_SLEEP As Boolean) As Boolean

        If TCL_SERVER_LISTNER Is Nothing Then
            Return True
        End If

        Try
            Call TCL_SERVER_LISTNER.Stop()
            BLN_LISTENER_ENABLED = False
        Catch ex As Exception
            Return False
        End Try

        BLN_SERVER_THREAD_ENABLED = False

        'If Not THR_SERVER Is Nothing Then
        '    'THR_SERVER.IsBackground = False
        '    Call THR_SERVER.Interrupt()
        'End If
        'THR_SERVER = Nothing

        'Try
        '    If BLN_SLEEP Then
        '        Call System.Threading.Thread.Sleep(100)
        '    End If
        'Catch ex As Exception

        'End Try



        Return True
    End Function

    Private Function FUNC_RE_START_SERVER()

        Call FUNC_SERVER_FIN(False)
        Call FUNC_SERVER_STOP()

        Call FUNC_SERVER_START(1234, FRM_SERVER)
        Call FUNC_SERVER_INIT()

        Return True
    End Function

    Private Sub SUB_SERVER_THREAD()

        Dim TCC_CLIENT As System.Net.Sockets.TcpClient
        Dim STM_N_STREAM As System.Net.Sockets.NetworkStream

        Try
            TCC_CLIENT = TCL_SERVER_LISTNER.AcceptTcpClient()
        Catch ex As Exception
            Exit Sub
        End Try

        STM_N_STREAM = TCC_CLIENT.GetStream()

        SMR_SERVER_READER = New StreamReader(STM_N_STREAM, System.Text.Encoding.UTF8)
        SMR_SERVER_WRITER = New StreamWriter(STM_N_STREAM, System.Text.Encoding.UTF8)

        While True

            If Not ProcessMessage(SMR_SERVER_READER, SMR_SERVER_WRITER) Then
                Exit While
            End If

            If Not BLN_LISTENER_ENABLED Then
                Exit While
            End If
            'Try
            '    Call 
            'Catch ex As ThreadInterruptedException
            '    Exit Sub
            'Catch ex As Exception

            'End Try

        End While

        'Try
        '    Call SMR_SERVER_WRITER.Dispose()
        '    Call SMR_SERVER_READER.Dispose()
        'Catch ex As Exception

        'End Try
        'Call STM_N_STREAM.Close()
        'Call TCC_CLIENT.Close()

        'SMR_SERVER_READER = Nothing
        'SMR_SERVER_WRITER = Nothing
        'STM_N_STREAM = Nothing
        'TCC_CLIENT = Nothing

        If BLN_LISTENER_ENABLED Then
            Call FUNC_RE_START_SERVER()
        End If
    End Sub

    Private OBJ_THREAD_LOCK As New Object
    Private Function ProcessMessage(ByRef SMR_MESSAGE As System.IO.StreamReader, ByRef SMR_W As System.IO.StreamWriter) As Boolean

        SyncLock OBJ_THREAD_LOCK
            If SMR_MESSAGE Is Nothing Then
                Return False
            End If

            Dim STR_MESSAGE As String
            Try
                STR_MESSAGE = SMR_MESSAGE.ReadLine()
            Catch ex As Exception
                Return False
            End Try

            If STR_MESSAGE Is Nothing Then
                Return False
            End If

            Call SMR_W.WriteLine("aaa")
            Call SMR_W.Flush()
            Call FRM_SERVER.SUB_RECEIVE_MESSAGE(STR_MESSAGE)
        End SyncLock

        Return True
    End Function

    Public Sub SUB_CLIENT_INIT_VALUE(ByVal STR_IP As String, ByVal INT_NUMBER_PORT As Integer)
        STR_CONNECT_IP = STR_IP
        INT_CONNECT_NUMBER_PORT = INT_NUMBER_PORT
    End Sub

    Public Function FUNC_CLIENT_INIT() As Boolean

        Try
            TCC_CLIENT_SESSION = New TcpClient()
            Call TCC_CLIENT_SESSION.Connect(STR_CONNECT_IP, INT_CONNECT_NUMBER_PORT)
        Catch ex As Exception
            STR_MODULE_LAST_ERROR = ex.Message
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
                'Call SMR_CLIENT_WRITER.Close()
                'Call SMR_CLIENT_WRITER.Dispose()
                SMR_CLIENT_WRITER = Nothing
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
            Call Console.WriteLine(STR_SEND)
            Call SMR_CLIENT_WRITER.Flush()

            Dim STR_TEMP As String
            STR_TEMP = SMR_CLIENT_READER.ReadLine()
            Call Console.WriteLine(STR_TEMP)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
End Module
