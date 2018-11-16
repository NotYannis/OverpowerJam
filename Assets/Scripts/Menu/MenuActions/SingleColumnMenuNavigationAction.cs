using UnityEngine;
using StateControllerManagement;
using InControl;
using MenuManagement;

[CreateAssetMenu(fileName = "MenuNavigation_Action", menuName = "Actions/Menu/Navigation")]
public class SingleColumnMenuNavigationAction : Action
{
#pragma warning disable 649
	[SerializeField]
    InputControlType upButton;
    [SerializeField]
	InputControlType downButton;
#pragma warning restore 649
	bool upButtonPressed = false;
    bool downButtonPressed = false;
    bool selectionButtonPressed = false;

    private void OnEnable()
    {
        upButtonPressed = false;
        downButtonPressed = false;
        selectionButtonPressed = false;
    }

    public override void Act<T>(T controller)
    {
        NavigateMenu();
    }

    private void NavigateMenu()
    {
        InputDevice relevantDevice = MenuManager.Instance.playerInChargeController;

        if (relevantDevice.GetControl(upButton) || relevantDevice.LeftStickY > 0.5f)
        {
            if (!upButtonPressed)
            {
                MoveUp();
            }
            upButtonPressed = true;
        }
        else
        {
            upButtonPressed = false;
        }

        if (relevantDevice.GetControl(downButton) || relevantDevice.LeftStickY < -0.5f)
        {
            if (!downButtonPressed)
            {
                MoveDown();
            }
            downButtonPressed = true;
        }
        else
        {
            downButtonPressed = false;
        }

        if (relevantDevice.Action1 && !Input.GetMouseButton(0))
        {
            if (!selectionButtonPressed)
            {
                MenuManager.Instance.currentMenu.Select();
            }
            selectionButtonPressed = true;
        }
        else
        {
            selectionButtonPressed = false;
        }
    }
    
    public void MoveUp()
    {
        MenuManager.Instance.currentMenu.selectionIndex--;
    }

    public void MoveDown()
    {
        MenuManager.Instance.currentMenu.selectionIndex++;
    }
}