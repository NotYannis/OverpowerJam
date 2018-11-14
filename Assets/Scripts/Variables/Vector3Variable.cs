using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector 3")]
public class Vector3Variable : ScriptableObject
{
    [SerializeField]
#pragma warning disable 649
	internal Vector3 value;
#pragma warning restore 649
}