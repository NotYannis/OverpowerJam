using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace MenuManagement
{
    public class MenuSelection : MonoBehaviour
    {
        public Text text;
        public Button button;

        TextProperties initialTextComponent;
        ButtonProperties initialButtonComponent;

        private void OnValidate()
        {
            text = GetComponentInChildren<Text>();
            button = GetComponentInChildren<Button>();

            if (text is Text)
            {
                initialTextComponent = new TextProperties(text);
            }

            if (button is Button)
            {
                initialButtonComponent = new ButtonProperties(button);
            }
        }

        private void Awake()
        {
            if (!(text is Text))
            {
                text = GetComponentInChildren<Text>();

                if (text is Text)
                {
                    initialTextComponent = new TextProperties(text);
                }
            }

            if (!(button is Button))
            {
                button = GetComponentInChildren<Button>();

                if (button is Button)
                {
                    initialButtonComponent = new ButtonProperties(button);
                }
            }
        }

        public void ReturnToInitialState()
        {
            if (initialTextComponent is TextProperties)
            {
                initialTextComponent.CopyToTextComponent(text);
            }

            if (initialButtonComponent is ButtonProperties)
            {
                initialButtonComponent.CopyToButtonComponent(button);
            }
        }
    }
}