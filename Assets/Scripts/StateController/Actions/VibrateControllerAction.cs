using UnityEngine;
using InControl;
using StateControllerManagement;

//Vibrates the controller.
[CreateAssetMenu(menuName = "Actions/Player/Vibrate Controller")]
class VibrateControllerAction : Action
{
    [Range(0.0f, 1.0f)]
    public float intensity = 0;

    public override void Act<T>(T controller)
    {
        VibrateController(controller as PlayerStateController);
    }

    private void VibrateController(PlayerStateController controller)
    {
        InputDevice inputDevice = controller.playerController.inputDevice;
        inputDevice.Vibrate(intensity);
    }
}