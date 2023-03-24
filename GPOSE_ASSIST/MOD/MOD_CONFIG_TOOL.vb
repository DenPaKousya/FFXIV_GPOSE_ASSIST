Module MOD_CONFIG_TOOL

#Region "外部用・構造体"
    Public Structure SRT_APP_SETTINGS
        Public PROCESS_NAME As String
        Public SAVE As SRT_APP_SETTINGS_SAVE
        Public GUIDE As SRT_APP_SETTINGS_GUIDE
        Public TRIM As SRT_APP_SETTINGS_TRIM
        Public CAMERA As SRT_APP_SETTINGS_CAMERA
    End Structure

#Region "SAVE"
    Public Structure SRT_APP_SETTINGS_SAVE
        Public DIRECTORY As String
        Public FILE As SRT_APP_SETTINGS_SAVE_FILE
    End Structure

    Public Structure SRT_APP_SETTINGS_SAVE_FILE
        Public DIRECTORY As String
        Public NAME As String
        Public TYPE As String
        Public QUALITY As Integer
        Public COPYRIGHT As Integer
        Public INDEX As Integer
    End Structure

#End Region

#Region "GUIDE"
    Public Structure SRT_APP_SETTINGS_GUIDE
        Public LOCATION As SRT_APP_SETTINGS_GUIDE_LOCATION
    End Structure

    Public Structure SRT_APP_SETTINGS_GUIDE_LOCATION
        Public ALIGNMENT As String
    End Structure
#End Region

#Region "TRIM"
    Public Structure SRT_APP_SETTINGS_TRIM
        Public LOCATION As SRT_APP_SETTINGS_TRIM_LOCATION
        Public SIZE As SRT_APP_SETTINGS_TRIM_SIZE
        Public ASPECT_RATIO As SRT_APP_SETTINGS_TRIM_ASPECT_RATIO
        Public COMPOTION As SRT_APP_SETTINGS_TRIM_COMPOTION
    End Structure

    Public Structure SRT_APP_SETTINGS_TRIM_LOCATION
        Public LEFT As Integer
        Public TOP As Integer
    End Structure

    Public Structure SRT_APP_SETTINGS_TRIM_SIZE
        Public WIDTH As Integer
        Public HEIGHT As Integer
    End Structure

    Public Structure SRT_APP_SETTINGS_TRIM_ASPECT_RATIO
        Public TYPE As Integer
    End Structure

    Public Structure SRT_APP_SETTINGS_TRIM_COMPOTION
        Public TYPE As Integer
        Public USER() As SRT_APP_SETTINGS_TRIM_COMPOTION_USER
    End Structure

    Public Structure SRT_APP_SETTINGS_TRIM_COMPOTION_USER
        Public BASE As String
        Public NAME As String
        Public TYPE() As Integer
        Public Function FUNC_GET_BASE() As String
            Dim STR_RET As String
            STR_RET = ""
            STR_RET &= Me.NAME
            For i = 1 To (Me.TYPE.Length - 1)
                STR_RET &= "," & CStr(Me.TYPE(i))
            Next
            Return STR_RET
        End Function
    End Structure
#End Region

#Region "CAMERA"
    Public Structure SRT_APP_SETTINGS_CAMERA
        Public CONTROL As SRT_APP_SETTINGS_CAMERA_CONTROL

    End Structure

    Public Structure SRT_APP_SETTINGS_CAMERA_CONTROL
        Public WAIT_FOR_GAME_RESPONSE As Integer
        Public WASD_PUSH_WEIGHT As Integer
        Public ARROW_PUSH_WEIGHT As Integer
        Public PAGEUD_PUSH_WEIGHT As Integer
    End Structure
#End Region

#End Region

#Region "モジュール用・定数"
    Private Const cstCONFIG_EXTENT As String = "config"
    Private Const cstCONFIE_KEY_MAIN As String = "configuration"
    Private Const cstCONFIE_KEY_SUB As String = "appSettings"
    Private Const cstCONFIG_KEY_ATTRIBUTE_ADD As String = "add"
    Private Const cstCONFIG_KEY_PROPERTY_KEY As String = "key"
    Private Const cstCONFIG_KEY_PROPERTY_VALUE As String = "value"
#End Region

    Public Function FUNC_GET_APP_SETTINGS(ByVal STR_KEY As String) As String
        Dim OBJ_TEMP As Object
        Dim STR_RET As String

        Try
            OBJ_TEMP = System.Configuration.ConfigurationManager.AppSettings(STR_KEY)
        Catch ex As Exception
            Return ""
        End Try

        If OBJ_TEMP Is Nothing Then
            Return ""
        End If

        STR_RET = OBJ_TEMP.ToString

        Return STR_RET
    End Function

    Public Function FUNC_WRITE_APP_SETTINGS(ByVal strKEY_NAME As String, ByVal strVALUE As String)

        Dim STR_NAME_EXE As String
        STR_NAME_EXE = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath)

        Dim STR_NAME_APPL As String
        STR_NAME_APPL = STR_NAME_EXE.Replace(".exe", "")

        Dim STR_NAME_DLL As String
        STR_NAME_DLL = STR_NAME_APPL & ".dll"

        Dim ASM_EXEC As System.Reflection.Assembly
        ASM_EXEC = System.Reflection.Assembly.GetExecutingAssembly()

        Dim STR_PATH_CONFIG_APP As String
        STR_PATH_CONFIG_APP = System.IO.Path.GetDirectoryName(ASM_EXEC.Location) & "\" & STR_NAME_DLL & "." & cstCONFIG_EXTENT

        Dim doc As System.Xml.XmlDocument
        doc = New System.Xml.XmlDocument

        Try
            Call doc.Load(STR_PATH_CONFIG_APP)
        Catch ex As Exception
            doc = Nothing
            Return False
        End Try

        Dim node As System.Xml.XmlNode
        node = doc(cstCONFIE_KEY_MAIN)(cstCONFIE_KEY_SUB)

        Dim blnMOD As Boolean
        blnMOD = False
        For Each n In doc(cstCONFIE_KEY_MAIN)(cstCONFIE_KEY_SUB)
            If n.Name = cstCONFIG_KEY_ATTRIBUTE_ADD Then
                If n.Attributes.GetNamedItem(cstCONFIG_KEY_PROPERTY_KEY).Value = strKEY_NAME Then
                    n.Attributes.GetNamedItem(cstCONFIG_KEY_PROPERTY_VALUE).Value = strVALUE
                    blnMOD = True
                End If
            End If
        Next

        If Not blnMOD Then
            Dim newNode As System.Xml.XmlElement
            newNode = doc.CreateElement(cstCONFIG_KEY_ATTRIBUTE_ADD)
            newNode.SetAttribute(cstCONFIG_KEY_PROPERTY_KEY, strKEY_NAME)
            newNode.SetAttribute(cstCONFIG_KEY_PROPERTY_VALUE, strVALUE)
            node.AppendChild(newNode)
        End If

        doc.Save(STR_PATH_CONFIG_APP)
        doc = Nothing

        Call System.Configuration.ConfigurationManager.RefreshSection(cstCONFIE_KEY_SUB)

        Return True
    End Function

End Module
