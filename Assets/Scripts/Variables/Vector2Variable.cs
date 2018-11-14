using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector 2")]
public class Vector2Variable : ScriptableObject 
{
    [SerializeField]
#pragma warning disable 649
	internal Vector2 value;
#pragma warning restore 649

}