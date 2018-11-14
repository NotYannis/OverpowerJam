using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
	public enum TreeState { Small, Medium, Big }

	[Header("Trees")]
	public float[] growthRates;

	[Header("Player")]
	public FloatVariable maxSpeed;
	public float fireRate;
}
