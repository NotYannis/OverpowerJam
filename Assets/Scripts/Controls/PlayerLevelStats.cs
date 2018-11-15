using UnityEngine;

[CreateAssetMenu (fileName = "PlayerLevel_", menuName = "Player Level Stats")]
public class PlayerLevelStats : ScriptableObject
{
    public Sprite normalSprite;
    public float speed = 8; //Slows down the player when bigger, supposedly won't be used.

    public float knockoutDuration;
    public float knockoutPushbackForce;

    public float spoutOriginMinimumDistance = 0.75f;

    [Header ("Normal Blast")]
    public float force; //Power of the water jet. Effects distance water travels and force it pushes fruit
    [Range(0, 10)]
    public float spray; //Randomness of directional control
    public float quantity; //The amount of water particles/second.
    [Range(0, 1)]
    public float pushback; //The force the player is pushed back. Always a percantage of force

    [Header("Drip")]
    public Sprite holdingSprite;
    public float holdDuration; //The length of time that the player can hold the water in his mouth
    public float miniForce; //Power of the water jet. Effects distance water travels and force it pushes fruit
    [Range(0, 10)]
    public float miniSpray; //Randomness of directional control
    public float miniQuantity; //The amount of water particles/second.
    [Range(0, 1)]
    public float miniPushback; //The force the player is pushed back. Always a percantage of force

    [Header ("Full Blast")]
    public float extraForceRate;
    public float holdingDecreaseSpeed;
    public float burstPushback = 0.3f;
    [Range(0,10)]
    public float burstSpray = 3;
}