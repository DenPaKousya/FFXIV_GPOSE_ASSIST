Imports System.Windows

Public Module MOD_COMBO_ITEM_TOOL
    Public Sub SUB_ADD_ITEMS_COMPOTION_TYPE_ONLY(ByRef ITM_ADD As Controls.ItemCollection)
        With ITM_ADD
            Call .Add("2分割")
            Call .Add("3分割")
            Call .Add("4分割")
            Call .Add("3分割(φ)")
            Call .Add("X分割")
            Call .Add("対角-左下起点")
            Call .Add("対角-左上起点")
            Call .Add("V字")
            Call .Add("V字(逆)")
            Call .Add(">字")
            Call .Add(">字(逆)")
            Call .Add("調和三角形-横-左下起点")
            Call .Add("調和三角形-横-左上起点")
            Call .Add("調和三角形-縦-左下起点")
            Call .Add("調和三角形-縦-左上起点")
            Call .Add("黄金螺旋-横-左下起点")
            Call .Add("黄金螺旋-横-右上起点")
            Call .Add("黄金螺旋-横-左上起点")
            Call .Add("黄金螺旋-横-右下起点")
            Call .Add("黄金螺旋-縦-左下起点")
            Call .Add("黄金螺旋-縦-右上起点")
            Call .Add("黄金螺旋-縦-左上起点")
            Call .Add("黄金螺旋-縦-右下起点")
        End With
    End Sub

    Public Sub SUB_ADD_ITEMS_COMPOTION_TYPE_USER(ByRef ITM_ADD As Controls.ItemCollection, ByRef STR_ITEM() As String)

        If IsNothing(STR_ITEM) Then
            Exit Sub
        End If

        Dim INT_INDEX As Integer
        INT_INDEX = (STR_ITEM.Length - 1)
        For i = 1 To INT_INDEX
            With ITM_ADD
                Call .Add(STR_ITEM(i))
            End With
        Next
    End Sub

End Module
