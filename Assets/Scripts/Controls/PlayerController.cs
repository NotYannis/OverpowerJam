using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{
    public InputDevice inputDevice
    {
        get
        {
            return InputManager.ActiveDevice;
        }
    }
}