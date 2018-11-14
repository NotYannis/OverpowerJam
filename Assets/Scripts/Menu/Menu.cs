using UnityEngine;
namespace MenuManagement
{
    public class Menu : MonoBehaviour
    {
        MenuSelection activeSelection;

        MenuSelection[] menuItems;
        public bool deactivatePrev;
        int _selectionIndex = 0;

        [SerializeField]
        public HighlightEffect highlight;

        public int selectionIndex
        {
            get
            {
                return _selectionIndex;
            }
            set
            {
                if (value < 0)
                {
                    value = menuItems.Length - 1;
                }

                value = value % menuItems.Length;
                _selectionIndex = value;

                if (activeSelection is MenuSelection)
                {
                    activeSelection.ReturnToInitialState();
                }

                activeSelection = menuItems[_selectionIndex];
                highlight.Highlight(activeSelection);
            }
        }

        private void OnValidate()
        {
            if (!(menuItems is MenuSelection[]))
            {
                menuItems = GetComponentsInChildren<MenuSelection>();
            }
        }

        private void OnEnable()
        {
            if (!(menuItems is MenuSelection[]))
            {
                menuItems = GetComponentsInChildren<MenuSelection>();
            }

            selectionIndex = 0;
        }

        private void OnDisable()
        {
            activeSelection.ReturnToInitialState();
        }

        public void UpdateMenu()
        {
            highlight.Highlight(activeSelection);
        }

        public void Select()
        {
            activeSelection.button.onClick.Invoke();
        }

        public void ResetActiveSelection()
        {
            activeSelection.ReturnToInitialState();
        }
    }
}