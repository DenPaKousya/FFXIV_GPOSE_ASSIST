Module MOD_CAST_TOOL

#Region "文字列編集関連"
	'特定の文字列を任意の文字列で囲む
	Public Function FUNC_ADD_ENCLOSED(
	ByVal strBASE As String, ByVal strENCLOSE As String
	) As String
		Dim strRET As String

		strRET = strENCLOSE & strBASE & strENCLOSE

		Return strRET
	End Function

	'FUNC_ADD_ENCLOSEDの[']ラップ
	'SQLサーバーでのクエリ実行時に文字列を扱う場合等に使用します
	Public Function FUNC_ADD_ENCLOSED_SCOT(
	ByVal strBASE As String
	) As String
		Dim strRET As String

		strRET = FUNC_ADD_ENCLOSED(strBASE, "'")

		Return strRET
	End Function

	'任意の文字で囲まれている文字列から囲み文字を外す
	'例："'2008/01/01'","'"→"2008/01/01"
	Public Function FUNC_ADD_ENCLOSED_REMOVE(
	ByVal strBASE As String, ByVal strENCLOSE As String,
	Optional ByVal blnRTRIM_SPACE As Boolean = True
	) As String
		Dim strRET As String
		Dim strTEMP As String
		Dim strTEMP_LEFT As String
		Dim strTEMP_RIGHT As String

		strTEMP = strBASE

		If strTEMP.Length < strENCLOSE.Length Then
			Return strTEMP
		End If
		strTEMP_LEFT = strTEMP.Substring(0, strENCLOSE.Length) '左端を取得
		If strTEMP_LEFT = strENCLOSE Then '囲み文字列と同様なら
			strTEMP = strTEMP.Substring(strTEMP_LEFT.Length, strTEMP.Length - strTEMP_LEFT.Length) '左端を除く
		End If

		If strTEMP.Length < strENCLOSE.Length Then
			Return strTEMP
		End If
		strTEMP_RIGHT = strTEMP.Substring(strTEMP.Length - strENCLOSE.Length, strENCLOSE.Length) '右端を取得
		If strTEMP_RIGHT = strENCLOSE Then '囲み文字列と同様なら
			strTEMP = strTEMP.Substring(0, strTEMP.Length - strTEMP_RIGHT.Length) '右端を除く
		End If

		If blnRTRIM_SPACE Then
			strTEMP = strTEMP.TrimEnd(" ")
		End If
		strRET = strTEMP
		Return strRET
	End Function

	'文字の置換(置換前文字列が空の場合文字列は置換されずに返ります)
	'例："2008/04/01","/",""→"20080401"
	Public Function FUNC_REPRACE_STRING(
	ByVal strBASE As String,
	ByVal strREPRACE_FROM As String, ByVal strREPRACE_TO As String
	) As String
		Dim strRET As String

		If strREPRACE_FROM = "" Then
			Return strBASE
		End If

		strRET = strBASE.Replace(strREPRACE_FROM, strREPRACE_TO)

		Return strRET
	End Function

	'左端特定文字のトリミング
	'例："000012345678901","0"→"12345678901"・"    山田 太郎"," "→"山田 太郎"
	Public Function FUNC_LTRIM_CHAR(
	ByVal strBASE As String,
	ByVal chrTRIM As Char
	) As String
		Dim chrONE As String
		Dim intLOOP_INDEX As Integer
		Dim intTOP_INDEX As Integer
		Dim strRET As String

		strRET = ""
		For intLOOP_INDEX = 0 To strBASE.Length - 1
			chrONE = strBASE.Substring(intLOOP_INDEX, 1)
			If chrONE <> chrTRIM Then
				intTOP_INDEX = intLOOP_INDEX
				Exit For
			End If
		Next

		strRET = strBASE.Substring(intTOP_INDEX, (strBASE.Length - intTOP_INDEX))

		Return strRET
	End Function

	'左端特定文字列の削除
	Public Function FUNC_L_REMOVE_STRING(
	ByVal strBASE As String,
	ByVal strREMOVE As String
	) As String
		Dim strTEMP As String
		Dim strRET As String

		strRET = strBASE

		If strBASE.Length < strREMOVE.Length Then
			Return strRET
		End If

		strTEMP = strBASE.Substring(0, strREMOVE.Length)
		If strTEMP = strREMOVE Then
			strRET = strBASE.Substring(strREMOVE.Length, strBASE.Length - strREMOVE.Length)
		End If

		Return strRET
	End Function

	'右端特定文字列の削除
	Public Function FUNC_R_REMOVE_STRING(
	ByVal strBASE As String,
	ByVal strREMOVE As String
	) As String
		Dim strTEMP As String
		Dim strRET As String

		strRET = strBASE

		If strBASE.Length < strREMOVE.Length Then
			Return strRET
		End If

		strTEMP = strBASE.Substring(strBASE.Length - strREMOVE.Length, strREMOVE.Length)
		If strTEMP = strREMOVE Then
			strRET = strBASE.Substring(0, strBASE.Length - strREMOVE.Length)
		End If

		Return strRET
	End Function

	'文字の切断(バイト数指定)
	'例："あいうえおかきくけこ",10→"あいうえお"
	Public Function FUNC_CUT_STRING_BYTE(
	ByVal strBASE As String, ByVal intBYTE As Integer
	) As String
		Dim strRET As String
		Dim strTEMP As String
		Dim strONE As String
		Dim intBYTE_CUR As Integer
		Dim intLOOP_INDEX As Integer

		strRET = ""
		For intLOOP_INDEX = 0 To strBASE.Length - 1
			strONE = strBASE.Substring(intLOOP_INDEX, 1)
			strTEMP = strRET & strONE
			intBYTE_CUR = System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(strTEMP)
			If intBYTE_CUR > intBYTE Then
				Exit For
			End If
			strRET = strTEMP
		Next

		Return strRET
	End Function

	'文字のチェック(Shift_JISでおk)
	Public Function FUNC_CHECK_SHIFT_JIS(
	ByVal strBASE As String
	) As Boolean
		Dim bytUNI As Byte()
		Dim bytSJIS As Byte()
		Dim chrSJIS As Char()
		Dim strCHECK As String
		Dim blnRET As Boolean

		bytUNI = System.Text.Encoding.GetEncoding("unicode").GetBytes(strBASE) 'UNICODEのバイト配列を取得

		bytSJIS = System.Text.Encoding.Convert(
		System.Text.Encoding.GetEncoding("unicode"), System.Text.Encoding.GetEncoding("shift_jis"),
		bytUNI
		) 'バイト配列をShift_JISへマッピング変換

		chrSJIS = System.Text.Encoding.GetEncoding("shift_jis").GetChars(bytSJIS) 'バイト配列を文字配列に変換

		strCHECK = FUNC_CONVERT_CHAR_ROW_TO_STRING(chrSJIS) '文字配列から文字列を取得

		blnRET = (strBASE = strCHECK) '元文字列と一致すれば化けない

		Return blnRET
	End Function

	'文字配列を文字列型へ変換
	Public Function FUNC_CONVERT_CHAR_ROW_TO_STRING(
	ByVal chrROW() As Char
	) As String
		Dim intLOOP_INDEX As Integer
		Dim strRET As String

		If IsNothing(chrROW) Then
			Return ""
		End If

		strRET = ""
		For intLOOP_INDEX = LBound(chrROW) To UBound(chrROW)
			strRET &= chrROW(intLOOP_INDEX)
		Next

		Return strRET
	End Function

	'文字列配列を文字列型へ変換
	Public Function FUNC_CONVERT_STRING_ROW_TO_STRING(
	ByVal strROW() As String,
	Optional ByVal strSEP As String = ","
	) As String
		Dim intLOOP_INDEX As Integer
		Dim strRET As String

		If IsNothing(strROW) Then
			Return ""
		End If

		strRET = ""
		For intLOOP_INDEX = LBound(strROW) To UBound(strROW)
			strRET &= If(intLOOP_INDEX = LBound(strROW), "", strSEP) & strROW(intLOOP_INDEX)
		Next

		Return strRET
	End Function

	'文字列を任意の区切文字で切断し任意のインデックスの文字を返す
	Public Function FUNC_SPLIT_STR(
	ByVal strBASE As String, ByVal intINDEX As Integer,
	Optional ByVal chrSPLIT_CHAR As Char = ","
	) As String
		Dim strROW() As String
		Dim strRET As String

		strROW = strBASE.Split(chrSPLIT_CHAR)

		If (strROW.Length - 1) < intINDEX Then
			Return ""
		End If

		Try

			strRET = strROW(intINDEX)
		Catch ex As Exception
			Return ""
		End Try

		Return strRET
	End Function

	'文字列を任意の区切文字で切断し数値配列として返す
	Public Function FUNC_SPLIT_STR_CONV_INT_ROW(
	ByVal strBASE As String,
	Optional ByVal chrSPLIT_CHAR As Char = ","
	) As Integer()
		Dim strROW() As String
		Dim intROW() As Integer
		Dim intLOOP_INDEX As Integer

		strROW = strBASE.Split(chrSPLIT_CHAR)

		ReDim intROW(-1)
		For intLOOP_INDEX = LBound(strROW) To UBound(strROW)
			ReDim Preserve intROW(intLOOP_INDEX)
			If IsNumeric(strROW(intLOOP_INDEX)) Then
				Try
					intROW(intLOOP_INDEX) = CInt(strROW(intLOOP_INDEX))
				Catch ex As Exception
					intROW(intLOOP_INDEX) = 0
				End Try
			Else
				intROW(intLOOP_INDEX) = 0
			End If
		Next

		Return intROW

	End Function

#End Region

#Region "特殊なキャスト"
	'INT型→BOOL型
	'例：1→TRUE,0→FALSE,999→FALSE
	Public Function FUNC_CAST_INT_TO_BOOL(
	ByVal intVALUE As Integer
	) As Boolean
		Dim blnRET As Boolean

		blnRET = (intVALUE = 1)

		Return blnRET
	End Function

	'BOOL型→INT型
	'例：TRUE→1,FALSE→0
	Public Function FUNC_CAST_BOOL_TO_INT(
	ByVal blnVALUE As Boolean
	) As Integer
		Dim intRET As Integer

		intRET = If(blnVALUE, 1, 0)

		Return intRET
	End Function

	'STR型→BOOL型
	'例：Y→TRUE,N→FALSE,Z→FALSE
	Public Function FUNC_CAST_STR_TO_BOOL(
	ByVal chrVALUE As Char
	) As Boolean
		Select Case chrVALUE
			Case "Y"
				Return True
			Case "N"
				Return False
			Case Else
				Return False
		End Select
	End Function

	'BOOL型→STR型(チェック文字列表記用)
	'例：TRUE→"レ",FALSE→""
	Public Function FUNC_CAST_BOOL_TO_STR_CHECK(
	ByVal blnVALUE As Boolean
	) As String
		Dim strRET As String

		strRET = If(blnVALUE, "レ", "")

		Return strRET
	End Function

	'BOOL型→STR型(可否文字列表記用)
	'例：TRUE→"○",FALSE→"×"
	Public Function FUNC_CAST_BOOL_TO_STR_PROPRIETY(
	ByVal blnVALUE As Boolean
	) As String
		Dim strRET As String

		strRET = If(blnVALUE, "○", "×")

		Return strRET
	End Function

#End Region

#Region "強制キャスト"

	'数値型項目への強制型変換(認識できない場合第二引数として返す)
	'例："00001"→1.0,"99.560"→99.56,"abcde"→-1.0
	Public Function FUNC_VALUE_CONVERT_NUMERIC(
	ByVal objBASE_VALUE As Object,
	Optional ByVal dblDEFFAULT_NUM As Double = -1
	) As Double
		Dim dblRET As Double
		Dim strTEMP As String
		Const cstMAX_NUMERIC As Integer = 20 '数値として扱う最大桁数

		If IsNothing(objBASE_VALUE) Then '非参照なら
			Return dblDEFFAULT_NUM
		End If

		If IsDBNull(objBASE_VALUE) Then 'NULL値なら
			Return dblDEFFAULT_NUM
		End If

		strTEMP = CStr(objBASE_VALUE) '  一旦文字列としては確定

		If Not IsNumeric(strTEMP) Then '非数値なら
			Return dblDEFFAULT_NUM
		End If

		strTEMP = FUNC_LTRIM_CHAR(strTEMP, "0") '左端に「0」があれば省く
		strTEMP = FUNC_LTRIM_CHAR(strTEMP, "０") '左端に「０」があれば省く

		If strTEMP.Length >= cstMAX_NUMERIC Then '最大桁数を超えていた場合も数値として扱わない
			Return dblDEFFAULT_NUM
		End If

		dblRET = CDbl(strTEMP)

		Return dblRET
	End Function

	'文字列項目への強制変換(認識できない場合第二引数として返す)
	'例："001"→"001","abcde"→"abcde",NULL→""
	Public Function FUNC_VALUE_CONVERT_STRING(
	 ByVal objBASE_VALUE As Object,
	 Optional ByVal dblDEFFAULT_STRING As String = ""
	 ) As String
		Dim strRET As String

		If IsNothing(objBASE_VALUE) Then '非参照なら
			Return dblDEFFAULT_STRING
		End If

		If IsDBNull(objBASE_VALUE) Then 'NULL値なら
			Return dblDEFFAULT_STRING
		End If

		strRET = CStr(objBASE_VALUE)

		Return strRET
	End Function

	'数値型項目への強制型変換(認識できない場合第二引数として返す)-Integer用のラッピング
	Public Function FUNC_VALUE_CONVERT_NUMERIC_INT(
	ByVal objBASE_VALUE As Object,
	Optional ByVal intDEFFAULT_NUM As Integer = -1
	) As Integer
		Dim intRET As Integer
		Dim lngTEMP As Long

		lngTEMP = FUNC_VALUE_CONVERT_NUMERIC(objBASE_VALUE, intDEFFAULT_NUM)
		If lngTEMP > Integer.MaxValue Then
			Return intDEFFAULT_NUM
		End If

		If lngTEMP < Integer.MinValue Then
			Return intDEFFAULT_NUM
		End If

		intRET = CInt(lngTEMP)

		Return intRET
	End Function

	'数値型項目への強制型変換(認識できない場合第二引数として返す)-Long用のラッピング
	Public Function FUNC_VALUE_CONVERT_NUMERIC_LONG(
	ByVal objBASE_VALUE As Object,
	Optional ByVal intDEFFAULT_NUM As Integer = -1
	) As Long
		Dim lngRET As Long
		Dim lngTEMP As Long

		lngTEMP = FUNC_VALUE_CONVERT_NUMERIC(objBASE_VALUE, intDEFFAULT_NUM)
		If lngTEMP > Long.MaxValue Then
			Return intDEFFAULT_NUM
		End If

		If lngTEMP < Long.MinValue Then
			Return intDEFFAULT_NUM
		End If

		lngRET = CLng(lngTEMP)

		Return lngRET
	End Function

#End Region

End Module
