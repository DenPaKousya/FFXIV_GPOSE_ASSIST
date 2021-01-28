Module MOD_CAST_TOOL

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

End Module
