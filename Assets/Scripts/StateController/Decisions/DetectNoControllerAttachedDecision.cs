using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(fileName = "General_NoControllerAttached_Decision", menuName = "Decisions/General/DetectNoControllerAttachedDecision")]
public class DetectNoControllerAttachedDecision : Decision 
{
	public override bool Decide<T>(T controller)
	{
        bool controllerAttached = CheckInputAssignmentManager();
        return controllerAttached;
	}

    private bool CheckInputAssignmentManager()
    {
        return false;
        //return !InputAssignmentManager.Instance.controllerAssigned;
    }
}