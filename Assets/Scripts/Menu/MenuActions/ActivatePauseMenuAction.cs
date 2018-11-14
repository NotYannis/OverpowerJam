using UnityEngine;
using StateControllerManagement;
using MenuManagement;

[CreateAssetMenu(fileName = "ActivatePauseMenuAction_Action", menuName = "Actions/Menu/Activate Pause Menu")]
public class ActivatePauseMenuAction : Action 
{
	public override void Act<T>(T controller)
	{
        MenuManager.Instance.currentMenu = MenuManager.Instance.pauseMenu;
	}
}