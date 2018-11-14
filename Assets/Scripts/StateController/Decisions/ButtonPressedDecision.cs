using UnityEngine;
using InControl;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Decisions/Player/Button Pressed")]
public class ButtonPressedDecision : Decision
{
    public InputControlType button;
    PlayerController pC;

    public override bool Decide<T>(T controller)
    {
        bool buttonPressed;
        buttonPressed = CheckIfButtonPressed(controller as PlayerStateController);

        return buttonPressed;
    }

    //TODO Seems like excessive error checking. 
    //How does this decision get assigned to an object without a player controller.
    //Is it necessary to have a different Decision for handing over menu control?
    private bool CheckIfButtonPressed(PlayerStateController controller)
    {
        if (!(controller is PlayerStateController))
        {
            Debug.Log("State Controller is not a PlayerStateController");
        }

        if (controller.playerController.inputDevice is InputDevice)
        {
            return controller.playerController.inputDevice.GetControl(button);
        }

        return false;
    }
}