using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AimReticule : MonoBehaviour
{
    [SerializeField]
    private float distanceFromPlayer;

    private void Update()
    {
        if (new Vector3(InputManager.ActiveDevice.RightStickX, InputManager.ActiveDevice.RightStickY, 0).normalized != Vector3.zero)
            transform.localPosition = new Vector3(InputManager.ActiveDevice.RightStickX, InputManager.ActiveDevice.RightStickY, 0).normalized * distanceFromPlayer;
    }
}
