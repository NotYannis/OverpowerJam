using UnityEngine;
using InControl;

namespace MenuManagement
{
    [RequireComponent(typeof(Canvas))]
    public class MenuManager : MonoBehaviour
    {
        private static MenuManager _instance;
        public static MenuManager Instance
        {
            get
            {
                if (!(_instance is MenuManager))
                {
                    GameObject gO;
                    _instance = FindObjectOfType<MenuManager>();

                    if (!(_instance is MenuManager))
                    {
                        gO = Instantiate(Resources.Load("Prefab/UI/MenuManager") as GameObject);
                        _instance = gO.GetComponent<MenuManager>();
                    }
                    else
                    {
                        gO = _instance.gameObject;
                    }
                    DontDestroyOnLoad(gO);
                }
                return _instance;
            }
        }

        private Menu _currentMenu;
        public Menu currentMenu
        {
            get
            {
                return _currentMenu;
            }
            set
            {
                if (_currentMenu != value)
                {
                    if (value.deactivatePrev)
                    {
                        _currentMenu.gameObject.SetActive(false);
                    }

                    prevMenu = _currentMenu;
                    _currentMenu = value;
                    _currentMenu.gameObject.SetActive(true);
                }
            }
        }

        Menu prevMenu;

        public InputDevice playerInChargeController;
        public Menu emptyCanvas;
        public Menu pauseMenu;
        public Menu startMenu;

        void Awake()
        {
            if (_instance is MenuManager)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = Instance;
                if (startMenu is Menu)
                {
                    _currentMenu = startMenu;
                }
                else
                {
                    _currentMenu = emptyCanvas;
                }
            }
        }

        private void OnDestroy()
        {
            _instance = null;
        }

        void Update()
        {
            if (currentMenu == emptyCanvas)
            {
                return;
            }
            else
            {
                currentMenu.UpdateMenu();
            }
        }
    }
}