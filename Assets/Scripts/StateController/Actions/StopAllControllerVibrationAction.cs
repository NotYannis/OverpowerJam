using UnityEngine;
using StateControllerManagement;
using InControl;

[CreateAssetMenu(menuName = "Actions/General/Stop All Controller Vibrations")]
class StopAllControllerVibrationAction : Action
{
    public override void Act<T>(T controller)
    {
        for (int i = 0; i < InputManager.Devices.Count; ++i)
        {
            if (InputManager.Devices[i].active)
            {
                InputManager.Devices[i].Vibrate(0);
            }
        }
    }
}
