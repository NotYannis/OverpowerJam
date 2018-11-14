using UnityEngine;
using MenuManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TextAlphaChange_Highlight", menuName = "Highlight Effect/Text/Alpha Change")]
public class TextFadeInOutAlphaChangeHighlight : HighlightEffect
{
    [SerializeField]
#pragma warning disable 649
	float fadeSpeed;
    [SerializeField]
    [Range(0.0f,1.0f)]
	float min;
    [SerializeField]
    [Range(0.0f, 1.0f)]
	float max;
#pragma warning restore 649

	public override void Highlight(MenuSelection selection)
    {
        Color color = selection.text.color;

        color.a = Mathf.Clamp(Mathf.Abs(Mathf.Sin(Time.time * fadeSpeed)), min, max);
        selection.text.color = color;
    }

    public override void Highlight(Text text, Button button = null)
    {
    }
}
