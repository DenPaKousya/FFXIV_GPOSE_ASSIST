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

End Module
