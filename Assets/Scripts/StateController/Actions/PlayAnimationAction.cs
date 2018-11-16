using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Actions/Tree/Play Animation")]
public class PlayAnimationAction : Action
{
    [SerializeField] string animationClip;

    public override void Act<T>(T controller)
    {
        controller.GetComponent<Animator>().Play(animationClip);
    }
}