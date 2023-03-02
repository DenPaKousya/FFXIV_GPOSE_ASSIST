Imports System.Windows

Public Module MOD_COMBO_TOOL

    Public Sub SUB_SET_COMBO_KIND_CODE_FIRST(ByRef CMB_SET As Controls.ComboBox)

        With CMB_SET

            If .Items.Count <= 0 Then
                Exit Sub
            End If

            If .SelectedIndex = 0 Then
                Exit Sub
            End If

            .SelectedIndex = -1

            .SelectedIndex = 0

        End With

    End Sub
End Module
