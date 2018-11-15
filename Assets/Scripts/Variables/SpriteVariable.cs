using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Sprite")]
public class SpriteVariable : ScriptableObject
{
	[SerializeField]
#pragma warning disable 649
	internal Sprite value;
#pragma warning restore 649

}