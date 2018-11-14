using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(fileName = "_FrameTimer_Decision", menuName = "Decisions/General/Frame Timer")]
public class FrameTimerDecision : Decision
{
    [SerializeField]
    IntVariable numFrames;

    public override bool Decide<T>(T controller)
    {
        bool finishedAction = CheckFinishedAction(controller);
        return finishedAction;
    }

    private bool CheckFinishedAction(StateController controller)
    {
        return controller.CheckIfStateFrameCountElapsed(numFrames.value);
    }
}