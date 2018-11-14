using UnityEngine;
using UnityEngine.UI;

namespace MenuManagement
{
    public abstract class HighlightEffect : ScriptableObject
    {
        public abstract void Highlight(MenuSelection selection);
        public abstract void Highlight(Text text, Button button = null);
    }
}