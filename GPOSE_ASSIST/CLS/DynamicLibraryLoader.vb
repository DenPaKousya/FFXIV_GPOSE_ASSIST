Imports System
Imports System.Runtime.InteropServices

Public Class DynamicLibraryLoader
    <DllImport("kernel32", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function LoadLibrary(ByVal lpFileName As String) As IntPtr
    End Function

    <DllImport("kernel32", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
    End Function

    <DllImport("kernel32", CharSet:=CharSet.Ansi, SetLastError:=True)>
    Private Shared Function GetProcAddress(ByVal hModule As IntPtr, ByVal lpProcName As String) As IntPtr
    End Function

    Private _libraryModule As IntPtr = IntPtr.Zero

    Private _lastErrorNo As Integer = 0
    Private _errorMessage As String = String.Empty

    Public ReadOnly Property LastErrorNo() As Integer
        Get
            Return _lastErrorNo
        End Get
    End Property

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return _errorMessage
        End Get
    End Property

    Public Function Load(ByVal fileName As String) As Boolean

        _libraryModule = LoadLibrary(fileName)
        If _libraryModule = IntPtr.Zero Then

            Dim lastError As Integer = Marshal.GetHRForLastWin32Error
            Dim ex As Exception = Marshal.GetExceptionForHR(lastError)

            _lastErrorNo = lastError
            _errorMessage = ex.Message

            Return False

        End If

        Return True

    End Function

    Public Function GetDelegate(ByVal procName As String, ByVal delegateType As Type) As [Delegate]

        If _libraryModule = IntPtr.Zero Then
            Return Nothing
        End If

        Dim procPtr As IntPtr = GetProcAddress(_libraryModule, procName)
        If procPtr = IntPtr.Zero Then

            Dim lastError As Integer = Marshal.GetHRForLastWin32Error
            Dim ex As Exception = Marshal.GetExceptionForHR(lastError)

            _lastErrorNo = lastError
            _errorMessage = ex.Message

            Return Nothing

        End If

        Dim d As [Delegate] = Marshal.GetDelegateForFunctionPointer(procPtr, delegateType)
        Return d

    End Function

    Public Function Free() As Boolean

        If _libraryModule <> IntPtr.Zero Then

            Dim result As Boolean = FreeLibrary(_libraryModule)
            If result = False Then

                Dim lastError As Integer = Marshal.GetHRForLastWin32Error
                Dim ex As Exception = Marshal.GetExceptionForHR(lastError)

                _lastErrorNo = lastError
                _errorMessage = ex.Message

                Return False

            End If

        End If

        Return True

    End Function
End Class
