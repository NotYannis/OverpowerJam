
using UnityEngine;

[CreateAssetMenu(fileName = "_TreeType", menuName = "Tree Type")]
public class TreeType : ScriptableObject
{
    [SerializeField]
    public SpriteVariable fruitSprite;
    [SerializeField]
    public SpriteVariable seedlingSprite;
    [SerializeField]
    public SpriteVariable bushSprite;
    [SerializeField]
    public SpriteVariable treeSprite;
}
