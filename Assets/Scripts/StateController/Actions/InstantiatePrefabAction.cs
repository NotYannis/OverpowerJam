using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(fileName = "Instantiate__Action", menuName = "Actions/General/Instantiate Prefab")]
public class InstantiatePrefabAction : Action
{
    GameObject prefab;
    Transform parent;

    public override void Act<T>(T controller)
    {
        GameObject instance = GameObject.Instantiate(prefab,parent);
    }
}