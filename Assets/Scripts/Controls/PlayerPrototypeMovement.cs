using UnityEngine;
using InControl;

public class PlayerPrototypeMovement : MonoBehaviour
{
    Transform playerWeaponTransform;
    private float speed  = 8;
    private void Awake()
    {
        playerWeaponTransform = GetComponentInChildren<PlayerWeapon>().transform;
    }

    void Update()
    {

        transform.position += Vector3.right * InputManager.ActiveDevice.LeftStick.X * Time.deltaTime * speed;
        transform.position += Vector3.up * InputManager.ActiveDevice.LeftStick.Y * Time.deltaTime * speed;

        playerWeaponTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(InputManager.ActiveDevice.RightStick.Y, InputManager.ActiveDevice.RightStick.X) * 180 / Mathf.PI - 90);
    }
}
