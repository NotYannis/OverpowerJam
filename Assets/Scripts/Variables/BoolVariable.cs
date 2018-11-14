using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool")]
public class BoolVariable : ScriptableObject
{
    [SerializeField]
    internal bool value;
}