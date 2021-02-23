Public Module MOD_FILE_TOOL

#Region "モジュール用・変数"
    Public STR_FILE_TOOL_LAST_ERR_STRING As String
#End Region

    '該当のディレクトリで使用可能なファイル名称を取得
    Public Function FUNC_GET_FILE_NAME_SUB(ByVal strDIR As String, ByVal STR_FILE_NAME_BASE As String, ByRef STR_EXTENT_ROW() As String) As String

        If Not FUNC_FILE_CHECK_MULTI_EXTENT(strDIR & "\" & STR_FILE_NAME_BASE, STR_EXTENT_ROW) Then
            Return STR_FILE_NAME_BASE
        End If

        Dim STR_FILE_NAME_INDEX As String
        STR_FILE_NAME_INDEX = ""
        For i = 1 To 99
            STR_FILE_NAME_INDEX = STR_FILE_NAME_BASE & "_" & i.ToString("00")
            If Not FUNC_FILE_CHECK_MULTI_EXTENT(strDIR & "\" & STR_FILE_NAME_INDEX, STR_EXTENT_ROW) Then
                Exit For
            End If
        Next

        Dim STR_RET As String
        STR_RET = STR_FILE_NAME_INDEX
        Return STR_RET
    End Function


    'ファイルチェック処理(複数拡張子)
    Public Function FUNC_FILE_CHECK_MULTI_EXTENT(ByVal strFILE_PATH As String, ByRef STR_EXTENT_ROW() As String) As Boolean
        If STR_EXTENT_ROW Is Nothing Then
            Return False
        End If

        For i = 1 To (STR_EXTENT_ROW.Length - 1)
            If FUNC_FILE_CHECK(strFILE_PATH & STR_EXTENT_ROW(i)) Then
                Return True
            End If
        Next

        Return False
    End Function

    'ファイルチェック処理
    Public Function FUNC_FILE_CHECK(
    ByVal strFILE_PATH As String
    ) As Boolean
        Dim filBASE As System.IO.FileInfo
        Dim blnRET As Boolean

        If strFILE_PATH Is Nothing Then
            Return False
        End If

        If strFILE_PATH = "" Then
            Return False
        End If

        filBASE = New System.IO.FileInfo(strFILE_PATH)
        blnRET = filBASE.Exists

        Call GC.ReRegisterForFinalize(filBASE)
        filBASE = Nothing

        Return blnRET
    End Function


    'ディレクトリのチェック
    Public Function FUNC_DIR_CHECK(ByVal strPATH_DIR As String) As Boolean
        Dim dirCHECK As System.IO.DirectoryInfo
        Dim blnRET As Boolean

        STR_FILE_TOOL_LAST_ERR_STRING = ""

        Try
            dirCHECK = New System.IO.DirectoryInfo(strPATH_DIR)
        Catch ex As Exception
            dirCHECK = Nothing
            STR_FILE_TOOL_LAST_ERR_STRING = ex.Message
            Return True '無理やりあるものとして返して上位関数でのエラーを誘う
        End Try

        blnRET = dirCHECK.Exists
        dirCHECK = Nothing

        Return blnRET
    End Function

    '上位ディレクトリへ変換
    Public Function FUNC_DIR_CONVERT_ONE_TOP(ByVal strPATH_DIR As String) As String
        Dim strRET As String
        Dim intLEN As Integer
        Dim strCHECK As String
        Dim intLOOP_INDEX As Integer

        intLEN = strPATH_DIR.Length
        strRET = ""
        For intLOOP_INDEX = 1 To intLEN
            strCHECK = Mid(strPATH_DIR, intLEN - intLOOP_INDEX, 1)
            If strCHECK = "\" Then
                strRET = Mid(strPATH_DIR, 1, intLEN - intLOOP_INDEX)
                Exit For
            End If
        Next

        Return strRET
    End Function

    Public Function FUNC_DIR_MAKE(ByVal strPATH_DIR As String) As Boolean
        Dim dirMake As System.IO.DirectoryInfo
        Dim strPATH_DIR_TOP As String

        STR_FILE_TOOL_LAST_ERR_STRING = ""

        strPATH_DIR_TOP = FUNC_DIR_CONVERT_ONE_TOP(strPATH_DIR)

        If Not FUNC_DIR_CHECK(strPATH_DIR_TOP) Then
            If Not FUNC_DIR_MAKE(strPATH_DIR_TOP) Then
                Return False
            End If
        End If

        If FUNC_DIR_CHECK(strPATH_DIR) Then 'すでに存在するなら
            Return True 'そのまま
        End If

        Try
            dirMake = New System.IO.DirectoryInfo(strPATH_DIR)
        Catch ex As Exception
            dirMake = Nothing
            STR_FILE_TOOL_LAST_ERR_STRING = ex.Message
            Return False
        End Try

        Try
            Call dirMake.Create() '作成
            dirMake = Nothing
        Catch ex As Exception
            dirMake = Nothing
            STR_FILE_TOOL_LAST_ERR_STRING = ex.Message
            Return False
        End Try

        Return True
    End Function


    'パスからディレクトリパスを抜き出す
    Public Function FUNC_PATH_TO_DIR_PATH(
    ByVal STR_PATH As String
    ) As String
        Dim INT_LEN As Integer
        INT_LEN = Len(STR_PATH)

        Dim STR_RET As String
        STR_RET = ""
        For i = 1 To INT_LEN
            Dim STR_CHECK As String
            STR_CHECK = Mid(STR_PATH, INT_LEN - i, 1)
            If STR_CHECK = "\" Then
                STR_RET = Mid(STR_PATH, 1, INT_LEN - i)
                Exit For
            End If
        Next

        Return STR_RET
    End Function


    'EXE実行処理(呼出用)
    Public Function FUNC_CALL_EXE_FILE_SHELL(
    ByVal strEXE_PATH As String, ByVal strCOMMAND_LINE As String,
    Optional ByVal blnCHECK_FILE As Boolean = True
    ) As Boolean
        Dim intACCESSID As Integer

        STR_FILE_TOOL_LAST_ERR_STRING = ""

        If blnCHECK_FILE Then
            If Not FUNC_FILE_CHECK(strEXE_PATH) Then 'チェックを行う
                STR_FILE_TOOL_LAST_ERR_STRING = strEXE_PATH & Environment.NewLine & "ファイルがありません"
                Return False
            End If
        End If

        intACCESSID = FUNC_EXE_FILE_SHELL(strEXE_PATH, strCOMMAND_LINE) '実呼出
        If intACCESSID = -1 Then
            Return False
        End If

        If intACCESSID > 0 Then
            Call System.Windows.Forms.Application.DoEvents()

            Call FUNC_APP_ACTIVE(intACCESSID) '画面をアクティブにする

        End If

        Return True
    End Function

    'EXE実行処理
    Private Function FUNC_EXE_FILE_SHELL(
    ByVal strEXE_PATH As String, ByVal strCOMMAND_LINE As String
    ) As Integer
        Dim strPATH As String
        Dim intRET As Integer
        Dim strEXE_PATH_ABB As String

        Dim psiSET As System.Diagnostics.ProcessStartInfo
        STR_FILE_TOOL_LAST_ERR_STRING = ""

        psiSET = New System.Diagnostics.ProcessStartInfo

        strEXE_PATH_ABB = FUNC_GET_ABB_PATH(strEXE_PATH)

        With psiSET
            .FileName = strEXE_PATH_ABB
            .Arguments = strCOMMAND_LINE
            .WorkingDirectory = FUNC_PATH_TO_DIR_PATH(strEXE_PATH_ABB)
        End With

        strPATH = strEXE_PATH & " " & strCOMMAND_LINE
        Try
            ' パラメータを指定して実行
            'Process.Start(strEXE_PATH, strCOMMAND_LINE)
            Process.Start(psiSET)
        Catch ex As Exception
            intRET = -1
            STR_FILE_TOOL_LAST_ERR_STRING = ex.Message
        End Try

        Return intRET
    End Function

    '他のEXEの画面をアクティブにする(繰返あり)
    Public Function FUNC_APP_ACTIVE(
    ByVal intACCESS_ID As Integer,
    Optional ByVal intNUMBER_OF_TIMES As Integer = 1
    ) As Boolean
        Dim blnRET As Boolean
        Dim intLOOP_INDEX As Integer

        blnRET = False
        For intLOOP_INDEX = 1 To intNUMBER_OF_TIMES
            If FUNC_APP_ACTIVE_MAIN(intACCESS_ID) Then
                blnRET = True
                Exit For
            End If
            Call System.Windows.Forms.Application.DoEvents()
        Next

        Return blnRET
    End Function

    '他のEXEの画面をアクティブにする
    Private Function FUNC_APP_ACTIVE_MAIN(
    ByVal intACCESS_ID As Integer
    ) As Boolean
        Try
            Call Microsoft.VisualBasic.Interaction.AppActivate(intACCESS_ID)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    'パスから絶対パスを取得する
    Public Function FUNC_GET_ABB_PATH(ByVal strFILE_PATH As String) As String
        Dim strRET As String

        Try
            strRET = System.IO.Path.GetFullPath(strFILE_PATH)
        Catch ex As Exception
            Return ""
        End Try

        Return strRET
    End Function

End Module
