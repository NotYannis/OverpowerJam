using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
    internal int value;
}