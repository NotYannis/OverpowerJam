using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(fileName = "Coroutine_Action", menuName = "Actions/General/Do Coroutine")]
public class DoCoroutineAction : Action
{
    [SerializeField]
    CoroutineAction coroutineAction;

    public override void Act<T>(T controller)
    {
        controller.StartCoroutine(controller.DoCoroutineAction(coroutineAction));
    }
}