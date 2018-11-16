using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (fileName = "PlayerLevel_", menuName = "Player Level Stats")]
public class PlayerLevelStats : ScriptableObject
{
    public Sprite normalSprite;
    public Sprite stunSprite;

    public Sprite spit_back;
    public Sprite spit_front;
    public Sprite spit_side;

    public Sprite walking_back;
    public Sprite walking_front;
    public Sprite walking_side;

    public float speed = 8; //Slows down the player when bigger, supposedly won't be used.

    public float knockoutDuration;
    public float knockoutPushbackForce;

    public float spoutOriginMinimumDistance = 0.75f;
	public float spoutYOffset;
	public float spoutZOffset = -0.01f;

    [Header ("Normal Blast")]
    public float force; //Power of the water jet. Effects distance water travels and force it pushes fruit
    public float quantity; //The amount of water particles/second.
	public float lifetime;
	public float particleForce;
    [Range(0, 1)]
    public float pushback; //The force the player is pushed back. Always a percantage of force

    [Header("Drip")]
    public Sprite holdingSprite;
    public float holdDuration; //The length of time that the player can hold the water in his mouth
    [FormerlySerializedAs("miniForce")] public float dripForce; //Power of the water jet. Effects distance water travels and force it pushes fruit
    [FormerlySerializedAs("miniQuantity")] public float dripQuantity; //The amount of water particles/second.
	public float dripLifetime;
	public float dripParticleForce;
    [FormerlySerializedAs("miniPushback")] [Range(0, 1)]
    public float dripPushback; //The force the player is pushed back. Always a percantage of force

    [FormerlySerializedAs("extraForceRate")] [Header ("Full Blast")]
    public float burstForce;
    public float holdingDecreaseSpeed;

	public float burstQuantity;
	public float burstLifetime;
	public float burstParticleForce;
	[Range(0, 1)]
	public float burstPushback = 0.3f;
	[Range(0, 10)]
	public float burstSpray = 3;

	public Action OnUpdate;
}