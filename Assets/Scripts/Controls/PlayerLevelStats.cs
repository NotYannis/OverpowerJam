using UnityEngine;

[CreateAssetMenu (fileName = "PlayerLevel_", menuName = "Player Level Stats")]
public class PlayerLevelStats : ScriptableObject
{
    public Sprite sprite;
    public float weight = 1; //Slows down the player when bigger, supposedly won't be used.
    public float force; //Power of the water jet. Effects distance water travels and force it pushes fruit
    [Range(0, 360)]
    public float spray; //Randomness of directional control
    public float quantity; //The amount of water particles/second.
    public float holdDuration; //The length of time that the player can hold the water in his mouth
    [Range(0, 1)]
    public float pushback; //The force the player is pushed back. Always a percantage of force
}