using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Decisions/Tree/HasDropFruitAction")]
public class HasDropFruitDesicion : Decision
{
    public override bool Decide<T>(T controller)
    {
        return false;
    }
}