using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
	public enum TreeState { Small, Medium, Big }

	[Header("Trees")] public GameObject tree;
	[Range(0f, 2f)] 
	public float ungrowRate;
	public FloatVariable babyGrowthRates;
	public FloatVariable bushGrowthRates;
	public FloatVariable adultGrowthRates;
	public FloatVariable fruitGrowthRates;

	[FormerlySerializedAs("mass")]
	[Header("Fruits")]
	[Range(0.1f, 5f)]
	public float fruitMass;
	[Range(0f, 5f)]
	public float fruitLinearDrag;
	[Range(0f, 5f)]
	public float fruitAngularDrag;
	[Range(0f, 5f)]
	public float fruitGravityScale;

	[Header("Player")]
	public FloatVariable maxSpeed;
	public float fireRate;
}
