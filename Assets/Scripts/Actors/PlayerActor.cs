using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateControllerManagement;

[RequireComponent (typeof (PlayerStateController))]
public class PlayerActor : MonoBehaviour
{
    [HideInInspector]
    PlayerStateController playerStateController;

    private void Awake()
    {
        playerStateController = GetComponent<PlayerStateController>();
    }
}
