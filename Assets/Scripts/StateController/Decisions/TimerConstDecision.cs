using UnityEngine;
using StateControllerManagement;

//Basic waiting decision. Can decide how long a state remains active before transitioning.
[CreateAssetMenu(fileName = "_Timer_Decision",menuName = "Decisions/General/Timer")]
public class TimerConstDecision : Decision
{
    [SerializeField]
    FloatVariable timerDurationVariable;
    public override bool Decide<T>(T controller)
    {
        bool finishedAction = CheckFinishedAction(controller);
        return finishedAction;
    }

    private bool CheckFinishedAction(StateController controller)
    {
        return controller.CheckIfStateCountDownElapsed(timerDurationVariable.value);
    }
}