using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool")]
public class BoolVariable : ScriptableObject
{
    [SerializeField]
#pragma warning disable 649
	internal bool value;
#pragma warning restore 649

}