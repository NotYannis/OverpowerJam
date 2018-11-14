using UnityEngine;
using InControl;
using StateControllerManagement;

//Class to check if a button was released during the current frame.
//Note: Potential problem with checking for release rather than checking if up.
[CreateAssetMenu(menuName = "Decisions/Player/Button Released")]
public class ButtonReleasedDecision : Decision
{
    public InputControlType button;

    public override bool Decide<T>(T controller)
    {
        bool buttonReleased = false;

        buttonReleased = CheckButtonReleased(controller as PlayerStateController);

        return buttonReleased;
    }

    private bool CheckButtonReleased(PlayerStateController controller)
    {
        return !controller.playerController.inputDevice.GetControl(button);
    }
}
