using UnityEngine;
using InControl;
using StateControllerManagement;

[CreateAssetMenu(fileName = "_ButtonPressedOnAnyController_Decision", menuName = "Decisions/General/Button Pressed On Any Controller")]
public class ButtonPressedOnAnyControllerDecision : Decision
{
    [SerializeField]
    InputControlType button;

    public override bool Decide<T>(T controller)
    {
        bool buttonPressed = GetButtonPressed();
        return buttonPressed;
    }

    private bool GetButtonPressed()
    {
        return InputManager.ActiveDevice.GetControl(button);
    }
}
