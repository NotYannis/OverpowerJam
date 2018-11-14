using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(fileName = "_PrevState_Decision", menuName = "Decisions/General/Prev State Condition")]
public class PrevStateConditionDecision : Decision
{
    [SerializeField]
    State prevState;

    [Tooltip("If true, decision will return true when the controller's prevState matches the field.\nElse returns true when it doesn't match.")]
    [SerializeField]
    bool prevStateIs;

    public override bool Decide<T>(T controller)
    {
        if (prevStateIs)
        {
            if (controller.prevState == prevState)
            {
                return true;
            }
        }
        else
        {
            if (controller.prevState != prevState)
            {
                return true;
            }
        }

        return false;
    }
}
