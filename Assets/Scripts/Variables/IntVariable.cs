using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
#pragma warning disable 649
	internal int value;
#pragma warning restore 649
}