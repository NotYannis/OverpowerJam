using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    //Class for storing a Text component variables without a component and allowing those variables to be reused later
    public class TextProperties
    {
        bool alignByGeometry;
        TextAnchor alignment;
        Font font;
        int fontSize;
        FontStyle fontStyle;
        HorizontalWrapMode horizontalOverflow;
        float lineSpacing;
        string text;
        VerticalWrapMode verticalOverflow;
        bool enabled;
        Color color;
        bool maskable;
        Material material;

        //RectTransform Variables
        Vector2 anchorMin;
        Vector2 anchorMax;
        Vector2 anchoredPosition;
        Vector2 sizeDelta;

        public TextProperties(Text textComponent)
        {
            alignByGeometry = textComponent.alignByGeometry;
            alignment = textComponent.alignment;
            font = textComponent.font;
            fontSize = textComponent.fontSize;
            fontStyle = textComponent.fontStyle;
            horizontalOverflow = textComponent.horizontalOverflow;
            lineSpacing = textComponent.lineSpacing;
            text = textComponent.text;
            verticalOverflow = textComponent.verticalOverflow;
            enabled = textComponent.enabled;
            color = textComponent.color;
            material = textComponent.material;
            maskable = textComponent.maskable;

            anchorMin = textComponent.rectTransform.anchorMin;
            anchorMax = textComponent.rectTransform.anchorMax;
            anchoredPosition = textComponent.rectTransform.anchoredPosition;
            sizeDelta = textComponent.rectTransform.sizeDelta;
        }

        public void CopyToTextComponent(Text textComponent)
        {

            textComponent.alignByGeometry = alignByGeometry;
            textComponent.alignment = alignment;
            textComponent.font = font;
            textComponent.fontSize = fontSize;
            textComponent.fontStyle = fontStyle;
            textComponent.horizontalOverflow = horizontalOverflow;
            textComponent.lineSpacing = lineSpacing;
            textComponent.text = text;
            textComponent.verticalOverflow = verticalOverflow;
            textComponent.enabled = enabled;
            textComponent.color = color;
            textComponent.material = material;
            textComponent.maskable = maskable;

            textComponent.rectTransform.anchorMin = anchorMin;
            textComponent.rectTransform.anchorMax = anchorMax;
            textComponent.rectTransform.anchoredPosition = anchoredPosition;
            textComponent.rectTransform.sizeDelta = sizeDelta;
        }
    }
}