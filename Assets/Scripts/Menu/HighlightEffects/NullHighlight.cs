using UnityEngine;
using MenuManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Null_Highlight", menuName = "Highlight Effect/Null")]
public class NullHighlight : HighlightEffect
{
    public override void Highlight(MenuSelection selection)
    {
        return;
    }

    public override void Highlight(Text text, Button button = null)
    {
        return;
    }
}