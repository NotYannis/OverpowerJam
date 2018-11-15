using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
	public enum TreeState { Small, Medium, Big }

	[Header("Trees")]
	public FloatVariable babyGrowthRates;
	public FloatVariable bushGrowthRates;
	public FloatVariable adultGrowthRates;
	public FloatVariable fruitGrowthRates;
	public FloatVariable ungrowthRate;

	[Header("Player")]
	public FloatVariable maxSpeed;
	public float fireRate;
}
