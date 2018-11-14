using UnityEngine;
using StateControllerManagement;
using InControl;
using MenuManagement;

[CreateAssetMenu(fileName = "AssignRelevantMenuController_Action", menuName = "Actions/Menu/Assign Relevant Menu Controller")]
public class AssignRelevantMenuControllerAction : Action 
{
	public override void Act<T>(T controller)
	{
        MenuManager.Instance.playerInChargeController = InputManager.ActiveDevice;
	}
}