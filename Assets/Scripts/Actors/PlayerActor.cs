using UnityEngine;
using StateControllerManagement;

[RequireComponent (typeof (PlayerStateController))]
[RequireComponent (typeof (PlayerController))]
public class PlayerActor : MonoBehaviour
{
    [HideInInspector]
    PlayerStateController playerStateController;
    [HideInInspector] PlayerController playerController;

    private void Awake()
    {
        playerStateController = GetComponent<PlayerStateController>();
        playerController = GetComponent<PlayerController>();
    }
}
