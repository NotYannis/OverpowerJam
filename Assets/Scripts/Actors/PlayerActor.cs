using UnityEngine;
using StateControllerManagement;

[RequireComponent (typeof (PlayerStateController))]
[RequireComponent (typeof (SpriteRenderer))]
public class PlayerActor : MonoBehaviour
{
    [HideInInspector]
    PlayerStateController playerStateController;
    [HideInInspector]
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerStateController = GetComponent<PlayerStateController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
