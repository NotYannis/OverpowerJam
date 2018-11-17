
using UnityEngine;

[CreateAssetMenu(fileName = "_TreeType", menuName = "Tree Type")]
public class TreeType : ScriptableObject
{
    [SerializeField]
    public GameObject prefabVariant;
    [SerializeField]
    public SpriteVariable fruitSprite;
    [SerializeField]
    public SpriteVariable seedlingSprite;
    [SerializeField]
    public SpriteVariable bushSprite;
    [SerializeField]
    public SpriteVariable treeSprite;

    [SerializeField]
    public RuntimeAnimatorController controller;
}
