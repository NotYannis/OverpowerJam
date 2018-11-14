using System.Collections;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float dropOutTimer = 10;
    private IEnumerator dropOut;

    public int deviceID = -1;
    public InputDevice inputDevice
    {
        get
        {
            if (deviceID != -1)
                return InputManager.ActiveDevice;// InputAssignmentManager.Instance.playerDevices[deviceID];
            else
                return null;
        }
    }

    private void Awake()
    {
        dropOut = DropOut();
    }

    public void StartDropOutTimer()
    {
        StartCoroutine(dropOut);
    }

    public void StopDropOutTimer()
    {
        StopCoroutine(dropOut);
    }

    IEnumerator DropOut()
    {
        yield return new WaitForSeconds(dropOutTimer);
        
        //InputAssignmentManager.Instance.RemovePlayerDevice(deviceID);
        //PlayerContainer.Instance.RemovePlayerByController(this);
        
        yield return null;
    }
}