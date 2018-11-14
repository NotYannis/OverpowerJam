using UnityEngine;
using StateControllerManagement;

[RequireComponent (typeof (PlayerStateController))]
[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (PlayerController))]
public class PlayerActor : MonoBehaviour
{
    [HideInInspector]
    PlayerStateController playerStateController;
    [HideInInspector]
    SpriteRenderer spriteRenderer;
    [HideInInspector] PlayerController playerController;

    private void Awake()
    {
        playerStateController = GetComponent<PlayerStateController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }
}
