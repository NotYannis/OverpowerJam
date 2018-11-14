using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField]
#pragma warning disable 649
	internal float value;
#pragma warning restore 649
}
