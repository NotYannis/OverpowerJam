using UnityEngine;
using MenuManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TextColorAndSize_Highlight", menuName = "Highlight Effect/Text/Color And Size Highlight")]
public class TextColorAndSizeHighlight : HighlightEffect 
{
    [SerializeField]
#pragma warning disable 649
	Color color;
    [SerializeField]
    int maxSize;
#pragma warning restore 649

	public override void Highlight(MenuSelection selection)
    {
            selection.text.fontSize = maxSize;
            selection.text.color = color;
    }

    public override void Highlight(Text text, Button button = null)
    {
    }
}